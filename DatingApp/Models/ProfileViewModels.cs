using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatingApp.Models
{
    public class ProfileViewModels
    {

        public class ProfileIndexViewModel {


            public string Name { get; set; }

            public int Age { get; set; }

            public Gender _Gender { get; set; }

            public string Biography { get; set; }


        }



    }
}