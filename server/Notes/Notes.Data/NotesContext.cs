using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Notes.Model.Entities;

namespace Notes.Data
{
    public class NotesContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public NotesContext(DbContextOptions<NotesContext> options) : base(options)
        {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            ConfigureModelBuilderForUser(modelBuilder);
            ConfigureModelBuilderForNote(modelBuilder);
        }

        void ConfigureModelBuilderForUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>()
                .Property(user => user.Username)
                .HasMaxLength(60)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.Email)
                .HasMaxLength(60)
                .IsRequired();
        }

        void ConfigureModelBuilderForNote(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().ToTable("Note");
            modelBuilder.Entity<Note>()
                .Property(s => s.Title)
                .HasMaxLength(60);

            modelBuilder.Entity<Note>()
                .Property(s => s.OwnerId)
                .IsRequired();

            modelBuilder.Entity<Note>()
                .HasOne(s => s.Owner)
                .WithMany(u => u.Notes)
                .HasForeignKey(s => s.OwnerId);
        }
    }
}
