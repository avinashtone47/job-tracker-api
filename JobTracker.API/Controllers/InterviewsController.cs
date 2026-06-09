using JobTracker.Application.DTOs;
using JobTracker.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobTracker.API.Controllers
{
    [Route("api/[controller]")]
    [Route("api/jobs/{jobId}/[controller]")]
    [ApiController]
    public class InterviewsController : ControllerBase
    {


        private readonly IInterviewService _interviewService;

        public InterviewsController(IInterviewService interviewService)
        {
            _interviewService = interviewService;
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
        public async Task<IActionResult> GetAll([FromRoute] int jobId)
        {
            try
            {
                var result = await _interviewService
                    .GetAllByJobIdAsync(jobId, GetUserId());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromRoute] int jobId, [FromBody] CreateInterviewDto dto)
        {
            try
            {
                var result = await _interviewService
                    .CreateAsync(jobId, dto, GetUserId());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] int jobId, int id, [FromBody] UpdateInterviewDto dto)
        {
            try
            {
                var result = await _interviewService
                    .UpdateAsync(id, dto, GetUserId());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int jobId, int id)
        {
            try
            {
                await _interviewService.DeleteAsync(id, GetUserId());
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
