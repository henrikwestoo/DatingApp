using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatingApp.Models
{
    public class VisitorViewModel
    {

        public VisitorViewModel(VisitorModel model)
        {
            Id = model.Id;
            VisitorName = model.Visitor.Name;
            VisitorProfileId = model.Visitor.Id;
            VisitorActive = model.Visitor.Active;
        }

        public int Id { get; set; }

        public string VisitorName { get; set; }

        public int VisitorProfileId { get; set; }

        public bool VisitorActive { get; set; }

    }
}