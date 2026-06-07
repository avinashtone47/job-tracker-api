using JobTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Domain.Entities
{
    public class InterviewRound : BaseEntity
    {
        public int RoundNumber { get; set; }
        public InterviewType Type { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string? Notes { get; set; }
        public InterviewOutcome Outcome { get; set; } = InterviewOutcome.Pending;

        // Foreign Key
        public int JobApplicationId { get; set; }

        // Navigation Property
        public JobApplication JobApplication { get; set; } = null!;
    }

}
