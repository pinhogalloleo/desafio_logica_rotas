
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DeslocamentosListarComponent } from './deslocamentos-listar/deslocamentos-listar.component';
import { DeslocamentosCriarComponent } from './deslocamentos-criar/deslocamentos-criar.component';
import { DeslocamentosEditarComponent } from './deslocamentos-editar/deslocamentos-editar.component';
import { DeslocamentosExcluirComponent } from './deslocamentos-excluir/deslocamentos-excluir.component';
import { MelhorRotaComponent } from './melhor-rota/melhor-rota.component';


@NgModule({
  declarations: [
    AppComponent,
    DeslocamentosListarComponent,
    DeslocamentosCriarComponent,
    DeslocamentosEditarComponent,
    DeslocamentosExcluirComponent,
    MelhorRotaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
