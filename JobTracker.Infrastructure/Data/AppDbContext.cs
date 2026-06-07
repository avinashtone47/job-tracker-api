using JobTracker.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<InterviewRound> InterviewRounds { get; set; }
        public DbSet<Reminder> Reminders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ApplicationUser → JobApplications (One to Many)
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.JobApplications)
                .WithOne(j => j.User)
                .HasForeignKey(j => j.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // JobApplication → InterviewRounds (One to Many)
            builder.Entity<JobApplication>()
                .HasMany(j => j.InterviewRounds)
                .WithOne(i => i.JobApplication)
                .HasForeignKey(i => i.JobApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            // JobApplication → Reminders (One to Many)
            builder.Entity<JobApplication>()
                .HasMany(j => j.Reminders)
                .WithOne(r => r.JobApplication)
                .HasForeignKey(r => r.JobApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Store enums as strings in DB (readable)
            builder.Entity<JobApplication>()
                .Property(j => j.Status)
                .HasConversion<string>();

            builder.Entity<InterviewRound>()
                .Property(i => i.Type)
                .HasConversion<string>();

            builder.Entity<InterviewRound>()
                .Property(i => i.Outcome)
                .HasConversion<string>();
        }
    }
}
