using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatingApp.Repositories
{
    // Basrepository som alla repositories ärver ifrån
    public class BaseRepository
    {
        protected AppDbContext Ctx;

        public BaseRepository(AppDbContext ctx)
        {
            Ctx = ctx;
        }
    }
}