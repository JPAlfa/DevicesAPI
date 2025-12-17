using Devices.Domain.Entities;
using Devices.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Infrastructure.Persistence
{
    public sealed class DevicesDbContext : DbContext
    {
        public DevicesDbContext(DbContextOptions<DevicesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Devices => Set<Device>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureDevice(modelBuilder);
        }

        private static void ConfigureDevice(ModelBuilder modelBuilder)
        {
            var device = modelBuilder.Entity<Device>();

            device.ToTable("Devices");

            device.HasKey(d => d.Id);

            device.Property(d => d.Id)
                  .ValueGeneratedNever();
            device.Property(d => d.Name)
                  .IsRequired()
                  .HasMaxLength(200);
            device.Property(d => d.Brand)
                  .IsRequired()
                  .HasMaxLength(200);
            device.Property(d => d.CreationTime)
                  .IsRequired();
            device.Property(d => d.State)
                  .HasConversion(
                      state => state.Value,                 
                      value => DeviceState.From(value)      
                  )
                  .IsRequired()
                  .HasMaxLength(20);

            device.HasIndex(d => d.Brand);
            device.HasIndex(d => d.State);
        }
    }
}
