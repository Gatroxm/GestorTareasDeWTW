import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { TaskService } from '../../services/task.service';
import { TaskItem } from '../../models/task.model';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.html',
  styleUrls: ['./task-list.scss'],
  standalone: false
})
export class TaskListComponent implements OnInit {
  private taskService = inject(TaskService);
  private cdr = inject(ChangeDetectorRef);
  
  tasks: TaskItem[] = [];
  // Columnas actualizadas para nivel Senior
  displayedColumns: string[] = ['title', 'user', 'additionalInfo', 'status', 'createdAt', 'actions'];

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(status?: string): void {
    this.taskService.getTasks(status).subscribe({
      next: (data) => {
        this.tasks = data;
        this.cdr.detectChanges(); // Soluciona el error NG0100
      },
      error: (err) => console.error('Error cargando tareas', err)
    });
  }

  onStatusFilterChange(status: string): void {
    this.loadTasks(status);
  }

  // --- LÓGICA PARA PROCESAR EL JSON (ADDITIONAL INFO) ---
  
  private parseInfo(jsonString: string) {
    try {
      return jsonString ? JSON.parse(jsonString) : null;
    } catch {
      return null;
    }
  }

  getPriority(jsonString: string): string {
    const info = this.parseInfo(jsonString);
    return info?.priority || 'Media';
  }

  getTags(jsonString: string): string[] {
    const info = this.parseInfo(jsonString);
    return info?.tags || [];
  }

  getPriorityClass(jsonString: string): string {
    const p = this.getPriority(jsonString).toLowerCase();
    return `prio-${p}`; // Genera: prio-alta, prio-media, etc.
  }

  // --- ACCIONES ---

  changeStatus(id: number, newStatus: string): void {
    this.taskService.updateStatus(id, newStatus).subscribe({
      next: () => this.loadTasks(),
      error: (err) => {
        alert(err.error?.message || 'Error al cambiar estado');
      }
    });
  }
}