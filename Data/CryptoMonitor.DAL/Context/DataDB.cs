using CryptoMonitor.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoMonitor.DAL.Context
{
    public class DataDB : DbContext
    {
        public DbSet<DataValue> Values { get; set; }

        public DbSet<DataSource> Sources { get; set; }

        public DataDB(DbContextOptions<DataDB> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DataSource>()
                .HasMany<DataValue>()
                .WithOne(v => v.Source)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<DataSource>()
            //    .Property(source => source.Name)
            //    .IsRequired();

            //modelBuilder.Entity<DataSource>()
            //    .HasIndex(source => source.Name)
            //    .IsUnique(true);
        }
    }
}
