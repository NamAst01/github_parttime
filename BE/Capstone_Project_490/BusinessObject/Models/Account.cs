using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Account
    {
        public Account()
        {
            Admins = new HashSet<Admin>();
            Candidates = new HashSet<Candidate>();
            Comments = new HashSet<Comment>();
            Employers = new HashSet<Employer>();
            Notifications = new HashSet<Notification>();
            Reports = new HashSet<Report>();
        }

        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime LastLoginAt { get; set; }
        public short Status { get; set; }
        public string? Gender { get; set; }
        public string? ReasonBaned { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Admin> Admins { get; set; }
        public virtual ICollection<Candidate> Candidates { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Employer> Employers { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
