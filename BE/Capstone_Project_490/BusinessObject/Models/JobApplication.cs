using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class JobApplication
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public int JobId { get; set; }
        public DateTime CreatedId { get; set; }
        public short? Care { get; set; }
        public string? ReasonCancel { get; set; }
        public short Status { get; set; }
        public short? IsComment { get; set; }
        public short? IsEmployerComment { get; set; }
        public int? InterviewId { get; set; }

        public virtual Candidate Applicant { get; set; } = null!;
        public virtual Interview? Interview { get; set; }
        public virtual JobDetail Job { get; set; } = null!;
    }
}
