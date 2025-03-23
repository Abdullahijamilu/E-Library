using System;
using System.Collections.Generic;

namespace E_Library.Models;

public partial class Note
{
    public Guid NoteId { get; set; }

    public string Content { get; set; } = null!;

    public string UserId { get; set; }

    public Guid BookId { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
