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

            var profileRepo = new ProfileRepository();
            profileRepo.AddProfile(profile);
            profileRepo.SaveAndDispose();

            return RedirectToAction("Index", "Home");

        }

        public ActionResult IndexMe()
        {
            var postRepo = new PostRepository();
            var profileRepo = new ProfileRepository();            
            
            string userId = User.Identity.GetUserId();
            int profileId = profileRepo.GetProfileId(userId);

            var model = profileRepo.GetProfile(userId);

            var viewModel = new ProfileIndexViewModel(model);
            var listOfPosts = new List<PostIndexViewModel>();

            foreach (var post in postRepo.GetPosts(profileId))
            {
                var postViewModel = new PostIndexViewModel(post);
                listOfPosts.Add(postViewModel);
            }

            viewModel.Posts = listOfPosts;

            postRepo.Dispose();
            profileRepo.Dispose();

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Index(int userId)
        {
            var profileRepo = new ProfileRepository();
            var postRepo = new PostRepository();

            var model = profileRepo.GetProfile(userId);

            var viewModel = new ProfileIndexViewModel(model);
            var listOfPosts = new List<PostIndexViewModel>();

            foreach (var post in postRepo.GetPosts(userId))
            {
                var postViewModel = new PostIndexViewModel(post);
                listOfPosts.Add(postViewModel);
            }

            viewModel.Posts = listOfPosts;

            postRepo.Dispose();
            profileRepo.Dispose();

            return View(viewModel);
        }


        public ActionResult Edit()
        {
            string userId = User.Identity.GetUserId();

            var profileRepo = new ProfileRepository();

            var model = profileRepo.GetProfile(userId);
            var viewModel = new ProfileIndexViewModel(model);

            profileRepo.Dispose();

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

            var profileRepo = new ProfileRepository();
            string foreignKey = User.Identity.GetUserId();
            string fileName = "~/Images/" + file.FileName;

            var model = profileRepo.GetProfile(profileRepo.GetProfileId(foreignKey));
            model.Name = viewModel.Name;
            model.Age = viewModel.Age;
            model.Gender = viewModel.Gender;
            model.Biography = viewModel.Biography;
            if (fileName != null)
            {
                model.Image = fileName;
            }

            profileRepo.EditProfile(model);

            profileRepo.SaveAndDispose();
            return RedirectToAction("IndexMe", "Profile");
        }

        [HttpGet]
        public ActionResult Search(string search)
        {
            var profileRepo = new ProfileRepository();

            var profiles = profileRepo.SearchProfiles(search);
            var profilesViewModel = new ProfilesIndexViewModel();

            foreach (var profile in profiles)
            {
                var profileViewModel = new ProfileIndexViewModel(profile);
                profilesViewModel.Profiles.Add(profileViewModel);
            }

            profileRepo.Dispose();

            return View(profilesViewModel);
        }
    }
}