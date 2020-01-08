using DatingApp.Models;
using DatingApp.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DatingApp.Models.PostViewModels;
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

        [HttpPost]
        public ActionResult Create(ProfileModel profileModel, HttpPostedFileBase file)
        {
            string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
            file.SaveAs(path);

            var profile = new ProfileModel
            {
                Name = profileModel.Name,
                Age = profileModel.Age,
                Gender = profileModel.Gender,
                Biography = profileModel.Biography,
                Image = "~/Images/" + file.FileName,
                UserId = User.Identity.GetUserId()
            };

            UnitOfWork.ProfileRepository.AddProfile(profile);
            UnitOfWork.Save();

            return RedirectToAction("Index", "Home");

        }

        public ActionResult IndexMe()
        {           
            string userId = User.Identity.GetUserId();
            int profileId = UnitOfWork.ProfileRepository.GetProfileId(userId);

            var model = UnitOfWork.ProfileRepository.GetProfile(userId);

            var viewModel = new ProfileIndexViewModel(model);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Index(int userId)
        {
            var model = UnitOfWork.ProfileRepository.GetProfile(userId);

            var viewModel = new ProfileIndexViewModel(model);

            return View(viewModel);
        }


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
            if (file != null)
            {
                string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                file.SaveAs(path);
            }
            string foreignKey = User.Identity.GetUserId();
            string fileName = "~/Images/" + file.FileName;

            var model = UnitOfWork.ProfileRepository.GetProfile(UnitOfWork.ProfileRepository.GetProfileId(foreignKey));
            model.Name = viewModel.Name;
            model.Age = viewModel.Age;
            model.Gender = viewModel.Gender;
            model.Biography = viewModel.Biography;
            if (fileName != null)
            {
                model.Image = fileName;
            }

            UnitOfWork.ProfileRepository.EditProfile(model);

            UnitOfWork.Save();
            return RedirectToAction("IndexMe", "Profile");
        }

        [HttpGet]
        public ActionResult Search(string search)
        {
            var profiles = UnitOfWork.ProfileRepository.SearchProfiles(search);
            var profilesViewModel = new ProfilesIndexViewModel();

            foreach (var profile in profiles)
            {
                var profileViewModel = new ProfileIndexViewModel(profile);
                profilesViewModel.Profiles.Add(profileViewModel);
            }

            return View(profilesViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            UnitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}