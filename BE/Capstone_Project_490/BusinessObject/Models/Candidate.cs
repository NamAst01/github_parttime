using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Candidate
    {
        public Candidate()
        {
            JobApplications = new HashSet<JobApplication>();
            JobHistories = new HashSet<JobHistory>();
        }

        public int Id { get; set; }
        public string Phone { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string Address { get; set; } = null!;
        public string Image { get; set; } = null!;
        public short? IsReport { get; set; }
        public int AccountId { get; set; }
        public short Status { get; set; }
        public string? City { get; set; }
        public string? Distric { get; set; }
        public string? Skill { get; set; }
        public string? ExpectAddress { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<JobApplication> JobApplications { get; set; }
        public virtual ICollection<JobHistory> JobHistories { get; set; }
    }
}
