using System;

namespace HRMS.Models.Entities;

public class Notification
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = "Info"; // Info, Success, Warning, Error
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public User? User { get; set; }
}