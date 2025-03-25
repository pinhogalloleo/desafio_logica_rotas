
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RotasService, Rota } from '../services/rotas-service.service';

@Component({
  selector: 'app-melhor-rota',
  standalone: false,
  templateUrl: './melhor-rota.component.html',
  styleUrl: './melhor-rota.component.css'
})

export class MelhorRotaComponent {
  origem: string = '';
  destino: string = '';
  rota: Rota | null = null;
  errorMessage: string | null = null;

  constructor(
    private readonly rotasService: RotasService,
    private readonly router: Router
  ) { }

  calcularMelhorRota(): void {
    alert('Calculando melhor rota...');
    if (!this.origem || !this.destino) {
      this.errorMessage = 'Origem e destino são obrigatórios';
      return;
    }

    this.errorMessage = null;
    this.rotasService.bestRoute(this.origem, this.destino).subscribe({
      next: (result: Rota) => {
        this.rota = result; // Assigning the result to the rota property
      },
      error: (error) => {
        console.error('Erro ao calcular a melhor rota', error);
        this.errorMessage = 'Erro ao calcular a melhor rota ' + error;
      }
    });
  }

  cancelar(): void{
    this.router.navigate(['/deslocamentos-listar']);
  }

}

