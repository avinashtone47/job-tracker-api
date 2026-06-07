using JobTracker.Application.DTOs;
using JobTracker.Application.Interfaces;
using JobTracker.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {

        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        private string GetUserId()
        {
            // Try standard claim first
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // If null try sub claim (Identity uses this)
            if (string.IsNullOrEmpty(userId))
                userId = User.FindFirst("sub")?.Value;

            // If still null try directly by name
            if (string.IsNullOrEmpty(userId))
                userId = User.Identity?.Name;

            return userId!;
        }





        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            [FromQuery] JobStatus? status = null)
        {
            try
            {
                var result = await _jobService.GetAllAsync(
                    GetUserId(), pageNumber, pageSize, search, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _jobService.GetByIdAsync(id, GetUserId());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobDto dto)
        {
            try
            {
                var result = await _jobService.CreateAsync(dto, GetUserId());
                return CreatedAtAction(nameof(GetById),
                    new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    innerException = ex.InnerException?.Message,
                    innerInnerException = ex.InnerException?.InnerException?.Message
                });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id, [FromBody] UpdateJobDto dto)
        {
            try
            {
                var result = await _jobService.UpdateAsync(id, dto, GetUserId());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _jobService.DeleteAsync(id, GetUserId());
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
