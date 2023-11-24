using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessObject.Models
{
    public partial class ParttimeJobContext : DbContext
    {
        public ParttimeJobContext()
        {
        }

        public ParttimeJobContext(DbContextOptions<ParttimeJobContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Candidate> Candidates { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Employer> Employers { get; set; } = null!;
        public virtual DbSet<Interview> Interviews { get; set; } = null!;
        public virtual DbSet<JobApplication> JobApplications { get; set; } = null!;
        public virtual DbSet<JobDetail> JobDetails { get; set; } = null!;
        public virtual DbSet<JobHistory> JobHistories { get; set; } = null!;
        public virtual DbSet<JobType> JobTypes { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server = parttimejob.database.windows.net; database = parttime_job_db; uid=g84; pwd=parttimejob12@");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Email)
                    .HasMaxLength(1000)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(1000)
                    .HasColumnName("full_name");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1000)
                    .HasColumnName("gender");

                //entity.Property(e => e.IsBaned).HasColumnName("isBaned");

                entity.Property(e => e.LastLoginAt)
                    .HasColumnType("datetime")
                    .HasColumnName("last_login_at");

                entity.Property(e => e.Password)
                    .HasMaxLength(1000)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_account_role");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admin");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .HasColumnName("address");

                entity.Property(e => e.Image)
                    .HasMaxLength(500)
                    .HasColumnName("image");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Admins)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_admin_account");
            });

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.ToTable("candidate");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(1000)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .HasColumnName("city");

                entity.Property(e => e.Distric)
                    .HasMaxLength(100)
                    .HasColumnName("distric");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("dob");

                entity.Property(e => e.ExpectAddress)
                    .HasMaxLength(100)
                    .HasColumnName("expectAddress");

                entity.Property(e => e.Image)
                    .HasMaxLength(4000)
                    .HasColumnName("image");

                entity.Property(e => e.IsReport).HasColumnName("isReport");

                entity.Property(e => e.Phone)
                    .HasMaxLength(1000)
                    .HasColumnName("phone");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Candidates)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_candidate_account");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(1000)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Content)
                    .HasMaxLength(1000)
                    .HasColumnName("content");

                entity.Property(e => e.FromAccountId).HasColumnName("from_account_id");

                entity.Property(e => e.Start).HasColumnName("start");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Time)
                    .HasMaxLength(100)
                    .HasColumnName("time");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_comment_account");
            });

            modelBuilder.Entity<Employer>(entity =>
            {
                entity.ToTable("employer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Addressdetail)
                    .HasMaxLength(100)
                    .HasColumnName("addressdetail");

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .HasColumnName("city");

                entity.Property(e => e.Company)
                    .HasMaxLength(100)
                    .HasColumnName("company");

                entity.Property(e => e.CompanyAddress)
                    .HasMaxLength(1000)
                    .HasColumnName("company_address");

                entity.Property(e => e.Distric)
                    .HasMaxLength(100)
                    .HasColumnName("distric");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("dob");

                entity.Property(e => e.Expectaddress)
                    .HasMaxLength(100)
                    .HasColumnName("expectaddress");

                entity.Property(e => e.Image)
                    .HasMaxLength(1000)
                    .HasColumnName("image");

                entity.Property(e => e.IsReport).HasColumnName("isReport");

                entity.Property(e => e.Phone)
                    .HasMaxLength(1000)
                    .HasColumnName("phone");

                entity.Property(e => e.Position)
                    .HasMaxLength(1000)
                    .HasColumnName("position");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Employers)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employer_account");
            });

            modelBuilder.Entity<Interview>(entity =>
            {
                entity.ToTable("interview");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasMaxLength(50)
                    .HasColumnName("date");

                entity.Property(e => e.EmployerId).HasColumnName("employerId");

                entity.Property(e => e.End)
                    .HasMaxLength(50)
                    .HasColumnName("end");

                entity.Property(e => e.Start)
                    .HasMaxLength(50)
                    .HasColumnName("start");

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.Interviews)
                    .HasForeignKey(d => d.EmployerId)
                    .HasConstraintName("FK_interview_employer");
            });

            modelBuilder.Entity<JobApplication>(entity =>
            {
                entity.ToTable("job_application");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApplicantId).HasColumnName("applicant_id");

                entity.Property(e => e.Care).HasColumnName("care");

                entity.Property(e => e.CreatedId)
                    .HasColumnType("datetime")
                    .HasColumnName("created_id");

                entity.Property(e => e.InterviewId).HasColumnName("interviewId");

                entity.Property(e => e.IsComment).HasColumnName("isComment");

                entity.Property(e => e.IsEmployerComment).HasColumnName("isEmployerComment");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.ReasonCancel)
                    .HasMaxLength(1000)
                    .HasColumnName("reasonCancel");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Applicant)
                    .WithMany(p => p.JobApplications)
                    .HasForeignKey(d => d.ApplicantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__job_appli__appli__52593CB8");

                entity.HasOne(d => d.Interview)
                    .WithMany(p => p.JobApplications)
                    .HasForeignKey(d => d.InterviewId)
                    .HasConstraintName("FK_job_application_interview");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobApplications)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__job_appli__job_i__5070F446");
            });

            modelBuilder.Entity<JobDetail>(entity =>
            {
                entity.ToTable("job_detail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Agreesalary)
                    .HasMaxLength(100)
                    .HasColumnName("agreesalary");

                entity.Property(e => e.Checktypejob).HasColumnName("checktypejob");

                entity.Property(e => e.Company).HasColumnName("company");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Daywork)
                    .HasMaxLength(50)
                    .HasColumnName("daywork");

                entity.Property(e => e.Deadline)
                    .HasColumnType("datetime")
                    .HasColumnName("deadline");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasColumnName("description");

                entity.Property(e => e.Dob).HasColumnName("dob");

                entity.Property(e => e.EmployerId).HasColumnName("employer_id");

                entity.Property(e => e.Experience)
                    .HasMaxLength(100)
                    .HasColumnName("experience");

                entity.Property(e => e.Experient)
                    .HasMaxLength(50)
                    .HasColumnName("experient");

                entity.Property(e => e.Fromage).HasColumnName("fromage");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .HasColumnName("gender");

                entity.Property(e => e.JobTime)
                    .HasMaxLength(1000)
                    .HasColumnName("job_time");

                entity.Property(e => e.JobTypeId).HasColumnName("job_type_id");

                entity.Property(e => e.Levellearn)
                    .HasMaxLength(100)
                    .HasColumnName("levellearn");

                entity.Property(e => e.Location)
                    .HasMaxLength(1000)
                    .HasColumnName("location");

                entity.Property(e => e.Moredesciption)
                    .HasMaxLength(1000)
                    .HasColumnName("moredesciption");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.NumberApply).HasColumnName("numberApply");

                entity.Property(e => e.Rolecompany).HasColumnName("rolecompany");

                entity.Property(e => e.Salary)
                    .HasMaxLength(100)
                    .HasColumnName("salary");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(1000)
                    .HasColumnName("title");

                entity.Property(e => e.Toage).HasColumnName("toage");

                entity.Property(e => e.TypeJob).HasColumnName("typeJob");

                entity.Property(e => e.TypeSalary)
                    .HasMaxLength(1000)
                    .HasColumnName("type_salary");

                entity.Property(e => e.Typename)
                    .HasMaxLength(100)
                    .HasColumnName("typename");

                entity.Property(e => e.Welfare)
                    .HasMaxLength(100)
                    .HasColumnName("welfare");

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.JobDetails)
                    .HasForeignKey(d => d.EmployerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__job_detai__emplo__5165187F");

                entity.HasOne(d => d.JobType)
                    .WithMany(p => p.JobDetails)
                    .HasForeignKey(d => d.JobTypeId)
                    .HasConstraintName("FK_job_detail_job_type");
            });

            modelBuilder.Entity<JobHistory>(entity =>
            {
                entity.ToTable("job_history");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CandidateId).HasColumnName("candidate_id");

                entity.Property(e => e.IsComment)
                    .HasMaxLength(1000)
                    .HasColumnName("isComment");

                entity.Property(e => e.JobId)
                    .HasMaxLength(1000)
                    .HasColumnName("job_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(1000)
                    .HasColumnName("status");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.JobHistories)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__job_histo__appli__5629CD9C");
            });

            modelBuilder.Entity<JobType>(entity =>
            {
                entity.ToTable("job_type");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.NameType)
                    .HasMaxLength(400)
                    .HasColumnName("name_type");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.JobTypes)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_job_type_category");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("notification");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Content)
                    .HasMaxLength(1000)
                    .HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.IsNew).HasColumnName("isNew");

                entity.Property(e => e.NotifyFrom).HasColumnName("notify_from");

                entity.Property(e => e.Title)
                    .HasMaxLength(1000)
                    .HasColumnName("title");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_notification_account");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("report");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Content)
                    .HasMaxLength(1000)
                    .HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Title)
                    .HasMaxLength(1000)
                    .HasColumnName("title");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_report_account");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("role_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
