using DatingApp.Repositories;
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
        private UnitOfWork UnitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            var viewModels = new ProfilesIndexViewModel();

            try
            {
                // Kontrollerar om användare är inloggad
                // Om sant, hämtas användarens profil-ID
                int profileId = 0;
                if (User.Identity.GetUserId() != null)
                {
                    profileId = UnitOfWork.ProfileRepository.GetProfileId(User.Identity.GetUserId());
                }

                var lowestProfileId = UnitOfWork.ProfileRepository.GetLowestProfileId();
                var profilesCount = UnitOfWork.ProfileRepository.CountProfiles();
                var highestProfileId = profilesCount + lowestProfileId;

                // Kontrollerar om det finns tillräckligt med profiler för att göra profilkorten på startsidan
                if (profilesCount > 3)
                {
                    Random rnd = new Random();

                    int a, b, c;
                    
                    //Genererar tre unika siffror, som inte är lika med id:t på den som är inloggad
                    do
                    {
                        a = rnd.Next(lowestProfileId, highestProfileId);
                        b = rnd.Next(lowestProfileId, highestProfileId);
                        c = rnd.Next(lowestProfileId, highestProfileId);
                    } while ((a == b) || (b == c) || (a == c) || (a == profileId) ||(b == profileId) || (c == profileId));

                    List<int> randomNumbers = new List<int>()
                    {
                        a,
                        b,
                        c
                    };

                    foreach (int item in randomNumbers)
                    {
                        var model = UnitOfWork.ProfileRepository.GetProfile(item);
                        var viewModel = new ProfileIndexViewModel(model);
                        viewModels.Profiles.Add(viewModel);
                    }

                    UnitOfWork.Dispose();

                    return View(viewModels);
                }
                else
                {
                    return View(viewModels);
                }
            }

            // Inga profiler kunde hämtas från databasen
            catch (InvalidOperationException)
            {
                return View(viewModels);
            }
        }
    }
}