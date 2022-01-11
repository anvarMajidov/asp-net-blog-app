using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Data;
using VotingSystem.Dtos.UserDtos;
using VotingSystem.Models;

namespace votingsystem.Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _db;

        public ArticleService(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        private string GetUserRole() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

        public async Task<ServiceResponse<List<GetArticleDto>>> AddArticle(AddArticleDto articleDto)
        {
            ServiceResponse<List<GetArticleDto>> response = new ServiceResponse<List<GetArticleDto>>();
            try {
                User curUser = await _db.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
                Article newArticle = new Article{
                    Header = articleDto.Header,
                    Body = articleDto.Body,
                    User = curUser
                };

                await _db.Articles.AddAsync(newArticle);
                await _db.SaveChangesAsync();

                response = await GetAllArticles();
            }
            catch(Exception e) {
                response.Success = false;
                response.ErrorMessage = e.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetArticleDto>>> DeleteArticle(int id)
        {
            ServiceResponse<List<GetArticleDto>> response = new ServiceResponse<List<GetArticleDto>>();

            Article article = GetUserRole().Equals("Admin") ?
            await _db.Articles.FirstOrDefaultAsync(a => a.Id == id) :
            await _db.Articles.FirstOrDefaultAsync(a => a.User.Id == GetUserId() && a.Id == id);

            if(article == null)
            {
                response.Success = false;
                response.ErrorMessage = "No such article with given id";
                return response;
            }
            
            _db.Articles.Remove(article);
            await _db.SaveChangesAsync();

            response = await GetAllArticles();
            return response;
        }

        public async Task<ServiceResponse<List<GetArticleDto>>> GetAllArticles()
        {
            ServiceResponse<List<GetArticleDto>> response = new ServiceResponse<List<GetArticleDto>>();
            List<Article> articles = GetUserRole().Equals("Admin") ?
            await _db.Articles.ToListAsync() :
            await _db.Articles.Where(a => a.User.Id == GetUserId()).ToListAsync();

            response.Data = articles.Select(a => new GetArticleDto{
                    Id = a.Id,
                    Header = a.Header,
                    Body = a.Body
                }).ToList();
            
            return response;
        }

        public async Task<ServiceResponse<GetArticleDto>> GetArticle(int id)
        {
            ServiceResponse<GetArticleDto> response = new ServiceResponse<GetArticleDto>();
            Article article = GetUserRole().Equals("Admin") ?
            await _db.Articles.FirstOrDefaultAsync(a => a.Id == id) :
            await _db.Articles.FirstOrDefaultAsync(a => a.Id == id && a.User.Id == GetUserId());

            if(article == null)
            {
                response.Success = false;
                response.ErrorMessage = "Article with such id not found";
                return response;
            }

            response.Data = new GetArticleDto{
                Id = article.Id,
                Header = article.Header,
                Body = article.Body
            };
            
            return response;
        }
    }
}
