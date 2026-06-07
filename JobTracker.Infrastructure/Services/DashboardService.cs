using JobTracker.Application.DTOs;
using JobTracker.Application.Interfaces;
using JobTracker.Domain.Enums;
using JobTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> GetDashboardAsync(string userId)
        {
            var jobs = await _context.JobApplications
                .Where(j => j.UserId == userId)
                .ToListAsync();

            var total = jobs.Count;
            var applied = jobs.Count(j => j.Status == JobStatus.Applied);
            var interviews = jobs.Count(j => j.Status == JobStatus.Interview);
            var offers = jobs.Count(j => j.Status == JobStatus.Offer);
            var rejected = jobs.Count(j => j.Status == JobStatus.Rejected);

            // Success rate = Offers / Total * 100
            var successRate = total > 0
                ? Math.Round((double)offers / total * 100, 1)
                : 0;

            // Weekly data — last 6 weeks
            var weeklyData = new List<WeeklyApplicationDto>();
            for (int i = 5; i >= 0; i--)
            {
                var weekStart = DateTime.UtcNow
                    .AddDays(-(int)DateTime.UtcNow.DayOfWeek)
                    .AddDays(-7 * i)
                    .Date;
                var weekEnd = weekStart.AddDays(7);

                var count = jobs.Count(j =>
                    j.CreatedAt >= weekStart &&
                    j.CreatedAt < weekEnd);

                weeklyData.Add(new WeeklyApplicationDto
                {
                    Week = weekStart.ToString("MMM dd"),
                    Count = count
                });
            }

            // Recent 5 applications
            var recent = jobs
                .OrderByDescending(j => j.CreatedAt)
                .Take(5)
                .Select(j => new JobResponseDto
                {
                    Id = j.Id,
                    Company = j.Company,
                    Role = j.Role,
                    Location = j.Location,
                    SalaryRange = j.SalaryRange,
                    Status = j.Status.ToString(),
                    AppliedDate = j.AppliedDate,
                    CreatedAt = j.CreatedAt
                })
                .ToList();

            return new DashboardDto
            {
                TotalApplications = total,
                Applied = applied,
                Interviews = interviews,
                Offers = offers,
                Rejected = rejected,
                SuccessRate = successRate,
                WeeklyData = weeklyData,
                RecentApplications = recent
            };
        }
    }
}
