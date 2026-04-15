import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.html',
  styleUrls: ['./user-form.scss'],
  standalone: false
})
export class UserFormComponent {
  private fb = inject(FormBuilder);
  private userService = inject(UserService);
  private router = inject(Router);

  userForm: FormGroup;

  constructor() {
    this.userForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onSubmit(): void {
    if (this.userForm.invalid) return;

    this.userService.createUser(this.userForm.value).subscribe({
      next: () => {
        alert('Colaborador registrado con éxito');
        this.router.navigate(['/tasks']); // Redirigir a tareas tras crear
      },
      error: (err) => alert('Error al registrar usuario: ' + err.message)
    });
  }
}