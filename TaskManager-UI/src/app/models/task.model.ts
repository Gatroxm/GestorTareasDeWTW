export interface TaskItem {
  id?: number;
  title: string;
  description?: string;
  status: 'Pending' | 'InProgress' | 'Done';
  createdAt?: Date;
  userId: number;
  additionalInfo?: string; // Aquí viene el JSON del backend
}