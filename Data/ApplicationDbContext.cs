using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalChat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalChat.Data
{
    /// <summary>
    /// Database context class that used to connect to the database.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext
    {
        /// <summary>
        /// ApplicationDbContext default constructor
        /// </summary>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        /// <summary>
        /// Method that join tables Message and ChatUser(which is default identity class)
        /// with one to many relation
        /// </summary>
        /// <param name="builder">ModelBuilder type object to build tables in database using
        /// code first and with specific settings</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Message>()
                .HasOne<ChatUser>(m => m.Sender)
                .WithMany(u => u.Messages)
                .HasForeignKey(u => u.UserId);
        }

        public DbSet<Message> Messages { get; set; }
    }
}
