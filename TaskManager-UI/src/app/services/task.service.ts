import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { TaskItem } from '../models/task.model';

@Injectable({ providedIn: 'root' })
export class TaskService {
  private api = inject(ApiService);

  getTasks(status?: string, userId?: number): Observable<TaskItem[]> {
    const params: any = {};
    if (status) params.status = status;
    if (userId) params.userId = userId;
    
    return this.api.get<TaskItem[]>('Tasks', params);
  }

  createTask(task: any): Observable<TaskItem> {
    return this.api.post<TaskItem>('Tasks', task);
  }

  updateStatus(id: number, status: string): Observable<void> {

    return this.api.put<void>(`Tasks/${id}/status`, JSON.stringify(status));
  }
}