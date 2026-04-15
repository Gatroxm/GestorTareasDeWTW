namespace TaskManager.API.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!; // Requerido 
    public string Email { get; set; } = null!; // Requerido 
}