using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Forum_descussion_ASP.NET_core_mvc.Models
{
    public class QuestionModel
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Titre { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public UserModel User { get; set;  }

    }
 }