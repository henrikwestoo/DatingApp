﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DatingApp.Models
{
    public class ProfileModel
    {   
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public string Biography { get; set; }

        public string Image { get; set; }

        public bool Active { get; set; }

        public int CSharp { get; set; }
        public int JavaScript { get; set; }
        public int StackOverflow { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
}

    public enum Gender
    {
        Male,
        Female,
        Other
            
    }
}