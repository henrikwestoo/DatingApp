using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatingApp.Repositories
{
    public class BaseRepository
    {
        protected AppDbContext Ctx;

        public BaseRepository()
        {
            Ctx = new AppDbContext();
        }

        public void SaveAndDispose()
        {
            Ctx.SaveChanges();
            Ctx.Dispose();
        }

        public void Save()
        {
            Ctx.SaveChanges();
        }

        public void Dispose()
        {
            Ctx.Dispose();
        }
    }
}