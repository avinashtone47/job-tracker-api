using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.DTOs
{
    public class JobResponseDto
    {
        public int Id { get; set; }
        public string Company { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string? SalaryRange { get; set; }
        public string? JobUrl { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime AppliedDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int InterviewRoundsCount { get; set; }
    }
}
