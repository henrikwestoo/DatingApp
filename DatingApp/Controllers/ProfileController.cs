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
    public class ProfileController : Controller
    {
        // GET: Profile


        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProfileModel profileModel)
        {
            var profile = new ProfileModel { Name = profileModel.Name, Age = profileModel.Age,
                                           _Gender = profileModel._Gender, Biography = profileModel.Biography,
                                            UserId = User.Identity.GetUserId()};

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
        public ActionResult Edit(ProfileIndexViewModel viewModel)
        {

            string foreignKey = User.Identity.GetUserId();

            var ctx = new AppDbContext();

            ctx.EditProfile(foreignKey, viewModel);

            ctx.SaveChanges();
            ctx.Dispose();
            return RedirectToAction("IndexMe", "Profile");

        }

        [HttpGet]
        public ActionResult Search(string SearchBar)
        {
            var ctx = new AppDbContext();

            var profiles = new ProfilesIndexViewModel();

            profiles.Profiles = ctx.FindProfiles(SearchBar);

            return View(profiles);
        }

    }
}