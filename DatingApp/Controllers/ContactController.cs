using DatingApp.Models;
using DatingApp.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DatingApp.Models.ProfileViewModels;

namespace DatingApp.Controllers
{
    public class ContactController : Controller
    {
        private UnitOfWork UnitOfWork = new UnitOfWork();
        // GET: Contact
        [Authorize]
        public ActionResult Index()
        {
            var currentProfileId = UnitOfWork.ProfileRepository.GetProfileId(User.Identity.GetUserId());

            var acceptedContacts = UnitOfWork.ContactRepository.FindContacts(currentProfileId, true);
            var pendingContacts = UnitOfWork.ContactRepository.FindContacts(currentProfileId, false);

            var profilesContactsAccepted = UnitOfWork.ProfileRepository.FindProfiles(acceptedContacts);
            var profilesContactsPending = UnitOfWork.ProfileRepository.FindProfiles(pendingContacts);

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


            return View(allContacts);

        }

        [HttpPost]
        public ActionResult AddContact(int contactProfileId)
        {
            var currentProfileId = UnitOfWork.ProfileRepository.GetProfileId(User.Identity.GetUserId());

            var contactModel = new ContactModel
            {
                ProfileId = currentProfileId,
                ContactId = contactProfileId
            };

            UnitOfWork.ContactRepository.AddContact(contactModel);
            UnitOfWork.Save();

            return RedirectToAction("Search", "Profile");
        }

        [HttpPost]

        public ActionResult AcceptContact(int contactUserId)
        {

            var currentProfileId = UnitOfWork.ProfileRepository.GetProfileId(User.Identity.GetUserId());

            UnitOfWork.ContactRepository.EditContact(currentProfileId, contactUserId);

            UnitOfWork.Save();

            return RedirectToAction("Index", "Contact");

        }

        [HttpPost]
        public ActionResult DeclineContact(int contactUserId)
        {
            var currentProfileId = UnitOfWork.ProfileRepository.GetProfileId(User.Identity.GetUserId());

            UnitOfWork.ContactRepository.RemoveContact(currentProfileId, contactUserId);

            UnitOfWork.Save();

            return RedirectToAction("Index", "Contact");

        }

        public ActionResult GetPendingRequests()
        {
            int pendingContacts = 0;

            try
            {
                var currentProfileId = UnitOfWork.ProfileRepository.GetProfileId(User.Identity.GetUserId());
                pendingContacts = UnitOfWork.ContactRepository.FindContacts(currentProfileId, false).Count;
            }

            catch (InvalidOperationException)
            {
                Debug.Write("No profile found");
            }

            return Json(new { number = pendingContacts }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            UnitOfWork.Dispose();
            base.Dispose(disposing);
        }

    }
}