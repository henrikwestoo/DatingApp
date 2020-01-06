using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DatingApp.Models
{
    public class PostModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Creator")]
        public virtual int CreatorId { get; set; }
        public virtual ProfileModel Creator { get; set; }

        [ForeignKey("Reciever")]
        public virtual int RecieverId { get; set; }
        public virtual ProfileModel Reciever { get; set; }

        public string Content { get; set; }

        public DateTime DateTime { get; set; }

    }
}