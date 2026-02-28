using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    // context: db tabloları ile proje classlarını bağlamak.
    public class NorthwindContext:DbContext
    {
        //HANGİ VERİTABANI OLDUĞUNU SÖYLEYEN KOD 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\sqlexpress;Database=Northwind;Trusted_Connection=true;TrustServerCertificate=true");
        }
        // HANGİ TABLOYA NE KARŞILIĞI GELECEK OLDUĞUNU SÖYLEYEN KOD.
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }


    }
}
