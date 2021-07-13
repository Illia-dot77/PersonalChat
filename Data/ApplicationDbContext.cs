using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalChat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalChat.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

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
