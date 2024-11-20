using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOSInventory.Data;
using SOSInventory.Dto;

namespace SOSInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SosInventoryController : ControllerBase
    {
        private readonly SosInventoryDbContext _context;
        private readonly IConfiguration _configuration;

        public SosInventoryController(SosInventoryDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemQuantity(int id)
        {
            try
            {
                // Get the item details
                var item = await _context.Items.FindAsync(id);
                if (item == null)
                {
                    return NotFound(new { Message = "Item not found" });
                }

                // Calculate the quantity on hand
                var totalReceived = await _context.ItemReceipts
                    .Where(r => r.ItemId == id)
                    .SumAsync(r => (int?)r.QuantityReceived) ?? 0;

                var totalShipped = await _context.Shipments
                    .Where(s => s.ItemId == id)
                    .SumAsync(s => (int?)s.QuantityShipped) ?? 0;

                var totalAdjusted = await _context.Adjustments
                    .Where(a => a.ItemId == id)
                    .SumAsync(a => (int?)a.QuantityAdjusted) ?? 0;

                int quantityOnHand = (totalReceived + totalAdjusted) - totalShipped;

                //Return the response
                return Ok(new ItemQuantityResponse
                {
                    Name = item.ItemName,
                    QuantityOnHand = quantityOnHand
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request." });
            }
        }
    }
}