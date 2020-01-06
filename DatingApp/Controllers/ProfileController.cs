using DatingApp.DbManager;
using DatingApp.Models;
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
                _Gender = profileModel._Gender,
                Biography = profileModel.Biography,
                Image = "~/Images/" + file.FileName,
                UserId = User.Identity.GetUserId()
            };

            var ctx = new AppDbContext();
            ctx.Profiles.Add(profile);
            ctx.SaveChanges();
            ctx.Dispose();
            return RedirectToAction("Index", "Home");

        }

        public ActionResult IndexMe()
        {
            string userId = User.Identity.GetUserId();

            var ctx = new AppDbContext();

            var model = ctx.GetProfile(userId);
            var viewModel = new ProfileIndexViewModel(model);

            ctx.Dispose();

            return View(viewModel);
        }
        [HttpGet]
        public ActionResult Index(int userId)
        {

            var ctx = new AppDbContext();

            var model = ctx.GetProfile(userId);
            var viewModel = new ProfileIndexViewModel(model);

            var listOfPosts = new List<PostIndexViewModel>();

            foreach (var post in ctx.GetPosts(userId))
            {
                var postViewModel = new PostIndexViewModel(post);
                listOfPosts.Add(postViewModel);
            }

            viewModel.Posts = listOfPosts;

            ctx.Dispose();

            return View(viewModel);
        }


        public ActionResult Edit()
        {
            string userId = User.Identity.GetUserId();

            var ctx = new AppDbContext();

            var model = ctx.GetProfile(userId);
            var viewModel = new ProfileIndexViewModel(model);

            ctx.Dispose();

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

            var ctx = new AppDbContext();

            string fileName = "~/Images/" + file.FileName;

            ctx.EditProfile(foreignKey, viewModel, fileName);

            ctx.SaveChanges();
            ctx.Dispose();
            return RedirectToAction("IndexMe", "Profile");
        }

        [HttpGet]
        public ActionResult Search(string SearchBar)
        {
            var ctx = new AppDbContext();


            var profiles = ctx.FindProfiles(SearchBar);
            var profilesViewModel = new ProfilesIndexViewModel();
            profilesViewModel.Profiles = profiles;

            return View(profilesViewModel);
        }
    }
}