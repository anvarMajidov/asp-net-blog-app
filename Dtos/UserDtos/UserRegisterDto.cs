using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystem.Dtos.UserDtos
{
    public class UserRegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
