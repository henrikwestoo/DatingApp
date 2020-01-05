using DatingApp.DbManager;
using DatingApp.Models;
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
        public ActionResult Index()
        {
            var ctx = new AppDbContext();
            var currentProfileId = ctx.GetProfileId(User.Identity.GetUserId());

            var acceptedContacts = ctx.FindContacts(currentProfileId, true);
            var pendingContacts = ctx.FindContacts(currentProfileId, false);

            var profilesIndexViewModelContactsAccepted = ctx.FindProfiles(acceptedContacts);
            var profilesIndexViewModelContactsPending = ctx.FindProfiles(pendingContacts);

            var allContacts = new ContactsViewModel(profilesIndexViewModelContactsAccepted, profilesIndexViewModelContactsPending);

            ctx.Dispose();

            return View(allContacts);

        }

        [HttpPost]
        public ActionResult AddContact(int contactProfileId)
        {
            var ctx = new AppDbContext();
            var currentProfileId = ctx.GetProfileId(User.Identity.GetUserId());

            var contactModel = new ContactModel
            {
                ProfileId = currentProfileId,
                ContactId = contactProfileId
            };

            ctx.Contacts.Add(contactModel);
            ctx.SaveChanges();
            ctx.Dispose();

            return RedirectToAction("Search", "Profile");
        }

        [HttpPost]

        public ActionResult AcceptContact(int contactUserId) {

            var ctx = new AppDbContext();
            var currentProfileId = ctx.GetProfileId(User.Identity.GetUserId());

            ctx.EditContact(currentProfileId, contactUserId);

            ctx.SaveChanges();
            ctx.Dispose();

            return RedirectToAction("Index", "Contact");
        
        }

        [HttpPost]

        public ActionResult DeclineContact(int contactUserId)
        {

            var ctx = new AppDbContext();
            var currentProfileId = ctx.GetProfileId(User.Identity.GetUserId());

            ctx.RemoveContact(currentProfileId, contactUserId);

            ctx.SaveChanges();
            ctx.Dispose();

            return RedirectToAction("Index", "Contact");

        }

    }
}