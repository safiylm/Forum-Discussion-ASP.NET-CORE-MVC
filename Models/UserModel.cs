using System.ComponentModel.DataAnnotations;

namespace Forum_descussion_ASP.NET_core_mvc.Models
{

    public class UserModel
    {
        public int Id { get; set; }
        public String NameUser { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }

        public bool isEmailConfirm { get; set; }
        public List<QuestionModel> QuestionModel { get; set; }
        public List<ResponseModel> ResponseModel { get; set; }

    }
}
