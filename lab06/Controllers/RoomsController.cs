using lab06.DTOs;
using lab06.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lab06.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        
        [HttpGet]
        public IActionResult GetRooms([FromQuery] int? minCapacity, [FromQuery] bool? hasProjector,
            bool? isActive)
        {
            var rooms = DataStore.Rooms.Where(r =>
                (minCapacity is null || r.Capacity >= minCapacity) &&
                (hasProjector is null || r.HasProjector == hasProjector) &&
                (isActive is null || r.IsActive == isActive)
            );
            
            if (!rooms.Any())
            {
                return NotFound("No rooms found");
            }

            return Ok(rooms);
        }
        
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
            {
                return NotFound("No room with the provided id found");
            }
            
            return Ok(room);
        }
        
        [HttpGet("building/{buildingCode}")]
        public IActionResult GetAllByBuildingCode([FromRoute] string buildingCode)
        {

            var rooms = DataStore.Rooms.Where(r => r.BuildingCode == buildingCode);
            
            if (!rooms.Any())
            {
                return NotFound("No rooms with a provided building code found");
            }
            
            return Ok(rooms);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] CreateRoomDto createRoomDto)
        {
            var room = new Room()
            {
                Id = DataStore.Rooms.Count + 1,
                Name = createRoomDto.Name,
                BuildingCode = createRoomDto.BuildingCode,
                Floor = createRoomDto.Floor,
                Capacity = createRoomDto.Capacity,
                HasProjector = createRoomDto.HasProjector,
                IsActive = createRoomDto.IsActive
            };
            
            DataStore.Rooms.Add(room);
            
            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] UpdateRoomDto updateRoomDto)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
            {
                return NotFound("Room with provided id does not exist");
            }

            room.Name = updateRoomDto.Name;
            room.BuildingCode = updateRoomDto.BuildingCode;
            room.Floor = updateRoomDto.Floor;
            room.Capacity = updateRoomDto.Capacity;
            room.HasProjector = updateRoomDto.HasProjector;
            room.IsActive = updateRoomDto.IsActive;
            
            return Ok(room);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound("Room with provided id does not exist");
            }
            
            bool hasReservations = DataStore.Reservations.Any(r => r.Id == id);

            if (hasReservations)
            {
                return Conflict("Room can not be deleted, future reservations exists");
            }
            
            DataStore.Rooms.Remove(room);
            
            return NoContent();
        }
    }
}
