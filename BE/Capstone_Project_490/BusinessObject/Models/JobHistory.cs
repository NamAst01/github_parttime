using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class JobHistory
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string JobId { get; set; } = null!;
        public string IsComment { get; set; } = null!;
        public string Status { get; set; } = null!;

        public virtual Candidate Candidate { get; set; } = null!;
    }
}
