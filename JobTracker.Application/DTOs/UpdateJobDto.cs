using JobTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.DTOs
{
    public class UpdateJobDto
    {
        public string Company { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string? SalaryRange { get; set; }
        public string? JobUrl { get; set; }
        public string? Notes { get; set; }
        public JobStatus Status { get; set; }
    }
}
