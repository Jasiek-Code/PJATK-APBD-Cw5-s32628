using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class CreateReservationDto
{
    [MaxLength(25), Required]
    public string OrganizerName { get; set; } = string.Empty;
    [MaxLength(25), Required]
    public string Topic { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    [MaxLength(25), Required]
    public string Status { get; set; } = string.Empty;
}