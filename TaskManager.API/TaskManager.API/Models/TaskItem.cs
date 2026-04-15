namespace TaskManager.API.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int UserId { get; set; }
    public User? User { get; set; }

    // Columna para el requerimiento adicional de JSON
    public string? AdditionalInfo { get; set; }
}