using JobTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Domain.Entities
{
    public class JobApplication : BaseEntity
    {
        public string Company { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string? SalaryRange { get; set; }
        public string? JobUrl { get; set; }
        public string? Notes { get; set; }
        public JobStatus Status { get; set; } = JobStatus.Applied;
        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;

        // Foreign Key
        public string UserId { get; set; } = string.Empty;

        // Navigation Properties
        public ApplicationUser User { get; set; } = null!;
        public ICollection<InterviewRound> InterviewRounds { get; set; }
            = new List<InterviewRound>();
        public ICollection<Reminder> Reminders { get; set; }
            = new List<Reminder>();
    }
}
