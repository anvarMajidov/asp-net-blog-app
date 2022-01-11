using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Dtos.UserDtos;
using VotingSystem.Models;

namespace votingsystem.Services.Articles
{
    public interface IArticleService
    {
        Task<ServiceResponse<List<GetArticleDto>>> GetAllArticles();
        Task<ServiceResponse<GetArticleDto>> GetArticle(int id);
        Task<ServiceResponse<List<GetArticleDto>>> AddArticle(AddArticleDto articleDto);
        Task<ServiceResponse<List<GetArticleDto>>> DeleteArticle(int id);
    }
}