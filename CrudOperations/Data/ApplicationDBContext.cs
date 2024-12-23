﻿using Microsoft.EntityFrameworkCore;

namespace CrudOperations.Data
{
    public class ApplicationDBContext:DbContext
    {


        public ApplicationDBContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("products");
        }
    }
}
