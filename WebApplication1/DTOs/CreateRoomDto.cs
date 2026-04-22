using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class CreateRoomDto
{
    [MaxLength(25), Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public int BuildingCode { get; set; }
    
    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }
    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }
}