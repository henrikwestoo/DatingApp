using DatingApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using static DatingApp.Models.ProfileViewModels;

namespace DatingApp.DbManager
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ProfileModel> Profiles { get; set; }

        public ProfileModel GetProfile(string userId)
        { 
            int key = Profiles.Where((p) => p.UserId.Equals(userId)).First().Id;
            var model = Profiles.Find(key);
            return model;
        }

        public int GetProfileId(string userId)
        {

            return Profiles.Where((p) => p.UserId.Equals(userId)).First().Id;

        }

        public ProfileModel GetProfile(int id)
        {
            var model = Profiles.Find(id);
            return model;
        }

        public void EditProfile(string foreignkey, ProfileIndexViewModel viewModel)
        {
            int key = Profiles.Where((p) => p.UserId.Equals(foreignkey)).First().Id;
            var model = Profiles.Find(key);
            model.Name = viewModel.Name;
            model.Age = viewModel.Age;
            model._Gender = viewModel._Gender;
            model.Biography = viewModel.Biography;

            Set<ProfileModel>().AddOrUpdate(model);
        }

        public List<ProfileModel> FindProfiles(string search) 
        {

            return Profiles.Where((p) => p.Name.Equals(search)).ToList();
        
        }


        public AppDbContext() : base("DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>(); // Enable cascade delete when you remove something that requires it.
            base.OnModelCreating(modelBuilder);
        }



    }
}