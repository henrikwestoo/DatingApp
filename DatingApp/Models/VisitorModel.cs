using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DatingApp.Models
{
    public class VisitorModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Profile")]
        public virtual int ProfileId { get; set; }
        public virtual ProfileModel Profile { get; set; }

        [ForeignKey("Visitor")]
        public virtual int VisitorId { get; set; }
        public virtual ProfileModel Visitor { get; set; }
    }
}