import { Component, OnInit } from '@angular/core';
import { Fornecedor } from '../../models/fornecedor.vm';
import { FornecedorService } from '../../services/fornecedor-service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { VinculacaoService } from 'src/app/services/vinculacao-service';

@Component({
  selector: 'app-cadastro-fornecedor',
  templateUrl: './cadastro-fornecedor.component.html',
  styleUrls: ['./cadastro-fornecedor.component.css']
})
export class CadastroFornecedorComponent implements OnInit {
  fornecedores!: Fornecedor[];
  filtroNome: string = '';
  filtroCNPJ: string = '';
  filtroEmail: string = '';
  filtroCEP: string = '';
  visibleVincular: boolean = false;

  constructor(
    private fornecedorService: FornecedorService,
    private messageService: MessageService,
    private router: Router,
    private vinculacaoService: VinculacaoService,) {}

  ngOnInit() {
      this.carregarLista();
      this.buscarFornecedores();
  }

  carregarLista(){
    this.fornecedorService.pegarLista().subscribe(response => {
      this.fornecedores = response;
    })
  }

  mostrarMensagemSucesso(message: string) {
    this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: message, life: 5000 });
  }

  buscarFornecedores() {
    this.fornecedorService.filtrarFornecedores(this.filtroNome, this.filtroCNPJ, this.filtroEmail,this.filtroCEP)
      .subscribe((fornecedores) => {
        this.fornecedores = fornecedores;
      });
  }

  limparFiltros() {
    this.filtroNome = '';
    this.filtroCNPJ = '';
    this.filtroEmail = '';
    this.filtroCEP= '';

    this.buscarFornecedores();
  }

  navegar(rotaDinamica: string) {
    this.router.navigate(['/fornecedores', rotaDinamica]);
  }

  excluir(id: number){
    this.fornecedorService.deletar(id).subscribe(resposta => {
      this.carregarLista();
      this.mostrarMensagemSucesso('Fornecedor Excluído com sucesso.');
    })
  }

  editarFornecedor(id: number) {
    this.router.navigate(['/fornecedores/editar', id]);
  }

  vincularEmpresa(id: number) {
    this.visibleVincular = true;
    this.vinculacaoService.fornecedorId = id;
  }
}
