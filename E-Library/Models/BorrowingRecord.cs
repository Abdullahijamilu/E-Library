using System;
using System.Collections.Generic;

namespace E_Library.Models;

public partial class BorrowingRecord
{
    public Guid BorrowingRecordId { get; set; }

    public string UserId { get; set; }

    public Guid BookId { get; set; }

    public DateTime BorrowDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
