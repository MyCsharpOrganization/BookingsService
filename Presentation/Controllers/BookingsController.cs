using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.Service;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController(IBookingService bookingService) : ControllerBase
{
    private readonly IBookingService _bookingService = bookingService;

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookingRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _bookingService.CreateBookingAsync(request);
            return result.Success
                ? Ok()
                : StatusCode(StatusCodes.Status500InternalServerError, "Booking failed. Service returned false.");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Exception: {ex.Message}");
        }
    }
}
