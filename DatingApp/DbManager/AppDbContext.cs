using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DatingApp.DbManager
{
    public class AppDbContext : DbContext
    {
        public DbSet <ProfileModels> Profiles { get; set; }

        public AppDbContext() : base("DefaultConnection")
        {

        }

    }
}