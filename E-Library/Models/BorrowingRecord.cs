using System;
using System.Collections.Generic;

namespace E_Library.Models;

public partial class BorrowingRecord
{
    public int BorrowingRecordId { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }

    public DateTime BorrowDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
