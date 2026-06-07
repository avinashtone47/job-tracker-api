using JobTracker.Application.DTOs;
using JobTracker.Application.Interfaces;
using JobTracker.Domain.Entities;
using JobTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Infrastructure.Services
{
    public class InterviewService : IInterviewService
    {
        private readonly AppDbContext _context;

        public InterviewService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<InterviewResponseDto>> GetAllByJobIdAsync(
            int jobId, string userId)
        {
            // Verify job belongs to user
            var job = await _context.JobApplications
                .FirstOrDefaultAsync(j => j.Id == jobId
                    && j.UserId == userId);

            if (job == null)
                throw new Exception("Job application not found.");

            return await _context.InterviewRounds
                .Where(i => i.JobApplicationId == jobId)
                .OrderBy(i => i.RoundNumber)
                .Select(i => new InterviewResponseDto
                {
                    Id = i.Id,
                    RoundNumber = i.RoundNumber,
                    Type = i.Type.ToString(),
                    ScheduledDate = i.ScheduledDate,
                    Notes = i.Notes,
                    Outcome = i.Outcome.ToString(),
                    JobApplicationId = i.JobApplicationId,
                    Company = i.JobApplication.Company,
                    Role = i.JobApplication.Role
                })
                .ToListAsync();
        }

        public async Task<InterviewResponseDto> GetByIdAsync(
            int id, string userId)
        {
            var interview = await _context.InterviewRounds
                .Include(i => i.JobApplication)
                .FirstOrDefaultAsync(i => i.Id == id
                    && i.JobApplication.UserId == userId);

            if (interview == null)
                throw new Exception("Interview round not found.");

            return MapToDto(interview);
        }

        public async Task<InterviewResponseDto> CreateAsync(
            int jobId, CreateInterviewDto dto, string userId)
        {
            // Verify job belongs to user
            var job = await _context.JobApplications
                .FirstOrDefaultAsync(j => j.Id == jobId
                    && j.UserId == userId);

            if (job == null)
                throw new Exception("Job application not found.");

            var interview = new InterviewRound
            {
                RoundNumber = dto.RoundNumber,
                Type = dto.Type,
                ScheduledDate = dto.ScheduledDate,
                Notes = dto.Notes,
                JobApplicationId = jobId,
                Outcome = Domain.Enums.InterviewOutcome.Pending
            };

            _context.InterviewRounds.Add(interview);

            // Update job status to Interview
            job.Status = Domain.Enums.JobStatus.Interview;
            job.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Reload with navigation property
            await _context.Entry(interview)
                .Reference(i => i.JobApplication)
                .LoadAsync();

            return MapToDto(interview);
        }

        public async Task<InterviewResponseDto> UpdateAsync(
            int id, UpdateInterviewDto dto, string userId)
        {
            var interview = await _context.InterviewRounds
                .Include(i => i.JobApplication)
                .FirstOrDefaultAsync(i => i.Id == id
                    && i.JobApplication.UserId == userId);

            if (interview == null)
                throw new Exception("Interview round not found.");

            interview.Type = dto.Type;
            interview.ScheduledDate = dto.ScheduledDate;
            interview.Notes = dto.Notes;
            interview.Outcome = dto.Outcome;
            interview.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return MapToDto(interview);
        }

        public async Task DeleteAsync(int id, string userId)
        {
            var interview = await _context.InterviewRounds
                .Include(i => i.JobApplication)
                .FirstOrDefaultAsync(i => i.Id == id
                    && i.JobApplication.UserId == userId);

            if (interview == null)
                throw new Exception("Interview round not found.");

            _context.InterviewRounds.Remove(interview);
            await _context.SaveChangesAsync();
        }

        private InterviewResponseDto MapToDto(InterviewRound interview)
        {
            return new InterviewResponseDto
            {
                Id = interview.Id,
                RoundNumber = interview.RoundNumber,
                Type = interview.Type.ToString(),
                ScheduledDate = interview.ScheduledDate,
                Notes = interview.Notes,
                Outcome = interview.Outcome.ToString(),
                JobApplicationId = interview.JobApplicationId,
                Company = interview.JobApplication?.Company ?? string.Empty,
                Role = interview.JobApplication?.Role ?? string.Empty
            };
        }
    }
}
