
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { RotasService, Deslocamento } from '../services/rotas-service.service';

@Component({
  selector: 'app-deslocamentos-criar',
  standalone: false,
  templateUrl: './deslocamentos-criar.component.html',
  styleUrl: './deslocamentos-criar.component.css'
})

export class DeslocamentosCriarComponent {
	deslocamento: Omit<Deslocamento, 'id'> = {
		origem: '',
		destino: '',
		custo: 0.0
	};
	
	constructor(
		private readonly rotasService: RotasService,
		private readonly router: Router
	) {}
	
	criar() : void {
		this.rotasService.insert(this.deslocamento).subscribe( () => 
			this.router.navigate(['/deslocamentos-listar']));
	}
	
	cancelar(): void {
		this.router.navigate(['/deslocamentos-listar']);
	}
}
