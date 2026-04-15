using TaskManager.API.Models;
using TaskManager.API.DTOs;

namespace TaskManager.API.Services;

public interface ITaskService
{
    // Cambiamos la firma para aceptar el int? userId
    Task<IEnumerable<TaskItem>> GetTasksAsync(string? status, int? userId);

    Task<TaskItem> CreateTaskAsync(TaskCreateDto dto);
    Task<bool> UpdateStatusAsync(int id, string newStatus);
}