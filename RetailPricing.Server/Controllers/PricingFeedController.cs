using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailPricing.Models;
using RetailPricing.Services.Interfaces;

namespace RetailPricing.Server.Controllers
{
    public class PricingFeedController : Controller
    {
        private readonly IPricingService _pricingService;

        public PricingFeedController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded or file is empty.");
            }

            try
            {
                var processResult = await _pricingService.ProcessFileAsync(file);
                if (processResult)
                {
                    return Ok(new { message = "File successfully processed and data stored." });
                }
                else
                {
                    return StatusCode(500, "Failed to process file.");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchCriteria criteria)
        {
            try
            {
                var records = await _pricingService.SearchAsync(criteria);
                return Ok(records);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PricingRecordUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updateResult = await _pricingService.UpdatePricingRecordAsync(id, updateDto);
                if (updateResult)
                {
                    return Ok(new { message = "Record updated successfully." });
                }
                else
                {
                    return NotFound($"Record with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PricingRecord>> GetPricingRecordById(int id)
        {
            try
            {
                var records = await _pricingService.GetRecordByIdAsync(id);
                return Ok(records);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
