using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystem.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        [Required] public User User { get; set; }
    }
}
