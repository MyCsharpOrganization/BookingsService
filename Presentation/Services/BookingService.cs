using Presentation.Data.Repository;
using Presentation.Models;
using Presentation.Entities;

namespace Presentation.Service;

public class BookingService(IBookingRepository bookingRepository) : IBookingService
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;


    public async Task<BookingResult> CreateBookingAsync(CreateBookingRequest request)
    {

        var bookingEntity = new BookingEntity
        {
            EventId = request.EventId,
            BookingDate = DateTime.Now,
            TicketQuantity = request.TicketQuantity,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            StreetName = request.StreetName,
            PostalCode = request.PostalCode,
            City = request.City,


            BookingOwner = new BookingOwnerEntity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Address = new BookingAddressEntity
                {
                    StreetName = request.StreetName,
                    PostalCode = request.PostalCode,
                    City = request.City
                }
            },
        };


        var result = await _bookingRepository.AddAsync(bookingEntity);

        if (!result.Success)
        {
            throw new Exception($"Booking failed: {result.Error}");
        }

        return new BookingResult { Success = true };

    }
}
