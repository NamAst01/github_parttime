﻿using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class JobDetail
    {
        public JobDetail()
        {
            JobApplications = new HashSet<JobApplication>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int EmployerId { get; set; }
        public string Description { get; set; } = null!;
        public string Salary { get; set; } = null!;
        public string Location { get; set; } = null!;
        public DateTime Deadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? TypeSalary { get; set; }
        public string? JobTime { get; set; }
        public short? Status { get; set; }
        public int? JobTypeId { get; set; }
        public string? Gender { get; set; }
        public string? Experient { get; set; }
        public string? Company { get; set; }
        public int? Rolecompany { get; set; }
        public int? NumberApply { get; set; }
        public int? TypeJob { get; set; }
        public string? Daywork { get; set; }
        public string? Note { get; set; }
        public int? Dob { get; set; }
        public int? Toage { get; set; }
        public string? Levellearn { get; set; }
        public int? Fromage { get; set; }
        public string? Experience { get; set; }
        public string? Welfare { get; set; }
        public string? Moredesciption { get; set; }
        public string? Typename { get; set; }
        public string? Agreesalary { get; set; }
        public int? Checktypejob { get; set; }
        public string? Reasonreject { get; set; }

        public virtual Employer Employer { get; set; } = null!;
        public virtual JobType? JobType { get; set; }
        public virtual ICollection<JobApplication> JobApplications { get; set; }
    }
}