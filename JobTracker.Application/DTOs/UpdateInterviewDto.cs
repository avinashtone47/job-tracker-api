using JobTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.DTOs
{
    public class UpdateInterviewDto
    {
        public InterviewType Type { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string? Notes { get; set; }
        public InterviewOutcome Outcome { get; set; }
    }
}
