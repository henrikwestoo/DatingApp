using DatingApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DatingApp.Repositories
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ProfileModel> Profiles { get; set; }

        public DbSet<ContactModel> Contacts { get; set; }
        
        public DbSet<PostModel> Posts { get; set; }

        public DbSet<VisitorModel> Visitors { get; set; }
        
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