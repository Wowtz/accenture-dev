import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Empresa } from 'src/app/models/empresa.vm';
import { FornecedorEmpresa } from 'src/app/models/fornecedor-empresa.vm';
import { EmpresaService } from 'src/app/services/empresa-service';
import { ValidationService } from 'src/app/services/validation-service';
import { VinculacaoService } from 'src/app/services/vinculacao-service';

@Component({
  selector: 'app-vincular-empresa',
  templateUrl: './vincular-empresa.component.html',
  styleUrls: ['./vincular-empresa.component.css']
})
export class VincularEmpresaComponent implements OnInit {
  empresas: Empresa[] = [];
  fornecedoresEmpresas: FornecedorEmpresa[] = [];
  @Input() visibleVincular: boolean = false;

  constructor( 
    private empresaService: EmpresaService,
    private vinculacaoService: VinculacaoService,
    private validationService: ValidationService,
    private messageService: MessageService,) { }

  ngOnInit(): void {
    this.vinculacaoService.vinculados = [];
    this.empresaService.pegarLista().subscribe(resposta => {
      this.empresas = resposta;
    });

    this.validationService.empresasVinculadas(this.vinculacaoService.fornecedorId!).subscribe(resposta => {
      this.fornecedoresEmpresas = resposta;
      const empresaIds = resposta.map(item => item.empresaId);
      this.vinculacaoService.vinculados = this.vinculacaoService.vinculados.concat(empresaIds);
    })
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['visibleVincular'] && changes['visibleVincular'].currentValue) {
      this.ngOnInit(); 
    }
  }

  vincularOuDesvincular(empresa: Empresa): void {
    if (this.vinculacaoService.vinculadosCheck(empresa.Id!)) {
      this.desvincularEmpresa(empresa);
    } else {
      this.vincularEmpresa(empresa);
    }
  }

  vincularEmpresa(empresa: Empresa): void {
    this.vinculacaoService.vincular(empresa.Id!);
  }

  desvincularEmpresa(empresa: Empresa): void {
    this.vinculacaoService.desvincular(empresa.Id!);
  }

  empresaEstaVinculada(empresaId: number): boolean {
    return this.fornecedoresEmpresas.some(x => x.empresaId === empresaId);
  }

  salvar() {
    console.log(this.vinculacaoService.vinculados, 'teste walter')
    this.validationService
      .vincularEmpresas(this.vinculacaoService.fornecedorId!, this.vinculacaoService.vinculados)
      .subscribe(resposta => {
        this.mostrarMensagemSucesso('Alterações salvas.');
    })
  }

  mostrarMensagemSucesso(message: string) {
    this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: message, life: 5000 });
  }
}
