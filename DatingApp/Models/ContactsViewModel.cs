using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static DatingApp.Models.ProfileViewModels;

namespace DatingApp.Models
{
    public class ContactsViewModel
    {
      
        public List<ContactProfileViewModel> Accepted { get; set; }
        public ProfilesIndexViewModel Pending { get; set; }

        public ContactsViewModel(List<ContactProfileViewModel> accepted, ProfilesIndexViewModel pending)
        {

            Accepted = accepted;
            Pending = pending;

        }

        public ContactsViewModel()
        {

        }
    }

    public class ContactProfileViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public Category Category { get; set; }

        public ContactProfileViewModel(ProfileModel profileModel, Category category)
        {
            Id = profileModel.Id;
            Name = profileModel.Name;
            Age = profileModel.Age;
            Gender = profileModel.Gender;
            Category = category;
        }

    }

}