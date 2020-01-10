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

        public void AddPost(PostModel model)
        {
            Ctx.Posts.Add(model);
        }

        public List<PostModel> GetPosts(int userId)
        {
            return Ctx.Posts.Where((u) => (u.ReceiverId == userId)).OrderByDescending(u => u.DateTime).ToList();
        }

        public void DeletePost(int postId)
        {
            var post = Ctx.Posts.Where((p) => (p.Id == postId)).First();
            Ctx.Set<PostModel>().Remove(post);

        }
    }
}