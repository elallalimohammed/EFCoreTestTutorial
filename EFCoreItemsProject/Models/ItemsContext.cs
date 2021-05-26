using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreItemsProject.Models
{
    public partial class ItemsContext : DbContext
    {


        public ItemsContext()
        {
        }
        public ItemsContext(DbContextOptions<ItemsContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer(" Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MovieStudioDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
           }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(
            b =>
            {
                b.Property("_id");
                b.HasKey("_id");
                b.Property(e => e.Name);
                b.HasMany(e => e.Tags).WithOne().IsRequired();
            });
                        modelBuilder.Entity<Tag>(
                b =>
                {
                    b.Property("_id");
                    b.HasKey("_id");
                    b.Property(e => e.Label);
                });
        }


        
    }
    }

