using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.DTOs
{
    public class DashboardDto
    {
        public int TotalApplications { get; set; }
        public int Applied { get; set; }
        public int Interviews { get; set; }
        public int Offers { get; set; }
        public int Rejected { get; set; }
        public double SuccessRate { get; set; }
        public List<WeeklyApplicationDto> WeeklyData { get; set; } = new();
        public List<JobResponseDto> RecentApplications { get; set; } = new();
    }

    public class WeeklyApplicationDto
    {
        public string Week { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
