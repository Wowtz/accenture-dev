import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class VinculacaoService {
  vinculados: number[] = [];
  fornecedorId?: number;
  empresaId?: number;

  desvincular(empresaId: number) {
    const index = this.vinculados.indexOf(empresaId);
    if (index !== -1) {
      this.vinculados.splice(index, 1);
    }
  }

  vincular(empresaId: number) {
    if (!this.vinculados.includes(empresaId)) {
      this.vinculados.push(empresaId);
    }
  }

  vinculadosCheck(empresaId: number): boolean {
    return this.vinculados.includes(empresaId);
  }
}
