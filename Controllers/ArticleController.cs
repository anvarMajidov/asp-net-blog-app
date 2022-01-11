using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using votingsystem.Services.Articles;
using VotingSystem.Dtos.UserDtos;
using VotingSystem.Models;

namespace VotingSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ServiceResponse<List<GetArticleDto>> response = await _articleService.GetAllArticles();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(int id)
        {
            ServiceResponse<GetArticleDto> response = await _articleService.GetArticle(id);
            if(response.Data == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle(AddArticleDto articleDto)
        {
            ServiceResponse<List<GetArticleDto>> response = await _articleService.AddArticle(articleDto);

            if(response.Data == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArticle(GetArticleDto updatedArticle)
        {
            ServiceResponse<GetArticleDto> response = await _articleService.UpdateArticle(updatedArticle);
            if (response.Data == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            ServiceResponse<List<GetArticleDto>> response = await _articleService.DeleteArticle(id);
            if(response.Data == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}
