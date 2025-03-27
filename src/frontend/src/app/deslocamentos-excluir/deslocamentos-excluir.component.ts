
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RotasService, Deslocamento } from '../services/rotas-service.service';
import { FormsModule } from '@angular/forms'; // Add this for template-driven forms


@Component({
	selector: 'app-deslocamentos-excluir',
	standalone: false,
	templateUrl: './deslocamentos-excluir.component.html',
	styleUrl: './deslocamentos-excluir.component.css'
})

export class DeslocamentosExcluirComponent implements OnInit {
	deslocamentoId: number | undefined;
	deslocamento: Deslocamento | undefined;

	constructor(
		private readonly rotasService: RotasService,
		private readonly route: ActivatedRoute,
		private readonly router: Router
	) { }

	ngOnInit(): void {
		// catch Id from sender
		this.deslocamentoId = Number(this.route.snapshot.paramMap.get('id'));
		console.log("excluir id: " + "[" + this.deslocamentoId + "]");
		if (this.deslocamentoId) { // load, if not null
			this.rotasService.getById(this.deslocamentoId).subscribe(data => {
				this.deslocamento = data;
			});
		}
	}//..ngOnInit()

	excluir(): void {
		const id = this.deslocamentoId ?? 0;
		if (this.deslocamento) {
			const confirmed = window.confirm("Deseja realmente excluir o deslocamento?");
			console.log("Confirmed: " + confirmed);
			if (confirmed) {
				this.rotasService.delete(id).subscribe(() => {
					this.router.navigate(['/deslocamentos-listar']);
				});
			}
		}
	}//..excluir()

	cancelar(): void {
		this.router.navigate(['/deslocamentos-listar']);
	}

}
