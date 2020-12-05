using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using QrCodeGeneratorApp.dto;

#nullable disable

namespace QrCodeGeneratorApp.Model
{
    public partial class TireInventoryContext : DbContext
    {
        public TireInventoryContext()
        {
        }

        public TireInventoryContext(DbContextOptions<TireInventoryContext> options)
            : base(options)
        {
        }      
        public virtual DbSet<Product> products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
