using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VHAPI.Models
{
    public class WebContext : DbContext
    {
        public WebContext(DbContextOptions<WebContext> options) : base(options)
        { }

        public DbSet<Toast> Toasts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<GalleryItem> GalleryItems { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GalleryItem>()
                                .HasOne(x => x.Service)
                                .WithMany(x => x.GalleryItems)
                                .HasForeignKey(x => x.ServiceId);
        }
    }
}
