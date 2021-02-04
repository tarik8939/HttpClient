using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Context : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
            //Database.EnsureCreated();
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Book>().HasKey(k => k.BookID);
            //modelBuilder.Entity<Book>().Property(p => p.Name).HasColumnName("BookName");
            //modelBuilder.Entity<Book>().Property(p => p.ID).HasColumnName("BookID");
            //modelBuilder.Entity<Book>().Property(p => p.Count).HasColumnName("BookCount");

            modelBuilder.Entity<Book>().Property(p => p.BookName).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Book>().Property(p => p.BookCount).IsRequired();
            modelBuilder.Entity<Book>().Property(p => p.BookID).IsRequired();


            base.OnModelCreating(modelBuilder);
        }
    }
}
