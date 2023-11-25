using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Forum_descussion_ASP.NET_core_mvc.Models;

namespace Forum_descussion_ASP.NET_core_mvc.Data
{
    public class ForumContext : DbContext
    {
        public ForumContext (DbContextOptions<ForumContext> options)
            : base(options)
        {
        }

        public DbSet<QuestionModel> QuestionModel { get; set; } = default!;

        public DbSet<UserModel>? UserModel { get; set; }
        public DbSet<ResponseModel>? ResponseModel { get; set; }
        public DbSet<EnregistrementModel>? EnregistrementModel { get; set; }
    }
}
