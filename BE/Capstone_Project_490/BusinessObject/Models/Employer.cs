using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Employer
    {
        public Employer()
        {
            Interviews = new HashSet<Interview>();
            JobDetails = new HashSet<JobDetail>();
        }

        public int Id { get; set; }
        public int AccountId { get; set; }
        public string? Company { get; set; }
        public string Phone { get; set; } = null!;
        public string CompanyAddress { get; set; } = null!;
        public string? Position { get; set; }
        public short? IsReport { get; set; }
        public string Image { get; set; } = null!;
        public short Status { get; set; }
        public DateTime Dob { get; set; }
        public string? City { get; set; }
        public string? Distric { get; set; }
        public string? Addressdetail { get; set; }
        public string? Expectaddress { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<Interview> Interviews { get; set; }
        public virtual ICollection<JobDetail> JobDetails { get; set; }
    }
}
