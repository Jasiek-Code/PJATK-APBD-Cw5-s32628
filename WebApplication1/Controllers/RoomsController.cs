using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataService;
using WebApplication1.DTOs;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController(IDataService dataService) : ControllerBase
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
        return Ok(dataService.Rooms.Select(e => new RoomDto
        {
            Id = e.Id,
            Name = e.Name,
            BuildingCode = e.BuildingCode,
            Capacity = e.Capacity,
            HasProjector = e.HasProjector,
            IsActive = e.IsActive
        }));
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var room = dataService.Rooms.FirstOrDefault(e => e.Id == id);

        if (room is null)
        {
            return NotFound($"Room with id {id} not found");
        }
        
        return Ok(new RoomDto
        {
            Id = room.Id,
            Name = room.Name,
            BuildingCode = room.BuildingCode,
            Capacity = room.Capacity,
            HasProjector = room.HasProjector,
            IsActive = room.IsActive
        });
    }
    
    [HttpGet("building/{buildingCode:int}")]
    public IActionResult GetByBuilding([FromRoute] int buildingCode)
    {
        return Ok(dataService.Rooms.Where(e => e.BuildingCode == buildingCode).Select(e => new RoomDto
        {
            Id = e.Id,
            Name = e.Name,
            BuildingCode = e.BuildingCode,
            Capacity = e.Capacity,
            HasProjector = e.HasProjector,
            IsActive = e.IsActive
        }));
    }
    
    // GET /api/rooms?minCapacity=20&hasProjector=true&activeOnly=true
    [HttpGet("filtered")]
    public IActionResult GetByParams(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly
        
        )
    {
        return Ok(dataService.Rooms.Where(e => (minCapacity == null || e.Capacity >= minCapacity) && 
                                                (hasProjector == null || e.HasProjector == hasProjector) && 
                                                (activeOnly == null || e.IsActive == activeOnly)
        ).Select(e => new RoomDto
        {
            Id = e.Id,
            Name = e.Name,
            BuildingCode = e.BuildingCode,
            Capacity = e.Capacity,
            HasProjector = e.HasProjector,
            IsActive = e.IsActive
        }));
    }
    
    [HttpPost]
    public IActionResult Add([FromBody] CreateRoomDto dto)
    {
        var room = new RoomDto
        {
            Id = dataService.Rooms.Max(e => e.Id) + 1,
            Name = dto.Name,
            BuildingCode = dto.BuildingCode,
            Capacity = dto.Capacity,
            HasProjector = dto.HasProjector,
            IsActive = dto.IsActive
        };

        dataService.Rooms.Add(room);
        
        // return Created($"students/{student.Id}", student);
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, UpdateRoomDto updateDto)
    {
        var room = dataService.Rooms.FirstOrDefault(e => e.Id == id);

        if (room is null)
        {
            return NotFound($"Room with id {id} not found");
        }
        
        room.Name = updateDto.Name;
        room.BuildingCode = updateDto.BuildingCode;
        room.Capacity = updateDto.Capacity;
        room.HasProjector = updateDto.HasProjector;
        room.IsActive = updateDto.IsActive;
        
        return Ok(room);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var room = dataService.Rooms.FirstOrDefault(e => e.Id == id);

        if (room is null)
        {
            return NotFound($"Room with id {id} not found");
        }

        var hasFutureReservations = dataService.Reservations.Any(r => 
            r.RoomId == id && 
            r.Date >= DateTime.Today);
        
        if (hasFutureReservations)
        {
            return Conflict($"Cannot delete room {room.Name}, because it has active reservations.");
        }
        
        dataService.Rooms.Remove(room);
        
        return NoContent();
    }
    
}