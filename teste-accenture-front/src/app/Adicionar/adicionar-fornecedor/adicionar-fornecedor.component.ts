import { Component, SimpleChanges } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { TipoPessoa } from 'src/app/enums/tipo-pessoa.enum';
import { Fornecedor } from 'src/app/models/fornecedor.vm';
import { ValidacaoCEP } from 'src/app/models/validacao-cep.vm';
import { FornecedorService } from 'src/app/services/fornecedor-service';
import { ValidationService } from 'src/app/services/validation-service';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';

@Component({
  selector: 'app-adicionar-fornecedor',
  templateUrl: './adicionar-fornecedor.component.html',
  styleUrls: ['./adicionar-fornecedor.component.css']
})
export class AdicionarFornecedorComponent {
  fornecedor = new Fornecedor();
  TipoPessoa = TipoPessoa;
  exibirErroTipoPessoa: boolean = false;
  cepExiste?: ValidacaoCEP;

  constructor(
    private fornecedorService : FornecedorService,
    private ValidationService : ValidationService,
    private messageService: MessageService,
    private router: Router,
  ) { }

  mostrarMensagemAlerta(message: string) {
    this.messageService.add({ severity: 'error', summary: 'Erro', detail: message });
  }

  mostrarMensagemSucesso(message: string) {
    this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: message });
  }

  fornecedorForm = new FormGroup({
    CNPJCPF: new FormControl('', [Validators.required, Validators.pattern(/^\d{3}\.\d{3}\.\d{3}-\d{2}$|^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$/)]),
    Nome: new FormControl('', [Validators.required]),
    Email: new FormControl('', [Validators.required, Validators.email, this.emailValidator]),
    CEP: new FormControl('', [Validators.required, Validators.pattern(/^\d{5}-\d{3}$/)]),
    DataNascimento: new FormControl(''),
    RG: new FormControl(''),
    TipoPessoa: new FormControl('')
  });

  onTipoPessoaChange() {
    if (this.fornecedor.TipoPessoa == TipoPessoa.PessoaFisica.valueOf()) {
      this.fornecedorForm.get('RG')?.setValidators([Validators.required]);
      this.fornecedorForm.get('DataNascimento')?.setValidators([Validators.required]);
    } else {
      this.fornecedorForm.get('RG')?.clearValidators();
      this.fornecedorForm.get('DataNascimento')?.clearValidators();
    }

    this.fornecedorForm.get('RG')?.updateValueAndValidity();
    this.fornecedorForm.get('DataNascimento')?.updateValueAndValidity();
  }


  errorMessages = {
    CNPJCPF: {
      required: 'Campo CNPJ/CPF é obrigatório.',
      pattern: 'O CNPJ/CPF deve estar no formato correto.'
    },
    Nome: {
      required: 'Campo Nome é obrigatório.'
    },
    Email: {
      required: 'Campo Email é obrigatório.',
      email: 'O endereço de e-mail não é válido.'
    },
    CEP: {
      required: 'Campo CEP é obrigatório.',
      pattern: 'O CEP deve estar no formato correto.'
    },
    DataNascimento: {
      required: 'Campo Data de Nascimento é obrigatório.'
    },
    RG: {
      required: 'Campo RG é obrigatório.'
    },
  };

  adicionarFornecedor() {
    this.pessoaSelecionada();
    this.validarCep();

    if (this.fornecedorForm.valid) {
      if (!this.cepExiste?.cepValido) { 
        this.mostrarMensagemAlerta("CEP inválido.");
        return 
      }
      
      this.fornecedor = this.criarFornecedorAPartirDoFormulario();

      if (!this.verificarMaiorIdade(this.fornecedor.DataNascimento!) && this.cepExiste.parana) { 
        this.mostrarMensagemAlerta("Quando o Fornecedor for do Paraná, deve ser maior de idade.");
        return 
      }

      this.fornecedorService.adicionar(this.fornecedor).subscribe(resposta => {
        this.mostrarMensagemSucesso("Fornecedor Adicionado com sucesso.")
      })

      this.router.navigate(['/fornecedores']);
    }
  }
  
  criarFornecedorAPartirDoFormulario(): Fornecedor {
    const fornecedor = new Fornecedor();
    fornecedor.CNPJCPF = this.fornecedorForm.get('CNPJCPF')?.value || '';
    fornecedor.Nome = this.fornecedorForm.get('Nome')?.value || '';
    fornecedor.Email = this.fornecedorForm.get('Email')?.value || '';
    fornecedor.CEP = this.fornecedorForm.get('CEP')?.value || '';
    fornecedor.DataNascimento = this.fornecedorForm.get('DataNascimento')?.value || null;
    fornecedor.RG = this.fornecedorForm.get('RG')?.value || null;
    fornecedor.TipoPessoa = this.fornecedor.TipoPessoa;
  
    return fornecedor;
  }

  pessoaSelecionada() {
    this.exibirErroTipoPessoa = this.fornecedor.TipoPessoa == null || this.fornecedor.TipoPessoa == undefined ? true : false;
  }

  validarCep() {
    let cep = this.fornecedorForm.get('CEP')?.value?.toString();
    if (cep) {
      this.ValidationService.validarCep(cep).subscribe(
        (resultado) => {
          console.log(resultado)
          this.cepExiste = resultado;
        }
      );
    }
  }

  tipoPessoaSelecionadaFisica() {
    return this.fornecedor.TipoPessoa == TipoPessoa.PessoaFisica.valueOf();
  }

  verificarMaiorIdade(date: string) {
    const dataNascimentoDate = new Date(date);

    const dataAtual = new Date();

    const diferencaAnos = dataAtual.getFullYear() - dataNascimentoDate.getFullYear();

    if (diferencaAnos > 18) {
      return true; 
    } else if (diferencaAnos === 18) {
      if (
        dataAtual.getMonth() > dataNascimentoDate.getMonth() ||
        (dataAtual.getMonth() === dataNascimentoDate.getMonth() &&
          dataAtual.getDate() >= dataNascimentoDate.getDate())
      ) {
        return true;
      }
    }
    return false; 
  }

  emailValidator(control: AbstractControl): ValidationErrors | null {
    const email = control.value as string;
  
    if (email && !email.endsWith('.com')) {
      return { email: 'O endereço de e-mail não é válido.' };
    }
  
    return null;
  }
}
