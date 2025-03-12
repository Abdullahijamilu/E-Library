using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Models;

public partial class ElibraryContext : DbContext
{
    public ElibraryContext()
    {
    }

    public ElibraryContext(DbContextOptions<ElibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BorrowingRecord> BorrowingRecords { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Discussion> Discussions { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-O4AVJKA;Database=Elibrary;User Id=Abdullahi Onimisi;Password=Abdul131;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C207AD0813C1");

            entity.Property(e => e.Author)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FileUrl)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Books_Category");
        });

        modelBuilder.Entity<BorrowingRecord>(entity =>
        {
            entity.HasKey(e => e.BorrowingRecordId).HasName("PK__Borrowin__D7C4571CA3D388D2");

            entity.ToTable("BorrowingRecord");

            entity.Property(e => e.BorrowDate).HasColumnType("datetime");
            entity.Property(e => e.ReturnDate).HasColumnType("datetime");

            entity.HasOne(d => d.Book).WithMany(p => p.BorrowingRecords)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BorrowingRecord_Book");

            entity.HasOne(d => d.User).WithMany(p => p.BorrowingRecords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BorrowingRecord_User");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0BBBF8F6A6");

            entity.ToTable("Category");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Discussion>(entity =>
        {
            entity.HasKey(e => e.DiscussionId).HasName("PK__Discussi__7E8E39C0313887A9");

            entity.ToTable("Discussion");

            entity.Property(e => e.Answer)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Question)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Book).WithMany(p => p.Discussions)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Discussion_Book");

            entity.HasOne(d => d.User).WithMany(p => p.Discussions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Discussion_User");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("PK__Note__EACE355F3DBDA532");

            entity.ToTable("Note");

            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Book).WithMany(p => p.Notes)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Note_Book");

            entity.HasOne(d => d.User).WithMany(p => p.Notes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Note_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4CE5B78A83");

            entity.ToTable("User");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UniversityId)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
