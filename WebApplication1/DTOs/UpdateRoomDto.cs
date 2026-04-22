using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class UpdateRoomDto
{
    [MaxLength(25), Required]
    public string Name { get; set; } = string.Empty;
    public int BuildingCode { get; set; }
    public int Capacity { get; set; }
    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }
}