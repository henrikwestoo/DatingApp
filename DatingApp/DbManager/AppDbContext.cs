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

        public DbSet<ContactModel> Contacts { get; set; }

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

        public void EditProfile(string foreignkey, ProfileIndexViewModel viewModel, string fileName)
        {
            int key = Profiles.Where((p) => p.UserId.Equals(foreignkey)).First().Id;
            var model = Profiles.Find(key);
            model.Name = viewModel.Name;
            model.Age = viewModel.Age;
            model._Gender = viewModel._Gender;
            model.Biography = viewModel.Biography;
            if (fileName != null)
            {
                model.Image = fileName;
            }

            Set<ProfileModel>().AddOrUpdate(model);
        }

        public List<ProfileIndexViewModel> FindProfiles(string search) 
        {
            
            List<ProfileModel>profiles = Profiles.Where((p) => p.Name.Equals(search)).ToList();
            var viewModels = new List<ProfileIndexViewModel>();

            foreach (var profile in profiles)
            {
                var viewModel = new ProfileIndexViewModel(profile);
                viewModels.Add(viewModel);
            }

            return viewModels;
        
        }

        public ProfilesIndexViewModel FindProfiles(List<int> contactIds)
        {
            var profiles = new List<ProfileModel>();

            foreach (int contactId in contactIds) 
            {
                profiles.Add(Profiles.Where((p) => p.Id == contactId).First());
            }

            var viewModels = new ProfilesIndexViewModel();

            foreach (var profile in profiles)
            {
                var viewModel = new ProfileIndexViewModel(profile);
                viewModels.Profiles.Add(viewModel);
            }

            return viewModels;
        }

        public List<int> FindContacts(int profileId)
        {

            return Contacts.Where((p) => p.ProfileId == profileId).Select(x => x.ContactId).ToList();

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