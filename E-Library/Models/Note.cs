using System;
using System.Collections.Generic;

namespace E_Library.Models;

public partial class Note
{
    public int NoteId { get; set; }

    public string Content { get; set; } = null!;

    public int UserId { get; set; }

    public int BookId { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
