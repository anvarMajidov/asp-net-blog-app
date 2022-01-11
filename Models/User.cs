using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        [Required] public string Role { get; set; }
        public List<Article> Articles { get; set; }
    }
}
