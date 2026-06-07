using JobTracker.Application.DTOs;
using JobTracker.Application.Interfaces;
using JobTracker.Domain.Entities;
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
    public class JobService : IJobService
    {
        private readonly AppDbContext _context;

        public JobService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResultDto<JobResponseDto>> GetAllAsync(
            string userId,
            int pageNumber,
            int pageSize,
            string? search,
            JobStatus? status)
        {
            var query = _context.JobApplications
                .Where(j => j.UserId == userId)
                .AsQueryable();

            // Search filter
            if (!string.IsNullOrEmpty(search))
                query = query.Where(j =>
                    j.Company.Contains(search) ||
                    j.Role.Contains(search));

            // Status filter
            if (status.HasValue)
                query = query.Where(j => j.Status == status.Value);

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(j => j.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(j => new JobResponseDto
                {
                    Id = j.Id,
                    Company = j.Company,
                    Role = j.Role,
                    Location = j.Location,
                    SalaryRange = j.SalaryRange,
                    JobUrl = j.JobUrl,
                    Notes = j.Notes,
                    Status = j.Status.ToString(),
                    AppliedDate = j.AppliedDate,
                    CreatedAt = j.CreatedAt,
                    InterviewRoundsCount = j.InterviewRounds.Count
                })
                .ToListAsync();

            return new PagedResultDto<JobResponseDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<JobResponseDto> GetByIdAsync(int id, string userId)
        {
            var job = await _context.JobApplications
                .Include(j => j.InterviewRounds)
                .FirstOrDefaultAsync(j => j.Id == id && j.UserId == userId);

            if (job == null)
                throw new Exception("Job application not found.");

            return MapToDto(job);
        }

        public async Task<JobResponseDto> CreateAsync(
            CreateJobDto dto, string userId)
        {
            var job = new JobApplication
            {
                Company = dto.Company,
                Role = dto.Role,
                Location = dto.Location,
                SalaryRange = dto.SalaryRange,
                JobUrl = dto.JobUrl,
                Notes = dto.Notes,
                AppliedDate = dto.AppliedDate,
                UserId = userId,
                Status = JobStatus.Applied
            };

            _context.JobApplications.Add(job);
            await _context.SaveChangesAsync();

            return MapToDto(job);
        }

        public async Task<JobResponseDto> UpdateAsync(
            int id, UpdateJobDto dto, string userId)
        {
            var job = await _context.JobApplications
                .FirstOrDefaultAsync(j => j.Id == id && j.UserId == userId);

            if (job == null)
                throw new Exception("Job application not found.");

            job.Company = dto.Company;
            job.Role = dto.Role;
            job.Location = dto.Location;
            job.SalaryRange = dto.SalaryRange;
            job.JobUrl = dto.JobUrl;
            job.Notes = dto.Notes;
            job.Status = dto.Status;
            job.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return MapToDto(job);
        }

        public async Task DeleteAsync(int id, string userId)
        {
            var job = await _context.JobApplications
                .FirstOrDefaultAsync(j => j.Id == id && j.UserId == userId);

            if (job == null)
                throw new Exception("Job application not found.");

            _context.JobApplications.Remove(job);
            await _context.SaveChangesAsync();
        }

        private JobResponseDto MapToDto(JobApplication job)
        {
            return new JobResponseDto
            {
                Id = job.Id,
                Company = job.Company,
                Role = job.Role,
                Location = job.Location,
                SalaryRange = job.SalaryRange,
                JobUrl = job.JobUrl,
                Notes = job.Notes,
                Status = job.Status.ToString(),
                AppliedDate = job.AppliedDate,
                CreatedAt = job.CreatedAt,
                InterviewRoundsCount = job.InterviewRounds?.Count ?? 0
            };
        }
    }
}
