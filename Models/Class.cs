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
             //if (context.UserModel.Any())
             //{
             //    return;   // DB has been seeded
             //}
             //context.UserModel.AddRange(
             //    new UserModel
             //    {
             //        NameUser = "Safinaz ",
             //        Email = "safinaz@gmail.com",
             //        Password = "Romantic",
             //        isEmailConfirm= false
             //    },
             //    new UserModel
             //    {
             //        NameUser = "Berat",
             //        Email = "Berat@gmail.com",
             //        Password = "Comedy",
             //        isEmailConfirm = false

             //    }
             //);
            

            ////// Look for any user.
            //if (context.QuestionModel.Any())
            //{
            //    return;   // DB has been seeded
            //}
            ////var a = context.UserModel.FirstOrDefaultAsync(m => m.NameUser == "Berat");


            //context.QuestionModel.AddRange(
            //    new QuestionModel
            //    {
            //        UserId = 15,
            //        Titre = "How to make pizza like italians? ",
            //        Topic = "Cook",
            //        Description = "bal bla bla bla y",
            //        DateCreation = DateTime.Now,
            //        isResolu = false

            //    });
            //    new QuestionModel
            //    {
            //        UserId = 15 ,
            //        Titre = "How to make tea like turc? ",
            //        Topic = "Cook",
            //        Description = "tea bal bla bla bla y",
            //        DateCreation = DateTime.Now,
            //        isRepondu = false
            //    }
            //) ;


            //// Look for any user.
            if (context.ResponseModel.Any())
            {
                return;   // DB has been seeded
            }


            context.ResponseModel.AddRange(
                new ResponseModel
                {
                    UserId = 15,
                    QuestionId = 8,
                    ResponseContent = "Response Lorem ipseum .......................................",
                    DateCreation = DateTime.Now

                }//,
                //new ResponseModel
            //    //{
            //    //    UserId = 15,
            //    //    QuestionId = 8,
            //    //    ResponseContent = "Response Lorem ipseum 2 .......................................",
            //    //    DateCreation = DateTime.Now
            //    //}
            ) ;



            context.SaveChanges();
        }
    }
}
