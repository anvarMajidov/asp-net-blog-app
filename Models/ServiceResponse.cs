using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.Dtos.UserDtos;

namespace VotingSystem.Models
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string ErrorMessage { get; set; }

        public static implicit operator ServiceResponse<T>(ServiceResponse<List<GetArticleDto>> v)
        {
            throw new NotImplementedException();
        }
    }
}
