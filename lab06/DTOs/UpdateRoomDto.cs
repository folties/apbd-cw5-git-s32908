using System.ComponentModel.DataAnnotations;

namespace lab06.DTOs;

public class UpdateRoomDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string BuildingCode { get; set; } = string.Empty;
        
    public int Floor { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0.")]
    public int Capacity { get; set; }
    
    public bool HasProjector { get; set; }
    
    public bool IsActive { get; set; }
    
    
    
}