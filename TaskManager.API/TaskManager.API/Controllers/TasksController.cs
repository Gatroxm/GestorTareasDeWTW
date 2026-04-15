using Microsoft.AspNetCore.Mvc;
using TaskManager.API.DTOs;
using TaskManager.API.Models;
using TaskManager.API.Services;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    // GET: api/tasks?status=Pending&userId=1
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll(
        [FromQuery] string? status,
        [FromQuery] int? userId)
    {
        // El servicio se encarga de aplicar los filtros si vienen en la URL
        var tasks = await _taskService.GetTasksAsync(status, userId);
        return Ok(tasks);
    }

    // POST: api/tasks
    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create([FromBody] TaskCreateDto dto)
    {
        if (dto.UserId <= 0)
        {
            return BadRequest("Se debe asignar un usuario válido a la tarea.");
        }

        var newTask = await _taskService.CreateTaskAsync(dto);
        return Ok(newTask);
    }

    // PUT: api/tasks/{id}/status
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] string newStatus)
    {
        try
        {
            var result = await _taskService.UpdateStatusAsync(id, newStatus);
            if (!result) return NotFound(new { message = "Tarea no encontrada" });

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            // Aquí capturamos la regla de negocio: "No pasar de Pending a Done"
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocurrió un error inesperado.");
        }
    }
}