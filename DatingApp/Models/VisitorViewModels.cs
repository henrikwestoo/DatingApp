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
            VisitorName = model.Visitor.Name;
        }

        public string VisitorName { get; set; }

    }
}