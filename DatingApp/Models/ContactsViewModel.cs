using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static DatingApp.Models.ProfileViewModels;

namespace DatingApp.Models
{
    public class ContactsViewModel
    {
      
        public ProfilesIndexViewModel Accepted { get; set; }
        public ProfilesIndexViewModel Pending { get; set; }

        public ContactsViewModel(ProfilesIndexViewModel accepted, ProfilesIndexViewModel pending)
        {

            Accepted = accepted;
            Pending = pending;

        }

        public ContactsViewModel()
        {

        }
    }
}