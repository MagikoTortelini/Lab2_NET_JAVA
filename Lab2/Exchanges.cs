using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Exchanges:DbContext
    {
        public DbSet<currency> Currencies { get; set; }
        public DbSet<Currency_exchange> Exchanges_rates { get; set; }

        public Exchanges()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated() ;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@"Data Source=Exch.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency_exchange>()
                .HasOne(e=>e.FromCurrency)
                .WithMany(e=>e.RatesFrom)
                .HasForeignKey(e=>e.From_Currency_id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Currency_exchange>()
                .HasOne(e => e.ToCurrency)
                .WithMany(e => e.RatesTo)
                .HasForeignKey(e => e.To_Currency_id)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
