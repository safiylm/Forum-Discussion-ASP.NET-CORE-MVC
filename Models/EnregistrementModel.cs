using Humanizer;
using System.ComponentModel.DataAnnotations;

namespace Forum_descussion_ASP.NET_core_mvc.Models
{
    public class EnregistrementModel
    {

        public int Id { get; set; }
        public int UserId   { get; set; }
        public int QuestionId { get; set; }
        public int ResponseId { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateCreation { get; set; }
        public QuestionModel Question { get; set; }
        public ResponseModel Response { get; set; }
    
}
}
