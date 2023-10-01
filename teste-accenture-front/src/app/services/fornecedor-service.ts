import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { take } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Empresa } from '../models/empresa.vm';
import { Fornecedor } from '../models/fornecedor.vm';

@Injectable()
export class FornecedorService {
  private URLs = {
    listar: 'https://localhost:44308/v1/fornecedores/listar',
    filtrar: 'https://localhost:44308/v1/fornecedores/filtrar',
    adicionar: 'https://localhost:44308/v1/fornecedores/adicionar',
    deletar:(id: number) => `https://localhost:44308/v1/fornecedores/deletar/${id}`,
    pegarPorId:(id: number) => `https://localhost:44308/v1/fornecedores/${id}`,
    atualizar:(id: number) => `https://localhost:44308/v1/fornecedores/atualizar/${id}`,
  };

  constructor(private http: HttpClient) {}

  public pegarLista(): Observable<Fornecedor[]> {
    return this.http.get<Fornecedor[]>(this.URLs.listar).pipe(take(1));
  }

  public pegarPorId(id: number): Observable<Fornecedor> {
    return this.http.get<Fornecedor>(this.URLs.pegarPorId(id)).pipe(take(1));
  }

  public atualizar(id: number, fornecedor: Fornecedor): Observable<Fornecedor> {
    return this.http.put<Fornecedor>(this.URLs.atualizar(id), fornecedor).pipe(take(1));
  }

  filtrarFornecedores(nome: string, cnpj: string, email: string,cep: string): Observable<Fornecedor[]> {
    let params = new HttpParams();
    if (nome) params = params.set('Nome', nome);
    if (cnpj) params = params.set('CNPJ', cnpj);
    if (email) params = params.set('Email', email);
    if (cep) params = params.set('CEP', cep);

    return this.http.get<Fornecedor[]>(this.URLs.filtrar, { params });
  }

  public adicionar(fornecedor: Fornecedor): Observable<Fornecedor[]> {
    return this.http.post<Fornecedor[]>(this.URLs.adicionar, fornecedor).pipe(take(1));
  }

  public deletar(id: number): Observable<any> {
    return this.http.delete<any>(this.URLs.deletar(id)).pipe(take(1));
  }
}