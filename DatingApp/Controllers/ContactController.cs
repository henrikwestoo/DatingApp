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

            // Hämtar en dictionary med alla användarens kontakter och kategorier
            var acceptedContactsAndCategories = UnitOfWork.ContactRepository.FindContactsAndCategories(currentProfileId);

            var pendingContactIds = UnitOfWork.ContactRepository.FindContactIds(currentProfileId, false);

            var profilesContactsPending = UnitOfWork.ProfileRepository.FindProfiles(pendingContactIds);

            var listOfProfileContactViewModel = new List<ContactProfileViewModel>();
            var profilesIndexViewModelContactsPending = new ProfilesIndexViewModel();

            foreach (var item in acceptedContactsAndCategories)
            {
                if (item.Key.Active == true)
                {
                    var profileContactViewModel = new ContactProfileViewModel(item.Key, item.Value);
                    listOfProfileContactViewModel.Add(profileContactViewModel);
                }
            }

            foreach (var model in profilesContactsPending)
            {
                if (model.Active == true)
                {
                    var profileIndexViewModelPending = new ProfileIndexViewModel(model);
                    profilesIndexViewModelContactsPending.Profiles.Add(profileIndexViewModelPending);
                }
            }

            var allContacts = new ContactsViewModel(listOfProfileContactViewModel, profilesIndexViewModelContactsPending);


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

            UnitOfWork.ContactRepository.AcceptContact(currentProfileId, contactUserId);

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

        [HttpPost]
        public void EditCategory(int contactId, Category category)
        {
            var currentProfileId = UnitOfWork.ProfileRepository.GetProfileId(User.Identity.GetUserId());
            UnitOfWork.ContactRepository.EditCategory(currentProfileId, contactId, category);

            UnitOfWork.Save();


        }

        public ActionResult GetPendingRequests()
        {
            int pendingContacts = 0;

            try
            {
                var currentProfileId = UnitOfWork.ProfileRepository.GetProfileId(User.Identity.GetUserId());
                pendingContacts = UnitOfWork.ContactRepository.FindContactIds(currentProfileId, false).Count;
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