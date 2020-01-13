using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatingApp.Repositories
{
    /* En klass som länkar samman alla repositories och vår AppDbContext
     * 
     */
    public class UnitOfWork : IDisposable
    {
        private AppDbContext Ctx;
        private ProfileRepository profileRepository;
        private PostRepository postRepository;
        private ContactRepository contactRepository;
        private VisitorRepository visitorRepository;

        public UnitOfWork()
        {
            Ctx = new AppDbContext();
        }

        public ProfileRepository ProfileRepository
        {
            get
            {
                profileRepository = new ProfileRepository(Ctx);
                return profileRepository;
            }
        }

        public PostRepository PostRepository
        {
            get
            {
            postRepository = new PostRepository(Ctx);
            return postRepository;
            }
        }

        public ContactRepository ContactRepository
        {
            get
            {
                contactRepository = new ContactRepository(Ctx);
                return contactRepository;
            }
        }

        public VisitorRepository VisitorRepository
        {
            get
            {
                visitorRepository = new VisitorRepository(Ctx);
                return visitorRepository;
            }
        }

        public void Save()
        {
            Ctx.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Ctx.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}