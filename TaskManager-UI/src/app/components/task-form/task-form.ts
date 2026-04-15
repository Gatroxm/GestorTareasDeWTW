import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TaskService } from '../../services/task.service';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-task-form',
  templateUrl: './task-form.html',
  styleUrls: ['./task-form.scss'],
  standalone: false
})
export class TaskFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private taskService = inject(TaskService);
  private userService = inject(UserService);
  private router = inject(Router);

  taskForm: FormGroup;
  users: User[] = [];

  constructor() {
    this.taskForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(3)]],
      description: [''],
      userId: [null, Validators.required],
      // Estos campos se agruparán en el JSON 'additionalInfo'
      priority: ['Baja'],
      tags: ['']
    });
  }

  ngOnInit(): void {
    // Cargamos los usuarios para el dropdown de asignación
    this.userService.getUsers().subscribe(data => this.users = data);
  }

  onSubmit(): void {
    if (this.taskForm.invalid) return;

    const formValue = this.taskForm.value;

    // Construimos el DTO para el backend
    const taskDto = {
      title: formValue.title,
      description: formValue.description,
      userId: formValue.userId,
      // Aquí empaquetamos el requerimiento de información adicional (JSON)
      additionalInfo: {
        priority: formValue.priority,
        tags: formValue.tags.split(',').map((t: string) => t.trim())
      }
    };

    this.taskService.createTask(taskDto).subscribe({
      next: () => {
        alert('Tarea creada con éxito');
        this.router.navigate(['/tasks']);
      },
      error: (err) => alert('Error al crear la tarea: ' + err.message)
    });
  }
}