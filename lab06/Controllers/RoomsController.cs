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
        public static List<Room> rooms = new List<Room>()
        {
            new Room() { Id = 1, BuildingCode = 1, Name = "Simple" },
            new Room() { Id = 2, BuildingCode = 2, Name = "Luxury" },
            new Room() { Id = 3, BuildingCode = 3, Name = "Middle" }
        };
        
        [HttpGet]
        public IActionResult Get([FromQuery] int? id = 0)
        {
            //200 OK
            return Ok(rooms.Where(r => r.Id >= id));
            
            //404 NotFound
            return NotFound();
        }
        
        //GET api/rooms/{id}
        //GET api/rooms/1
        [HttpGet("{buildingCode}")]
        public IActionResult Get([FromRoute] int buildingCode)
        {
            
            var room = rooms.FirstOrDefault(r => r.BuildingCode == buildingCode);

            if (room == null)
            {
                return NotFound();
            }
            
            //200 OK
            return Ok(room);
            
            //404 NotFound
            //return NotFound();
        }
        
        //POST api/rooms {"name": "Luxury", "BuildingCode": 12 }
        [HttpPost]
        public IActionResult Post([FromBody] CreateRoomDto createRoomDto)
        {
            var room = new Room()
            {
                Id = rooms.Count + 1,
                Name = createRoomDto.Name,
                BuildingCode = createRoomDto.BuildingCode,
            };
            
            rooms.Add(room);
            
            return CreatedAtAction(nameof(Get), new { id = room.Id }, room);
        }
    }
}
