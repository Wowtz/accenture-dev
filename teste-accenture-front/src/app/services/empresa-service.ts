import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { take } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Empresa } from '../models/empresa.vm';

@Injectable()
export class EmpresaService {
  private URLs = {
    listar: 'https://localhost:44308/v1/empresas/listar',
    filtrar: 'https://localhost:44308/v1/empresas/filtrar',
    adicionar: 'https://localhost:44308/v1/empresas/adicionar',
    deletar:(id: number) => `https://localhost:44308/v1/empresas/deletar/${id}`,
    pegarPorId:(id: number) => `https://localhost:44308/v1/empresas/${id}`,
    atualizar:(id: number) => `https://localhost:44308/v1/empresas/atualizar/${id}`,
  };

  constructor(private http: HttpClient) {}

  public pegarLista(): Observable<Empresa[]> {
    return this.http.get<Empresa[]>(this.URLs.listar).pipe(take(1));
  }

  public adicionar(empresa: Empresa): Observable<Empresa[]> {
    return this.http.post<Empresa[]>(this.URLs.adicionar, empresa).pipe(take(1));
  }

  public pegarPorId(id: number): Observable<Empresa> {
    return this.http.get<Empresa>(this.URLs.pegarPorId(id)).pipe(take(1));
  }

  public atualizar(id: number, empresa: Empresa): Observable<Empresa> {
    return this.http.put<Empresa>(this.URLs.atualizar(id), empresa).pipe(take(1));
  }

  filtrarEmpresas(nomeFantasia: string, cnpj: string, cep: string): Observable<Empresa[]> {
    let params = new HttpParams();
    if (nomeFantasia) params = params.set('NomeFantasia', nomeFantasia);
    if (cnpj) params = params.set('CNPJ', cnpj);
    if (cep) params = params.set('CEP', cep);

    return this.http.get<Empresa[]>(this.URLs.filtrar, { params });
  }

  public deletar(id: number): Observable<any> {
    return this.http.delete<any>(this.URLs.deletar(id)).pipe(take(1));
  }
}