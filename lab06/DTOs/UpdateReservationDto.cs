using System.ComponentModel.DataAnnotations;

namespace lab06.DTOs;

public class UpdateReservationDto
{
    public int Id { get; set; }
    [Required]
    public int RoomId { get; set; }
    [Required]
    public string OrganizerName { get; set; }
    [Required]
    public string Topic { get; set; }
    
    public DateTime Date { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
    
    [AllowedValues(values:["planned", "confirmed", "cancelled"])]
    public string Status { get; set; }
}