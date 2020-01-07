using DatingApp.Models;
using DatingApp.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static DatingApp.Models.PostViewModels;

namespace DatingApp.Controllers
{
    public class PostApiController : ApiController
    {
        private UnitOfWork UnitOfWork = new UnitOfWork();

        [HttpPost]
        public string Send([FromBody] PostIndexViewModel viewModel)
        {
            try
            {

                string userId = User.Identity.GetUserId();
                int profileId = UnitOfWork.ProfileRepository.GetProfileId(userId);

                var post = new PostModel
                {
                    CreatorId = profileId,
                    ReceiverId = viewModel.ReceiverId,
                    Content = viewModel.Content,
                    DateTime = viewModel.DateTime
                };

                UnitOfWork.PostRepository.AddPost(post);
                UnitOfWork.Save();
                return "Ok";
            } catch
            {
                return "Inte ok";
            }
        }
    }
}
