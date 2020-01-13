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
                Gender = model.Gender;
                Biography = model.Biography;
                Image = model.Image;
                Active = model.Active;
                CSharp = model.CSharp;
                JavaScript = model.JavaScript;
                StackOverflow = model.StackOverflow;
            }

            public ProfileIndexViewModel(ProfileModel model, List<ProfileModel> visitors)
            {
                Id = model.Id;
                Name = model.Name;
                Age = model.Age;
                Gender = model.Gender;
                Biography = model.Biography;
                Image = model.Image;
                Active = model.Active;
                CSharp = model.CSharp;
                JavaScript = model.JavaScript;
                StackOverflow = model.StackOverflow;
                Visitors = visitors;
            }


            public int Id { get; set; }

            [Display(Name = "Name")]
            [Required(ErrorMessage = "Name is required")]
            [StringLength(50, MinimumLength = 1, ErrorMessage = "Your name can not be this long")]
            public string Name { get; set; }

            [Display(Name = "Age")]
            [Required(ErrorMessage = "Age is required")]

            public int Age { get; set; }

            [Display(Name = "Gender")]
            [Required(ErrorMessage = "Gender is required")]
            public Gender Gender { get; set; }

            [Display(Name = "Motto")]
            [Required(ErrorMessage = "Motto is required")]
            [StringLength(50, MinimumLength = 1, ErrorMessage = "Character limit is 30")]
            public string Biography { get; set; }

            public bool Active { get; set; }

            public string Image { get; set; }
            [Display(Name = "C#")]
            public int CSharp { get; set; }
            [Display(Name = "JavaScript")]
            public int JavaScript { get; set; }
            [Display(Name = "Stack Overflow-ability")]
            public int StackOverflow { get; set; }

            public List<ProfileModel> Visitors { get; set; }
        }


        public class ProfilesIndexViewModel

        {

            public List<ProfileIndexViewModel> Profiles { get; set; }

            public ProfilesIndexViewModel()
            {
                Profiles = new List<ProfileIndexViewModel>();
            }

        }

        public class ProfileSearchViewModel
        {
            public ProfileSearchViewModel(ProfileModel model, bool isContact, int matchPercentage)
            {
                Id = model.Id;
                Name = model.Name;
                Age = model.Age;
                Gender = model.Gender;
                Biography = model.Biography;
                Image = model.Image;
                IsContact = isContact;
                MatchPercentage = matchPercentage;
            }


            public int Id { get; set; }

            [Display(Name = "Name")]
            public string Name { get; set; }

            [Display(Name = "Age")]
            public int Age { get; set; }

            [Display(Name = "Gender")]
            public Gender Gender { get; set; }

            [Display(Name = "Biography")]
            public string Biography { get; set; }

            public string Image { get; set; }

            public bool IsContact { get; set; }
            public int MatchPercentage { get; set; }
        }

        public class ProfilesSearchViewModel
        {
            public List<ProfileSearchViewModel> Profiles { get; set; }

            public ProfilesSearchViewModel()
            {
                Profiles = new List<ProfileSearchViewModel>();
            }
        }

        public class ProfileDownloadViewModel
        {
            public ProfileDownloadViewModel(ProfileModel model)
            {
                Name = model.Name;
                Age = model.Age;
                Gender = model.Gender;
                Biography = model.Biography;
            }

            public ProfileDownloadViewModel()
            {

            }

            public string Name { get; set; }
            public int Age { get; set; }
            public Gender Gender { get; set; }
            public string Biography { get; set; }
        }

    }
}