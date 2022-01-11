using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.Models;

namespace VotingSystem.Data.AuthenticationRepo
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
        Task<bool> AdminExists();
    }
}
