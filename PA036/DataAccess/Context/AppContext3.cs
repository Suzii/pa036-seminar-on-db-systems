﻿using System.Data.Entity;

namespace DataAccess.Context
{
    public class AppContext3 : AppContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
