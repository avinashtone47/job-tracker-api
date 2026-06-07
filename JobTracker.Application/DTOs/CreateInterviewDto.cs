using JobTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.DTOs
{
    public class CreateInterviewDto
    {
        public int RoundNumber { get; set; }
        public InterviewType Type { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string? Notes { get; set; }
    }
}
