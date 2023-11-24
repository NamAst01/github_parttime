using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int NotifyFrom { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int? AccountId { get; set; }
        public short? IsNew { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Account? Account { get; set; }
    }
}
