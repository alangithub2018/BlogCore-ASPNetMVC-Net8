﻿using BlogCore_ASPNetMVC_Net8.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogCore_ASPNetMVC_Net8.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add here all of the needed models to use for the application
        public DbSet<Category> Category { get; set; }
        public DbSet<Article> Article { get; set; }

        public DbSet<Slider> Slider { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
