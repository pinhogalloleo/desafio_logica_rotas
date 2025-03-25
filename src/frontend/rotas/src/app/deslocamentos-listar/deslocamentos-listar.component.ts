
import { Component, OnInit } from '@angular/core';
import { RotasService, Deslocamento } from '../services/rotas-service.service';


@Component({
	selector: 'app-deslocamentos-listar',
	standalone: false,
	templateUrl: './deslocamentos-listar.component.html',
	styleUrls: ['./deslocamentos-listar.component.css']
})

export class DeslocamentosListarComponent implements OnInit {

	deslocamentos: Deslocamento[] = [];

	constructor(
		private readonly rotasService: RotasService
	) { }

	ngOnInit(): void {
		this.getAll();
	}

	getAll(): void {
		this.rotasService.getAll().subscribe(lista => {
		  this.deslocamentos = lista;
		});
	  }

}
