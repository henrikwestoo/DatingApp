using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static DatingApp.Models.PostViewModels;

namespace DatingApp.Models
{
    public class ProfileViewModels
    {

        public class ProfileIndexViewModel {

            public ProfileIndexViewModel()
            {

            }

            public ProfileIndexViewModel(ProfileModel model)
            {
                Id = model.Id;
                Name = model.Name;
                Age = model.Age;
                _Gender = model._Gender;
                Biography = model.Biography;
                Image = model.Image;
                Posts = new List<PostIndexViewModel>();
            }


            public int Id { get; set; }

            [Display(Name = "Name")]
            [Required(ErrorMessage = "Field required")]
            [StringLength(50, MinimumLength = 1, ErrorMessage = "Your name can not be this long")]
            public string Name { get; set; }

            [Display(Name = "Age")]
            [Required(ErrorMessage = "Field required")]

            public int Age { get; set; }

            [Display(Name = "Gender")]
            [Required(ErrorMessage = "Field required")]
            public Gender _Gender { get; set; }

            [Display(Name = "Biography")]
            [Required(ErrorMessage = "Field required")]
            [StringLength(150, ErrorMessage = "Character limit is 150")]
            public string Biography { get; set; }

            public string Image { get; set; }

            public List<PostIndexViewModel> Posts { get; set; }
        }


        public class ProfilesIndexViewModel

        {

            public List<ProfileIndexViewModel> Profiles { get; set; }

            public ProfilesIndexViewModel()
            {
                Profiles = new List<ProfileIndexViewModel>();
            }

        }



    }
}