using DatingApp.Models;
using DatingApp.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DatingApp.Controllers
{
    public class VisitorApiController : ApiController
    {
        private readonly UnitOfWork UnitOfWork = new UnitOfWork();

        [HttpGet]
        public List<VisitorViewModel> GetVisitors()
        {
            var currentId = UnitOfWork.ProfileRepository.GetProfileId(User.Identity.GetUserId());

            var visitors = UnitOfWork.VisitorRepository.GetVisitorProfiles(currentId);

            List<VisitorViewModel> visitorViewModels = new List<VisitorViewModel>();

            foreach(var visitor in visitors)
            {
                var visitorViewModel = new VisitorViewModel(visitor);
                visitorViewModels.Add(visitorViewModel);
            }

            return visitorViewModels;
        }

        protected override void Dispose(bool disposing)
        {
            UnitOfWork.Dispose();
            base.Dispose(disposing);
        }

    }
}
