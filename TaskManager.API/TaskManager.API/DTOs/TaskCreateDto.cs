namespace TaskManager.API.DTOs;

public class TaskCreateDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int UserId { get; set; }
    public object? AdditionalInfo { get; set; } // Lo recibimos como objeto JSON
}