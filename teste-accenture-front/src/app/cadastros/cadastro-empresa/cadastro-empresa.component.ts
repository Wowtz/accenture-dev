import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Empresa } from 'src/app/models/empresa.vm';
import { EmpresaService } from 'src/app/services/empresa-service';
import { VinculacaoService } from 'src/app/services/vinculacao-service';

@Component({
  selector: 'app-cadastro-empresa',
  templateUrl: './cadastro-empresa.component.html',
  styleUrls: ['./cadastro-empresa.component.css']
})
export class CadastroEmpresaComponent {
  empresas!: Empresa[];
  filtroNomeFantasia: string = '';
  filtroCNPJ: string = '';
  filtroCEP: string = '';
  visibleVincular: boolean = false;

  constructor(
    private empresaService: EmpresaService,
    private router: Router,
    private messageService: MessageService,
    private vinculacaoService: VinculacaoService,) {}

  ngOnInit() {
      this.carregarLista();
  }

  carregarLista(){
    this.empresaService.pegarLista().subscribe(response => {
      this.empresas = response;
    })
  }

  buscarEmpresas() {
    this.empresaService.filtrarEmpresas(this.filtroNomeFantasia, this.filtroCNPJ, this.filtroCEP)
      .subscribe((empresas) => {
        this.empresas = empresas;
      });
  }

  excluir(id: number){
    this.empresaService.deletar(id).subscribe(resposta => {
      this.ngOnInit();
      this.mostrarMensagemSucesso('Fornecedor Excluído com sucesso.');
    })
  }

  editarEmpresa(id: number) {
    this.router.navigate(['/empresas/editar', id]);
  }

  mostrarMensagemSucesso(message: string) {
    this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: message, life: 5000 });
  }

  limparFiltros() {
    this.filtroNomeFantasia = '';
    this.filtroCNPJ = '';
    this.filtroCEP= '';

    this.buscarEmpresas();
  }

  navegar(rotaDinamica: string) {
    this.router.navigate(['/empresas', rotaDinamica]);
  }

  vincularFornecedor(id: number) {
    this.visibleVincular = true;
    this.vinculacaoService.empresaId = id;
  }
}