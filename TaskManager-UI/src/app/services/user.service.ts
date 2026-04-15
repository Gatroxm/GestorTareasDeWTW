import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { User } from '../models/user.model';

@Injectable({ providedIn: 'root' })
export class UserService {
  private api = inject(ApiService);

  getUsers(): Observable<User[]> {
    return this.api.get<User[]>('users');
  }

  createUser(user: User): Observable<User> {
    return this.api.post<User>('users', user);
  }
}