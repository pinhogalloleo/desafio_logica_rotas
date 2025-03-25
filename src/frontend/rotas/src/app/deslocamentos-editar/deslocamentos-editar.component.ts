
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { RotasService, Deslocamento } from '../services/rotas-service.service';

@Component({
  selector: 'app-deslocamentos-editar',
  standalone: false,
  templateUrl: './deslocamentos-editar.component.html',
  styleUrl: './deslocamentos-editar.component.css'
})

export class DeslocamentosEditarComponent implements OnInit {
	deslocamento: Deslocamento | undefined;
	
	constructor(
		private readonly rotasService: RotasService,
		private readonly route: ActivatedRoute,
		private readonly router: Router
	) {}
	
	ngOnInit(): void {
		// catch Id from sender
		const id = Number(this.route.snapshot.paramMap.get('id'));
		console.log("editar id: " + "["+ id + "]");
		if(id) { // load, if not null
			this.rotasService.getById(id).subscribe( data => {
				this.deslocamento = data;
			});
		}
	}//..ngOnInit()
	
	editar(): void {
		if (this.deslocamento) {
			this.rotasService.update(this.deslocamento).subscribe(() =>{
				this.router.navigate(['/deslocamentos-listar']);
			});
		}
	}//..editar()
	
	cancelar(): void {
		this.router.navigate(['/deslocamentos-listar']);
	}
}

