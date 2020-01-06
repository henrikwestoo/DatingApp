using DatingApp.DbManager;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DatingApp.Models.ProfileViewModels;

namespace DatingApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var ctx = new AppDbContext();

            var profileId = ctx.GetProfileId(User.Identity.GetUserId());

            var numbers = Enumerable.Range(1, ctx.Profiles.Count()).OrderBy(n => n * n * (new Random()).Next());
            var distinctNumbers = numbers.Distinct().Take(3);

            while (distinctNumbers.Contains(profileId))
            {
                numbers = Enumerable.Range(1, ctx.Profiles.Count()).OrderBy(n => n * n * (new Random()).Next());
                distinctNumbers = numbers.Distinct().Take(3);
            }

            var viewModels = new ProfilesIndexViewModel();

            foreach (int item in distinctNumbers)
            {
                var model = ctx.GetProfile(item);
                var viewModel = new ProfileIndexViewModel(model);
                viewModels.Profiles.Add(viewModel);
            }

            ctx.Dispose();

            return View(viewModels);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}