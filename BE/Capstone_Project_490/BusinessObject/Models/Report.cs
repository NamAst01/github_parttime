using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Report
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Content { get; set; } = null!;
        public int? AccountId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Account? Account { get; set; }
    }
}
