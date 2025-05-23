import { Component } from '@angular/core';
import { environment } from '../environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})

export class AppComponent {
  title = 'rotas';
  
  constructor() {
	console.log(`running in ${environment.environmentName} environment, pointing to backend at: ${environment.backendUrl}`);
  }
}
