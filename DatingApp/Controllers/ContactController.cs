using DatingApp.Models;
using DatingApp.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DatingApp.Models.ProfileViewModels;

namespace DatingApp.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        [Authorize]
        public ActionResult Index()
        {
            var profileRepo = new ProfileRepository();
            var contactRepo = new ContactRepository();
            var currentProfileId = profileRepo.GetProfileId(User.Identity.GetUserId());

            var acceptedContacts = contactRepo.FindContacts(currentProfileId, true);
            var pendingContacts = contactRepo.FindContacts(currentProfileId, false);

            var profilesContactsAccepted = profileRepo.FindProfiles(acceptedContacts);
            var profilesContactsPending = profileRepo.FindProfiles(pendingContacts);

            var profilesIndexViewModelContactsAccepted = new ProfilesIndexViewModel();
            var profilesIndexViewModelContactsPending = new ProfilesIndexViewModel();

            foreach (var model in profilesContactsAccepted)
            {
                var profileIndexViewModelAccepted = new ProfileIndexViewModel(model);
                profilesIndexViewModelContactsAccepted.Profiles.Add(profileIndexViewModelAccepted);
            }

            foreach (var model in profilesContactsPending)
            {
                var profileIndexViewModelPending = new ProfileIndexViewModel(model);
                profilesIndexViewModelContactsPending.Profiles.Add(profileIndexViewModelPending);
            }

            var allContacts = new ContactsViewModel(profilesIndexViewModelContactsAccepted, profilesIndexViewModelContactsPending);

            profileRepo.Dispose();
            contactRepo.Dispose();

            return View(allContacts);

        }

        [HttpPost]
        public ActionResult AddContact(int contactProfileId)
        {
            var profileRepo = new ProfileRepository();
            var currentProfileId = profileRepo.GetProfileId(User.Identity.GetUserId());

            var contactModel = new ContactModel
            {
                ProfileId = currentProfileId,
                ContactId = contactProfileId
            };

            var contactRepo = new ContactRepository();

            contactRepo.AddContact(contactModel);
            contactRepo.SaveAndDispose();
            profileRepo.Dispose();

            return RedirectToAction("Search", "Profile");
        }

        [HttpPost]

        public ActionResult AcceptContact(int contactUserId) {

            var profileRepo = new ProfileRepository();
            var contactRepo = new ContactRepository();

            var currentProfileId = profileRepo.GetProfileId(User.Identity.GetUserId());

            contactRepo.EditContact(currentProfileId, contactUserId);

            contactRepo.SaveAndDispose();
            profileRepo.Dispose();

            return RedirectToAction("Index", "Contact");
        
        }

        [HttpPost]
        public ActionResult DeclineContact(int contactUserId)
        {
            var profileRepo = new ProfileRepository();
            var contactRepo = new ContactRepository();

            var currentProfileId = profileRepo.GetProfileId(User.Identity.GetUserId());

            contactRepo.RemoveContact(currentProfileId, contactUserId);

            contactRepo.SaveAndDispose();
            profileRepo.Dispose();

            return RedirectToAction("Index", "Contact");

        }

        public ActionResult GetPendingRequests()
        {
            var profileRepo = new ProfileRepository();
            var contactRepo = new ContactRepository();

            var currentProfileId = profileRepo.GetProfileId(User.Identity.GetUserId());

            var pendingContacts = contactRepo.FindContacts(currentProfileId, false).Count;

            contactRepo.Dispose();
            profileRepo.Dispose();

            return Json(new { number = pendingContacts }, JsonRequestBehavior.AllowGet);
        }

    }
}