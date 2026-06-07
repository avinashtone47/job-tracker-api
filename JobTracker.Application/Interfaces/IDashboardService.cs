using JobTracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardAsync(string userId);
    }
}
