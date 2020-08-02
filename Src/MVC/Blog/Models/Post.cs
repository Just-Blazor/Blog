using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Blog.Models
{
    public class Post : IdentityUser
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; } 

        public DateTime Day { get; set; } = DateTime.Now;
    }
}
