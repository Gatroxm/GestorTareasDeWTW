import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TaskListComponent } from './components/task-list/task-list';
import { TaskFormComponent } from './components/task-form/task-form';
import { UserFormComponent } from './components/user-form/user-form';

const routes: Routes = [
  { path: 'tasks', component: TaskListComponent },
  { path: 'new-task', component: TaskFormComponent },
  { path: 'users', component: UserFormComponent },
  { path: '', redirectTo: '/tasks', pathMatch: 'full' }, // Redirige al inicio
  { path: '**', redirectTo: '/tasks' } // Ruta comodín por si escriben algo mal
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule] // ¡ESTO ES VITAL!
})
export class AppRoutingModule { }