using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace E_Library.Models;

public partial class User : IdentityUser
{
    //public string UserId { get; set; }

    public string Name { get; set; } = null!;

    public string UniversityId { get; set; } = null!;

    public string Role { get; set; } = null!;
    public string Password { get; set; } = null!;

    public virtual ICollection<BorrowingRecord> BorrowingRecords { get; set; } = new List<BorrowingRecord>();

    public virtual ICollection<Discussion> Discussions { get; set; } = new List<Discussion>();

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public User(string name, string universityid, string role, string password)
    {
        Name = name;
        UniversityId = universityid;
        Role = role;
        Password = password;
    }
    public User()
    {
    }
}
