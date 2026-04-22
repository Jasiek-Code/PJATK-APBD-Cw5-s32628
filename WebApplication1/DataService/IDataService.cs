using WebApplication1.DTOs;

namespace WebApplication1.DataService;

public interface IDataService
{
    public List<RoomDto> Rooms { get; set; }
    public List<ReservationDto> Reservations { get; set; }
}