using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class JobType
    {
        public JobType()
        {
            JobDetails = new HashSet<JobDetail>();
        }

        public int Id { get; set; }
        public string? NameType { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<JobDetail> JobDetails { get; set; }
    }
}
