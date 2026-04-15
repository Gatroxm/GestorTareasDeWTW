using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Data;
using TaskManager.API.Models;
using TaskManager.API.DTOs;

namespace TaskManager.API.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context) => _context = context;

    public async Task<IEnumerable<TaskItem>> GetTasksAsync(string? status, int? userId)
    {
        // Iniciamos la consulta
        var query = _context.Tasks.AsQueryable();

        // Filtro por estado (si viene)
        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(t => t.Status == status);
        }

        // Filtro por usuario (si viene)
        if (userId.HasValue)
        {
            query = query.Where(t => t.UserId == userId.Value);
        }

        // Ordenamos y ejecutamos
        return await query.OrderByDescending(t => t.CreatedAt).ToListAsync();
    }

    public async Task<TaskItem> CreateTaskAsync(TaskCreateDto dto)
    {
        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            UserId = dto.UserId,
            Status = "Pending",
            // Convertimos el objeto JSON que viene del front a un string para SQL
            AdditionalInfo = dto.AdditionalInfo != null ? JsonSerializer.Serialize(dto.AdditionalInfo) : null
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<bool> UpdateStatusAsync(int id, string newStatus)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return false;

        // VALIDACIÓN SENIOR: Impedir salto de Pending a Done
        if (task.Status == "Pending" && newStatus == "Done")
        {
            throw new InvalidOperationException("No se permite cambiar el estado directamente de Pending a Done. Debe pasar por InProgress.");
        }

        task.Status = newStatus;
        await _context.SaveChangesAsync();
        return true;
    }
}