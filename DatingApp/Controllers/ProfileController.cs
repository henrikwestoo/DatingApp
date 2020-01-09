using DatingApp.Models;
using DatingApp.Repositories;
using DatingApp.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DatingApp.Models.ProfileViewModels;

namespace DatingApp.Controllers
{
    public class ProfileController : Controller
    {
        private UnitOfWork UnitOfWork = new UnitOfWork();

        // GET: Profile
        public ActionResult Create()    
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create(ProfileIndexViewModel profileModel)
        {
            var profile = new ProfileModel
            {
                Name = profileModel.Name,
                Age = profileModel.Age,
                Gender = profileModel.Gender,
                Biography = profileModel.Biography,
                Image = profileModel.Image,
                UserId = User.Identity.GetUserId(),
                Active = true
            };

            UnitOfWork.ProfileRepository.AddProfile(profile);
            UnitOfWork.Save();

            return RedirectToAction("Index", "Home");

        }

        [Authorize]
        public ActionResult IndexMe()
        {           
            string userId = User.Identity.GetUserId();
            var model = UnitOfWork.ProfileRepository.GetProfile(userId);

            var viewModel = new ProfileIndexViewModel(model);

            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index(int userId)
        {
            var model = UnitOfWork.ProfileRepository.GetProfile(userId);

            var viewModel = new ProfileIndexViewModel(model);

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Edit()
        {
            string userId = User.Identity.GetUserId();
            var model = UnitOfWork.ProfileRepository.GetProfile(userId);
            var viewModel = new ProfileIndexViewModel(model);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(ProfileIndexViewModel viewModel, HttpPostedFileBase file)
        {
            string fileName = "";

            if (file != null)
            {
                string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                file.SaveAs(path);
                fileName = "~/Images/" + file.FileName;
            }
            string foreignKey = User.Identity.GetUserId();           

            var model = UnitOfWork.ProfileRepository.GetProfile(UnitOfWork.ProfileRepository.GetProfileId(foreignKey));
            model.Name = viewModel.Name;
            model.Age = viewModel.Age;
            model.Gender = viewModel.Gender;
            model.Biography = viewModel.Biography;

            if (!String.IsNullOrEmpty(fileName))
            {
                model.Image = fileName;
            }

            UnitOfWork.ProfileRepository.EditProfile(model);

            UnitOfWork.Save();
            return RedirectToAction("IndexMe", "Profile");
        }

        
        public ActionResult Disable() {

            string foreignKey = User.Identity.GetUserId();

            var model = UnitOfWork.ProfileRepository.GetProfile(UnitOfWork.ProfileRepository.GetProfileId(foreignKey));
            model.Active = false;

            UnitOfWork.ProfileRepository.EditProfile(model);
            UnitOfWork.Save();

            return RedirectToAction("Index", "Manage");

        }

        public ActionResult Enable()
        {

            string foreignKey = User.Identity.GetUserId();

            var model = UnitOfWork.ProfileRepository.GetProfile(UnitOfWork.ProfileRepository.GetProfileId(foreignKey));
            model.Active = true;

            UnitOfWork.ProfileRepository.EditProfile(model);
            UnitOfWork.Save();

            return RedirectToAction("Index", "Manage");

        }

        [Authorize]
        [HttpGet]
        public ActionResult Search(string search)
        {
            var profiles = UnitOfWork.ProfileRepository.SearchProfiles(search);


            var currentId = User.Identity.GetUserId();
            int profileId = UnitOfWork.ProfileRepository.GetProfileId(currentId);

            var profilesViewModel = new ProfilesSearchViewModel();

            var contacts = UnitOfWork.ContactRepository.FindAllContacts(profileId);

            foreach (var profile in profiles)
            {
                if (profile.Id != profileId && profile.Active == true)
                {
                    bool isContact = false;

                    if (contacts.Contains(profile.Id))
                    {
                        isContact = true;
                    }

                    var profileViewModel = new ProfileSearchViewModel(profile, isContact);
                    profilesViewModel.Profiles.Add(profileViewModel);
                }
            }

            return View(profilesViewModel);
        }

        public ActionResult Download()
        {
            var profile = UnitOfWork.ProfileRepository.GetProfile(User.Identity.GetUserId());

            string path = Server.MapPath("~/ExportedUserData/" + profile.Id + ".xml");

            var downloadViewModel = new ProfileDownloadViewModel(profile);

            XMLSerializer.Serialize<ProfileDownloadViewModel>(downloadViewModel, path);

            return RedirectToAction("IndexMe");
        }

        protected override void Dispose(bool disposing)
        {
            UnitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}