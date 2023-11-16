﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace skolesystem.Models
{
    public class User_information
    {
        [Key]
        public int user_information_id { get; set; }

        [MaxLength(40)]
        public string name { get; set; }

        [MaxLength(60)]
        public string last_name { get; set; }

        [MaxLength(20)]
        public string phone { get; set; }
        
        [MaxLength(25)]
        public string date_of_birth { get; set; }

        [MaxLength(90)]
        public string address { get; set; }

        public bool is_deleted { get; set; }




        // Foreign keys

        public int gender_id { get; set; }

        public int user_id {get; set;}




    }
}