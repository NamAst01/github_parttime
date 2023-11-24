using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public short Start { get; set; }
        public string? Content { get; set; }
        public short Status { get; set; }
        public string? Time { get; set; }
        public int? FromAccountId { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
