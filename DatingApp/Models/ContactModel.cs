using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DatingApp.Models
{
    public class ContactModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Profile")]
        public virtual int ProfileId { get; set; }
        public virtual ProfileModel Profile { get; set; }

        [ForeignKey("Contact")]
        public virtual int ContactId { get; set; }
        public virtual ProfileModel Contact { get; set; }

    }
}