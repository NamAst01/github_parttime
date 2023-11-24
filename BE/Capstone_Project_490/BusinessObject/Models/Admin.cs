using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string Address { get; set; } = null!;
        public int? AccountId { get; set; }
        public string Phone { get; set; } = null!;
        public string? Image { get; set; }
        public short? Status { get; set; }

        public virtual Account? Account { get; set; }
    }
}
