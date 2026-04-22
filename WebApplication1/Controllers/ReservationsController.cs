using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataService;
using WebApplication1.DTOs;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController(IDataService dataService) : ControllerBase
{ 
// return Ok(); // 200
// return Created(); // 201
// return NoContent(); // 204
// return BadRequest(); // 400
// return NotFound(); // 404
// return Problem(); // 500
    
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(dataService.Reservations.Select(e => new ReservationDto
        {
            Id = e.Id,
            RoomId = e.RoomId,
            OrganizerName = e.OrganizerName,
            Topic = e.Topic,
            Date = e.Date,
            StartTime = e.StartTime,
            EndTime =  e.EndTime,
            Status = e.Status
        }));
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var reservation = dataService.Reservations.FirstOrDefault(e => e.Id == id);

        if (reservation is null)
        {
            return NotFound($"Room with id {id} not found");
        }
        
        return Ok(new ReservationDto
        {
            Id = reservation.Id,
            RoomId = reservation.RoomId,
            OrganizerName = reservation.OrganizerName,
            Topic = reservation.Topic,
            Date = reservation.Date,
            StartTime = reservation.StartTime,
            EndTime = reservation.EndTime,
            Status = reservation.Status
        });
    }
    
    [HttpGet("filtered")]
    public IActionResult GetByParams(
        [FromQuery] DateTime? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId
    )
    {
        return Ok(dataService.Reservations.Where(e => (date == null || e.Date >= date) && 
                                               (status == null || e.Status == status) && 
                                               (roomId == null || e.RoomId == roomId)
        ).Select(e => new ReservationDto
        {
            Id = e.Id,
            RoomId = e.RoomId,
            OrganizerName = e.OrganizerName,
            Topic = e.Topic,
            Date = e.Date,
            StartTime = e.StartTime,
            EndTime = e.EndTime,
            Status = e.Status,
        }));
    }
    
    [HttpPost]
    public IActionResult CreateReservation([FromBody] ReservationDto dto)
    {
        var room = dataService.Rooms.FirstOrDefault(r => r.Id == dto.RoomId);
        
        if (room == null)
        {
            return BadRequest($"Room with id {dto.RoomId} not found.");
        }

        if (!room.IsActive)
        {
            return BadRequest($"Room {room.Name} is not active right now.");
        }
        
        var isOverlapping = dataService.Reservations.Any(r =>
                r.RoomId == dto.RoomId &&          
                r.Date.Date == dto.Date.Date &&    
                r.StartTime < dto.EndTime &&       
                r.EndTime > dto.StartTime          
        );

        if (isOverlapping)
        {
            return Conflict("Reservation is overlapping.");
        }

        var newId = dataService.Reservations.Count != 0 ? dataService.Reservations.Max(r => r.Id) + 1 : 1;
        dto.Id = newId;
        dataService.Reservations.Add(dto);
        
        return CreatedAtAction(nameof(GetById), new { id = newId }, dto);
    }
    
    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] ReservationDto updateDto)
    {
        var reservation = dataService.Reservations.FirstOrDefault(e => e.Id == id);

        if (reservation is null)
        {
            return NotFound($"Reservation with id {id} not found.");
        }
        
        reservation.RoomId = updateDto.RoomId;
        reservation.OrganizerName = updateDto.OrganizerName;
        reservation.Topic = updateDto.Topic;
        reservation.Date = updateDto.Date;
        reservation.StartTime = updateDto.StartTime;
        reservation.EndTime = updateDto.EndTime;
        reservation.Status = updateDto.Status;
        
        return Ok(reservation);
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var reservation = dataService.Reservations.FirstOrDefault(e => e.Id == id);

        if (reservation is null)
        {
            return NotFound($"Reservation with id {id} not found."); // 404
        }
        
        dataService.Reservations.Remove(reservation);
        
        return NoContent();
    }
    
}