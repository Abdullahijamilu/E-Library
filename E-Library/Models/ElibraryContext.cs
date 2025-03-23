using System;
using System.Collections.Generic;
using E_Library.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public partial class ElibraryContext : IdentityDbContext<User>
{
    public ElibraryContext(DbContextOptions<ElibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<BorrowingRecord> BorrowingRecords { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Discussion> Discussions { get; set; }
    public virtual DbSet<Note> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Ensure Identity tables are created

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId);
            entity.Property(e => e.Author).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(100).IsUnicode(false);
            
        });

        modelBuilder.Entity<BorrowingRecord>(entity =>
        {
            base.OnModelCreating(modelBuilder);
            entity.HasKey(e => e.BorrowingRecordId);
            entity.HasOne(d => d.Book).WithMany(p => p.BorrowingRecords)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.User).WithMany(p => p.BorrowingRecords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("AspNetUsers"); // Use Identity's default table name
            entity.Property(e => e.Name).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.Role).HasMaxLength(50).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
