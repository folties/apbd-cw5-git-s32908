namespace lab06.Models;

public class DataStore
{
    
    public static List<Room> Rooms = new List<Room>
    {
        new Room { Id = 1, Name = "A101", BuildingCode = "A", Floor = 1, Capacity = 20, HasProjector = true, IsActive = true },
        new Room { Id = 2, Name = "B201", BuildingCode = "B", Floor = 2, Capacity = 30, HasProjector = false, IsActive = true },
        new Room { Id = 3, Name = "C301", BuildingCode = "C", Floor = 3, Capacity = 15, HasProjector = true, IsActive = false },
        new Room { Id = 4, Name = "A102", BuildingCode = "A", Floor = 1, Capacity = 25, HasProjector = true, IsActive = true }
    };
    
    
    public static List<Reservation> Reservations = new List<Reservation>
    {
        new Reservation
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "Anna",
            Topic = "C# Basics",
            Date = new DateTime(2026, 5, 10),
            StartTime = new TimeSpan(10,0,0),
            EndTime = new TimeSpan(12,0,0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 2,
            RoomId = 2,
            OrganizerName = "John",
            Topic = "REST API",
            Date = new DateTime(2026, 5, 11),
            StartTime = new TimeSpan(9,0,0),
            EndTime = new TimeSpan(11,0,0),
            Status = "planned"
        },
        new Reservation
        {
            Id = 3,
            RoomId = 1,
            OrganizerName = "Maria",
            Topic = "Databases",
            Date = new DateTime(2026, 5, 10),
            StartTime = new TimeSpan(13,0,0),
            EndTime = new TimeSpan(15,0,0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 4,
            RoomId = 3,
            OrganizerName = "Alex",
            Topic = "Networking",
            Date = new DateTime(2026, 5, 12),
            StartTime = new TimeSpan(8,30,0),
            EndTime = new TimeSpan(10,0,0),
            Status = "cancelled"
        },
        new Reservation
        {
            Id = 5,
            RoomId = 4,
            OrganizerName = "Kate",
            Topic = "Algorithms",
            Date = new DateTime(2026, 5, 13),
            StartTime = new TimeSpan(14,0,0),
            EndTime = new TimeSpan(16,0,0),
            Status = "confirmed"
        }
    };
}