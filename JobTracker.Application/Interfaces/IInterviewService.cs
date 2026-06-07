using JobTracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Interfaces
{
    public interface IInterviewService
    {
        Task<List<InterviewResponseDto>> GetAllByJobIdAsync(
            int jobId, string userId);
        Task<InterviewResponseDto> GetByIdAsync(int id, string userId);
        Task<InterviewResponseDto> CreateAsync(
            int jobId, CreateInterviewDto dto, string userId);
        Task<InterviewResponseDto> UpdateAsync(
            int id, UpdateInterviewDto dto, string userId);
        Task DeleteAsync(int id, string userId);
    }
}
