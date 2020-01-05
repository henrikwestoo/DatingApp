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

            var contactIds = ctx.FindContacts(currentProfileId);
            
            var profilesIndexViewModelContacts = ctx.FindProfiles(contactIds);

            ctx.Dispose();

            return View(profilesIndexViewModelContacts);

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
    }
}