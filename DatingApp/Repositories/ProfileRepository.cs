using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace DatingApp.Repositories
{
    public class ProfileRepository : BaseRepository
    {
        public ProfileRepository(AppDbContext ctx) : base(ctx)
        {

        }

        public void AddProfile(ProfileModel profile)
        {
            Ctx.Profiles.Add(profile);
        }

        public ProfileModel GetProfile(string userId)
        {
            int key = Ctx.Profiles.Where((p) => p.UserId.Equals(userId)).First().Id;
            return Ctx.Profiles.Find(key);
        }

        public ProfileModel GetProfile(int id)
        {
            return Ctx.Profiles.Find(id);
        }

        public int GetProfileId(string userId)
        {
            return Ctx.Profiles.Where((p) => p.UserId.Equals(userId)).First().Id;
        }

        public void EditProfile(ProfileModel model)
        {
            Ctx.Set<ProfileModel>().AddOrUpdate(model);
        }

        public List<ProfileModel> SearchProfiles(string search)
        {
            return Ctx.Profiles.Where((p) => p.Name.Contains(search)).ToList();
        }

        public List<ProfileModel> FindProfiles(List<int> contactIds)
        {
            var profiles = new List<ProfileModel>();

            foreach (int contactId in contactIds)
            {
                profiles.Add(Ctx.Profiles.Where((p) => p.Id == contactId).First());
            }

            return profiles;
        }

        public List<ProfileModel> GetThreeNewestUsers() {

            return Ctx.Profiles.OrderByDescending((x) => x.Id).Take(3).ToList();
        
        }

        public int CountProfiles()
        {
            return Ctx.Profiles.Count();
        }

        public int GetLowestProfileId()
        {
            return Ctx.Profiles.Select((x) => x.Id).Min();
        }
    }
}