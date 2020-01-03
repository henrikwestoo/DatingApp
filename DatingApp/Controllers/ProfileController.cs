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
        public ActionResult Create(ProfileModels profileModel)
        {
            var profile = new ProfileModels { Name = profileModel.Name, Age = profileModel.Age,
                                           _Gender = profileModel._Gender, Biography = profileModel.Biography,
                                            UserId = User.Identity.GetUserId()};

            var ctx = new AppDbContext();
            ctx.Profiles.Add(profile);
            ctx.SaveChanges();
            return RedirectToAction("Index", "Home");
          
        }

        public ActionResult Index()
        {
            string foreignKey = User.Identity.GetUserId();

            var ctx = new AppDbContext();

            int key = ctx.Profiles.Where((p) => p.UserId.Equals(foreignKey)).First().Id;

            var model = ctx.Profiles.Find(key);
            var viewModel = new ProfileIndexViewModel();

            viewModel.Name = model.Name;
            viewModel.Age = model.Age;
            viewModel._Gender = model._Gender;
            viewModel.Biography = model.Biography;

            return View(viewModel);
        }

        public ActionResult Edit()
        {
            return View();
        }

    }
}