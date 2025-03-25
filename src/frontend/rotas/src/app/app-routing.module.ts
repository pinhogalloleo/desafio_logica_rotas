
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DeslocamentosListarComponent } from './deslocamentos-listar/deslocamentos-listar.component';
import { DeslocamentosCriarComponent } from './deslocamentos-criar/deslocamentos-criar.component';
import { DeslocamentosEditarComponent } from './deslocamentos-editar/deslocamentos-editar.component';
import { DeslocamentosExcluirComponent } from './deslocamentos-excluir/deslocamentos-excluir.component';
import { MelhorRotaComponent } from './melhor-rota/melhor-rota.component';

const routes: Routes = [
	{ path: '', redirectTo: '/deslocamentos-listar', pathMatch: 'full' },
	{ path: 'deslocamentos-listar', component: DeslocamentosListarComponent },
	{ path: 'deslocamentos-criar', component: DeslocamentosCriarComponent },
	{ path: 'deslocamentos-editar/:id', component: DeslocamentosEditarComponent },
	{ path: 'deslocamentos-excluir/:id', component: DeslocamentosExcluirComponent },
	{ path: 'melhor-rota', component: MelhorRotaComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

