using DatingApp.Models;
using DatingApp.Repositories;
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
                var post = new PostModel
                {
                    CreatorId = viewModel.CreatorId,
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
