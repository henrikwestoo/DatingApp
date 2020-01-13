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
        private readonly UnitOfWork UnitOfWork = new UnitOfWork();

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


        [Route("api/postapi/display")]
        [HttpPost]
        // Hämtar inlägg på användarens vägg och lägger in dem i en viewmodel
        public List<PostIndexViewModel> Display([FromBody] string receiverId) {

            int id = Int32.Parse(receiverId);

            var postModels =  UnitOfWork.PostRepository.GetPosts(id);
            
            var postViewModels = new List<PostIndexViewModel>();

            foreach (var postModel in postModels)
            {
                string name = UnitOfWork.ProfileRepository.GetProfile(postModel.CreatorId).Name;

                var postViewModel = new PostIndexViewModel(postModel, name);
                postViewModels.Add(postViewModel);
            }

            return postViewModels;
        
        }

        protected override void Dispose(bool disposing)
        {
            UnitOfWork.Dispose();
            base.Dispose(disposing);
        }

    }
}
