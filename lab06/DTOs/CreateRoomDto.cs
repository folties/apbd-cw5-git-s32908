using System.ComponentModel.DataAnnotations;

namespace lab06.DTOs;

public class CreateRoomDto
{
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    public int BuildingCode { get; set; } 
}