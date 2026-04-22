using WebApplication1.DTOs;

namespace WebApplication1.DataService;

public class DataService : IDataService
{
    public List<RoomDto> Rooms { get; set; } =
    [
        new RoomDto
        {
            Id = 1,
            Name = "Aula A",
            BuildingCode = 1,
            Capacity = 200,
            HasProjector = true,
            IsActive = false
        },
        new RoomDto
        {
            Id = 2,
            Name = "Aula B",
            BuildingCode = 2,
            Capacity = 200,
            HasProjector = true,
            IsActive = true
        },
        new RoomDto
        {
            Id = 3,
            Name = "Sala 210",
            BuildingCode = 1,
            Capacity = 18,
            HasProjector = true,
            IsActive = true
        },
        new RoomDto
        {
            Id = 4,
            Name = "Sala 130",
            BuildingCode = 2,
            Capacity = 18,
            HasProjector = false,
            IsActive = false
        }
    ];

    public List<ReservationDto> Reservations { get; set; } =
    [
        new ReservationDto
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "Kowalski", 
            Topic = "Wyklad",
            Date = DateTime.Now - new TimeSpan(24, 0, 0),
            StartTime  = DateTime.Now,
            EndTime =  DateTime.Now +  new TimeSpan(24, 0, 0),
            Status = "planned"
        },
        new ReservationDto
        {
            Id = 2,
            RoomId = 2,
            OrganizerName = "Nowak", 
            Topic = "Wyklad",
            Date = DateTime.Now,
            StartTime  = DateTime.Now,
            EndTime =  DateTime.Now +  new TimeSpan(24, 0, 0),
            Status = "cancelled"
        },
        new ReservationDto
        {
            Id = 3,
            RoomId = 3,
            OrganizerName = "Nauczycielski", 
            Topic = "Cwiczenia",
            Date = DateTime.Now - new TimeSpan(24, 0, 0),
            StartTime  = DateTime.Now,
            EndTime =  DateTime.Now +  new TimeSpan(24, 0, 0),
            Status = "confirmed"
        },
        new ReservationDto
        {
            Id = 4,
            RoomId = 4,
            OrganizerName = "Testowy", 
            Topic = "Cwiczenia",
            Date = DateTime.Now - new TimeSpan(24, 0, 0),
            StartTime  = DateTime.Now,
            EndTime =  DateTime.Now +  new TimeSpan(24, 0, 0),
            Status = "confirmed"
        }
    ];
}