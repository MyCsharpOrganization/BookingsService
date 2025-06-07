using System.ComponentModel.DataAnnotations.Schema;

namespace Presentation.Entities;

public class BookingEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string EventId { get; set; } = null!;
    public int TicketQuantity { get; set; } = 1;
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;
    public string StreetName { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
    public DateTime BookingDate {get; set; }

    [ForeignKey(nameof(BookingOwner))]

    public string BookingOwnerID { get; set; } = null!;

    public BookingOwnerEntity BookingOwner  { get; set; } = null!;
}
