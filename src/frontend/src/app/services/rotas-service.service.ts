
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface Deslocamento {
	id: number;
	origem: string;
	destino: string;
	custo: number;
}

export interface Rota {
	caminho: string[];
	custoTotal: number;
}

@Injectable({
  providedIn: 'root'
})

export class RotasService {
	private readonly apiUrl = environment.backendUrl;
	private readonly rotasCrudApiUrl = `${this.apiUrl}/deslocamento`;
	private readonly melhorRotaApiUrl = `${this.apiUrl}/melhorrota`;
	private readonly httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
	
	constructor(private readonly http: HttpClient) { }
	
	getAll(): Observable<Deslocamento[]> {
		return this.http.get<Deslocamento[]>(this.rotasCrudApiUrl);
	}
	
	getById(id: number): Observable<Deslocamento> {
		const url = `${this.rotasCrudApiUrl}/${id}`;
		console.log("get by id " + id + " - url: " + "[" + url + "]");
		return this.http.get<Deslocamento>(url);
	}
	
	insert(deslocamento: Omit<Deslocamento, 'id'>): Observable<number> {
		return this.http.post<number>(this.rotasCrudApiUrl, deslocamento, this.httpOptions);
	}
	
	update(deslocamento: Deslocamento): Observable<void> {
		return this.http.put<void>(this.rotasCrudApiUrl, deslocamento, this.httpOptions);
	}
	
	delete(id: number): Observable<string> {
		// gambi para evitar erro de response, pois o DELETE não retorna nada. Alterar api
		return this.http.delete(`${this.rotasCrudApiUrl}/${id}`, { responseType: 'text' });
	}
	
	bestRoute(origem: string, destino: string): Observable<Rota> {
		const url = `${this.melhorRotaApiUrl}?origem=${origem}&destino=${destino}`;
		return this.http.get<Rota>(url);
	}
	
}//..export class RotasService

