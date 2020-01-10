using DatingApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DatingApp.Controllers
{
    public class PostController : Controller
    {
        private UnitOfWork UnitOfWork = new UnitOfWork();

        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void Delete(int postId)
        {

            UnitOfWork.PostRepository.DeletePost(postId);
            UnitOfWork.Save();

        }

        protected override void Dispose(bool disposing)
        {
            UnitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}