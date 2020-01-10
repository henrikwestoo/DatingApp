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
        }

        public int Id { get; set; }

        public string VisitorName { get; set; }

    }
}