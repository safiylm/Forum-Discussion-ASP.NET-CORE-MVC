using System.ComponentModel.DataAnnotations;

namespace Forum_descussion_ASP.NET_core_mvc.Models
{

    public class ResponseModel
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public String ResponseContent{ get; set; }
        public DateTime DateCreation { get; set; }

        public UserModel User { get; set; }

        public QuestionModel Question { get; set; }
    }
}
