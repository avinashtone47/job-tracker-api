using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.DTOs
{
    public class InterviewResponseDto
    {
        public int Id { get; set; }
        public int RoundNumber { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime ScheduledDate { get; set; }
        public string? Notes { get; set; }
        public string Outcome { get; set; } = string.Empty;
        public int JobApplicationId { get; set; }
        public string Company { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
