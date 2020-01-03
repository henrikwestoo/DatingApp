using DatingApp.DbManager;
using DatingApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}