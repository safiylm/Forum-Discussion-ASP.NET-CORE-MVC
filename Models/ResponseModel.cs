﻿using System.ComponentModel.DataAnnotations;

namespace Forum_descussion_ASP.NET_core_mvc.Models
{

    public class ResponseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public String ResponseContent{ get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateCreation { get; set; }

        public UserModel User { get; set; }

        public QuestionModel Question { get; set; }
        public List<EnregistrementModel> EnregistrementModel { get; set; }


    }
}
