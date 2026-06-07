using JobTracker.Application.DTOs;
using JobTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Interfaces
{
    public interface IJobService
    {
        Task<PagedResultDto<JobResponseDto>> GetAllAsync(
            string userId,
            int pageNumber,
            int pageSize,
            string? search,
            JobStatus? status);

        Task<JobResponseDto> GetByIdAsync(int id, string userId);
        Task<JobResponseDto> CreateAsync(CreateJobDto dto, string userId);
        Task<JobResponseDto> UpdateAsync(int id, UpdateJobDto dto, string userId);
        Task DeleteAsync(int id, string userId);
    }
}
