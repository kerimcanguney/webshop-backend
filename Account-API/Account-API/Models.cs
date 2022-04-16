using System;
using System.Collections.Generic;
using Account_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Account_API
{
    public class WsContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Password> Passwords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Password>()
                .HasOne(b => b.Account)
                .WithOne(i => i.Password)
                .HasForeignKey<Password>(b => b.Account_Id);

            modelBuilder.Entity<IpAdress>()
                .HasOne(b => b.Account)
                .WithOne(i => i.IpAdress)
                .HasForeignKey<IpAdress>(b => b.Account_Id);

            modelBuilder.Entity<Transaction>()
                .HasOne(b => b.Account)
                .WithOne(i => i.Transaction)
                .HasForeignKey<Transaction>(b => b.Account_Id);


        }

        public string DbPath { get; }

        public WsContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "WSData.db");
        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=WSData;Trusted_Connection=True;");
    }
}