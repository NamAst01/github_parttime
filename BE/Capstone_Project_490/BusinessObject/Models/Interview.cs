using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Interview
    {
        public Interview()
        {
            JobApplications = new HashSet<JobApplication>();
        }

        public int Id { get; set; }
        public string? Start { get; set; }
        public string? End { get; set; }
        public string? Date { get; set; }
        public int? EmployerId { get; set; }

        public virtual Employer? Employer { get; set; }
        public virtual ICollection<JobApplication> JobApplications { get; set; }
    }
}
