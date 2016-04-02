﻿using DataAccess.Model;
using System.Data.Entity;

namespace DataAccess.Context
{
    public class AppContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}