using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatingApp.Repositories
{
    public class VisitorRepository : BaseRepository
    {
        public VisitorRepository(AppDbContext Ctx) : base(Ctx)
        {

        }

        public void AddVisitor(VisitorModel model)
        {
            Ctx.Visitors.Add(model);
        }

        public List<VisitorModel> GetVisitorProfiles(int id)
        {
            return Ctx.Visitors.Where((p) => p.ProfileId == id).ToList();
        }

        public void RemoveOldestVisitor()
        {
            var oldestVisitorId = Ctx.Visitors.Select((x) => x.Id).Min();
            var visitorToRemove = Ctx.Visitors.Where((v) => v.Id == oldestVisitorId).FirstOrDefault();

            Ctx.Set<VisitorModel>().Remove(visitorToRemove);
        }
    }
}