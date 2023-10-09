using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Forum_descussion_ASP.NET_core_mvc.Data;
using System;
using System.Linq;

namespace Forum_descussion_ASP.NET_core_mvc.Models;

public static class Class
{

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ForumContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ForumContext>>()))
        {
            // Look for any user.
            if (context.UserModel.Any())
            {
                return;   // DB has been seeded
            }
            context.UserModel.AddRange(
                new UserModel
                {
                    NameUser = "Safinaz ",
                    Email = "safinaz@gmail.com",
                    Password = "Romantic "
                },
                new UserModel
                {
                    NameUser = "Berat ",
                    Email = "Berat@gmail.com",
                    Password = " Comedy"
                }
            );


            // Look for any user.
            if (context.QuestionModel.Any())
            {
                return;   // DB has been seeded
            }

            context.QuestionModel.AddRange(
                new QuestionModel
                {
                    UserId = 1008,
                    Titre = "How to make pizza like italians? ",
                    Topic = "Cook",
                    Description = "bal bla bla bla y",
                 
                },
                new QuestionModel
                {
                    UserId = 1008,
                    Titre = "How to make tea like turc? ",
                    Topic = "Cook",
                    Description = "tea bal bla bla bla y",
                    
                }
            );


            context.SaveChanges();
        }
    }
}
