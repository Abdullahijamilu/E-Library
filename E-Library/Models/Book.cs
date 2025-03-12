using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Library.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public int Year { get; set; }

    public string Description { get; set; } = null!;

    public string? FileUrl { get; set; }

    public int? CategoryId { get; set; }

    public virtual ICollection<BorrowingRecord> BorrowingRecords { get; set; } = new List<BorrowingRecord>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Discussion> Discussions { get; set; } = new List<Discussion>();

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public Book(string title, string description, string fileUrl, int year)
    {
        Title = title;
        Description = description;
        FileUrl = fileUrl;
        Year = year;
    }

    public Book(string title, string author, string description)
    {
        Title = title;
        Author = author;
        Description = description;
    }
}
