import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app-module'; // Asegúrate que el nombre del archivo sea exacto

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));