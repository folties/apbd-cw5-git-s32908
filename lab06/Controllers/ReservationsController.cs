using lab06.DTOs;
using lab06.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lab06.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetReservations([FromQuery] DateTime? date, [FromQuery] string? status,
            int? roomId)
        {
            var query = DataStore.Reservations.AsQueryable();

            if (date.HasValue)
            {
                query = query.Where(r => r.Date >= date.Value);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(r => r.Status == status);
            }

            if (roomId.HasValue)
            {
                query = query.Where(r => r.RoomId == roomId);
            }

            if (!query.Any())
            {
                return NotFound("No reservations found");
            }

            return Ok(query.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound("No reservation with the provided id found");
            }

            return Ok(reservation);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateReservationDto createReservationDto)
        {
            var reservation = new Reservation()
            {
                Id = DataStore.Reservations.Count + 1,
                RoomId = createReservationDto.RoomId,
                OrganizerName = createReservationDto.OrganizerName,
                Topic = createReservationDto.Topic,
                Date = createReservationDto.Date,
                StartTime = createReservationDto.StartTime,
                EndTime = createReservationDto.EndTime,
                Status = createReservationDto.Status
            };

            bool overlap = DataStore.Reservations.Any(r =>
                r.RoomId == reservation.RoomId &&
                r.Date.Date == reservation.Date.Date &&
                reservation.StartTime < r.EndTime &&
                reservation.EndTime > r.StartTime);

            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
            if (room == null)
            {
                return BadRequest("Room with provided id does not exist");
            }

            if (!room.IsActive)
            {
                return BadRequest("Room with provided id is not active");
            }

            if (overlap)
            {
                return Conflict("Reservation overlaps with existing one");
            }

            DataStore.Reservations.Add(reservation);

            return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, CreateReservationDto createReservationDto)
        {
            var reservation = DataStore.Reservations.Find(x => x.Id == id);
            if (reservation == null)
                return NotFound("No reservation with the provided id found");

            var res = ValidateReservation(createReservationDto, id);
            if (res != null)
                return res;

            reservation.RoomId = createReservationDto.RoomId;
            reservation.OrganizerName = createReservationDto.OrganizerName;
            reservation.Topic = createReservationDto.Topic;
            reservation.Status = createReservationDto.Status;
            reservation.Date = createReservationDto.Date;
            reservation.StartTime = createReservationDto.StartTime;
            reservation.EndTime = createReservationDto.EndTime;

            return Ok(reservation);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var reservation = DataStore.Reservations.Find(x => x.Id == id);
            if (reservation == null)
                return NotFound("Reservation with provided id does not exist");

            DataStore.Reservations.Remove(reservation);

            return NoContent();
        }
        
        private IActionResult? ValidateReservation(CreateReservationDto createReservationDto, int id)
        {
            var room = DataStore.Rooms.FirstOrDefault(room => room.Id == createReservationDto.RoomId);
            
            var isOverlapping = DataStore.Reservations.Any(r =>
                r.Id != id &&
                r.RoomId == createReservationDto.RoomId &&
                r.Date == createReservationDto.Date &&
                r.StartTime < createReservationDto.EndTime &&
                r.EndTime > createReservationDto.StartTime
            );
            
            if (room is not { IsActive: true })
                return BadRequest("Room with provided id is not active");

            if (isOverlapping)
                return Conflict("Reservation overlaps with existing one");

            return null;
        }
    }
}
