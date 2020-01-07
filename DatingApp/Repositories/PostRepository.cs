using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatingApp.Repositories
{
    public class PostRepository : BaseRepository
    {
        public PostRepository(AppDbContext ctx) : base(ctx)
        {

        }

        public List<PostModel> GetPosts(int userId)
        {
            return Ctx.Posts.Where((u) => (u.ReceiverId == userId)).ToList();
        }
    }
}