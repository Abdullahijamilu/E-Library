using System;
using System.Collections.Generic;

namespace E_Library.Models;

public partial class Discussion
{
    public int DiscussionId { get; set; }

    public string Question { get; set; } = null!;

    public string? Answer { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
