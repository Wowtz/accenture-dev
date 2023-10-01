import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { take } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ValidacaoCEP } from '../models/validacao-cep.vm';
import { FornecedorEmpresa } from '../models/fornecedor-empresa.vm';

@Injectable()
export class ValidationService {
  private URLs = {
    validar:(cep: string) => `https://localhost:44308/v1/validacao/validarCEP/${cep}`,
    empresasVinculadas:(id: number) => `https://localhost:44308/v1/validacao/empresas-vinculadas/${id}`,
    vincularEmpresas:(id: number) => `https://localhost:44308/v1/validacao/vincular-empresa/${id}`,
    fornecedoresVinculados:(id: number) => `https://localhost:44308/v1/validacao/fornecedores-vinculados/${id}`,
    vincularFornecedores:(id: number) => `https://localhost:44308/v1/validacao/vincular-fornecedor/${id}`,
  };

  constructor(private http: HttpClient) {}

  public validarCep(cep: string): Observable<ValidacaoCEP> {
    return this.http.get<ValidacaoCEP>(this.URLs.validar(cep)).pipe(take(1));
  }

  public empresasVinculadas(id: number): Observable<FornecedorEmpresa[]> {
    return this.http.get<FornecedorEmpresa[]>(this.URLs.empresasVinculadas(id)).pipe(take(1));
  }

  public fornecedoresVinculados(id: number): Observable<FornecedorEmpresa[]> {
    return this.http.get<FornecedorEmpresa[]>(this.URLs.fornecedoresVinculados(id)).pipe(take(1));
  }

  public vincularEmpresas(id: number, idsEmpresas: number[]): Observable<FornecedorEmpresa[]> {
    return this.http.post<FornecedorEmpresa[]>(this.URLs.vincularEmpresas(id), idsEmpresas).pipe(take(1));
  }

  public vincularFornecedores(id: number, idsFornecedores: number[]): Observable<FornecedorEmpresa[]> {
    return this.http.post<FornecedorEmpresa[]>(this.URLs.vincularFornecedores(id), idsFornecedores).pipe(take(1));
  }
}