import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Empresa } from 'src/app/models/empresa.vm';
import { FornecedorEmpresa } from 'src/app/models/fornecedor-empresa.vm';
import { Fornecedor } from 'src/app/models/fornecedor.vm';
import { EmpresaService } from 'src/app/services/empresa-service';
import { FornecedorService } from 'src/app/services/fornecedor-service';
import { ValidationService } from 'src/app/services/validation-service';
import { VinculacaoService } from 'src/app/services/vinculacao-service';

@Component({
  selector: 'app-vincular-fornecedor',
  templateUrl: './vincular-fornecedor.component.html',
  styleUrls: ['./vincular-fornecedor.component.css']
})
export class VincularFornecedorComponent implements OnInit {
  fornecedores: Fornecedor[] = [];
  fornecedoresEmpresas: FornecedorEmpresa[] = [];
  @Input() visibleVincular: boolean = false;

  constructor( 
    private fornecedoresService: FornecedorService,
    private vinculacaoService: VinculacaoService,
    private messageService: MessageService,
    private validationService: ValidationService,
  ) { }

  ngOnInit(): void {
    this.vinculacaoService.vinculados = [];
    this.fornecedoresService.pegarLista().subscribe(resposta => {
      this.fornecedores = resposta;
    });

    this.validationService.fornecedoresVinculados(this.vinculacaoService.empresaId!).subscribe(resposta => {
      this.fornecedoresEmpresas = resposta;
      const fornecedorIds = resposta.map(item => item.fornecedorId);
      this.vinculacaoService.vinculados = this.vinculacaoService.vinculados.concat(fornecedorIds);
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['visibleVincular'] && changes['visibleVincular'].currentValue) {
      this.ngOnInit(); 
    }
  }

  vincularOuDesvincular(fornecedor: Fornecedor): void {
    if (this.vinculacaoService.vinculadosCheck(fornecedor.Id!)) {
      this.desvincularFornecedor(fornecedor);
    } else {
      this.vincularFornecedor(fornecedor);
    }
  }

  vincularFornecedor(fornecedor: Fornecedor): void {
    this.vinculacaoService.vincular(fornecedor.Id!);
  }

  desvincularFornecedor(fornecedor: Fornecedor): void {
    this.vinculacaoService.desvincular(fornecedor.Id!);
  }

  fornecedorEstaVinculado(fornecedorId: number): boolean {
    return this.fornecedoresEmpresas.some(x => x.fornecedorId === fornecedorId);
  }

  salvar() {
    console.log(this.vinculacaoService.vinculados, 'teste walter')
      this.validationService
        .vincularFornecedores(this.vinculacaoService.empresaId!, this.vinculacaoService.vinculados)
        .subscribe(resposta => {
          this.mostrarMensagemSucesso('Alterações salvas.');
      })
  }

  mostrarMensagemSucesso(message: string) {
    this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: message, life: 5000 });
  }
}
