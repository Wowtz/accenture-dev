import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { FornecedorService } from 'src/app/services/fornecedor-service';
import { Fornecedor } from 'src/app/models/fornecedor.vm';
import { ValidacaoCEP } from 'src/app/models/validacao-cep.vm';
import { TipoPessoa } from 'src/app/enums/tipo-pessoa.enum';
import { ValidationService } from 'src/app/services/validation-service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-editar-fornecedor',
  templateUrl: './editar-fornecedor.component.html',
  styleUrls: ['./editar-fornecedor.component.css']
})
export class EditarFornecedorComponent implements OnInit {
  fornecedorForm!: FormGroup;
  fornecedor = new Fornecedor();
  TipoPessoa = TipoPessoa;
  exibirErroTipoPessoa: boolean = false;
  cepExiste!: ValidacaoCEP;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fornecedorService: FornecedorService,
    private ValidationService : ValidationService,
    private messageService: MessageService,
  ) {}

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id')!;
    const id = parseInt(idParam, 10);

    this.fornecedorForm = new FormGroup({
      CNPJCPF: new FormControl('', [Validators.required, Validators.pattern(/^\d{3}\.\d{3}\.\d{3}-\d{2}$|^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$/)]),
      Nome: new FormControl('', [Validators.required]),
      Email: new FormControl('', [Validators.required, Validators.email, this.emailValidator]),
      CEP: new FormControl('', [Validators.required, Validators.pattern(/^\d{5}-\d{3}$/)]),
      DataNascimento: new FormControl(''),
      RG: new FormControl(''),
      TipoPessoa: new FormControl('')
    });
  
    this.fornecedorService.pegarPorId(id).subscribe((fornecedor) => {
      this.fornecedor = fornecedor;
  
      if (this.fornecedor) {
        this.fornecedorForm.patchValue({
          Nome: this.fornecedor.Nome,
          CNPJCPF: this.fornecedor.CNPJCPF,
          Email: this.fornecedor.Email,
          CEP: this.fornecedor.CEP,
          DataNascimento: this.fornecedor.DataNascimento,
          RG: this.fornecedor.RG,
          TipoPessoa: this.fornecedor.TipoPessoa
        });
      }
    });

    this.validarCep();
  }

  mostrarMensagemAlerta(message: string) {
    this.messageService.add({ severity: 'error', summary: 'Erro', detail: message });
  }

  mostrarMensagemSucesso(message: string) {
    this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: message });
  }

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

  atualizarFornecedor() {
    if (this.fornecedorForm.valid) {
      let fornecedor: Fornecedor = {
        ...this.fornecedor,
        CNPJCPF: this.fornecedorForm.get('CNPJCPF')?.value,
        Nome: this.fornecedorForm.get('Nome')?.value,
        Email: this.fornecedorForm.get('Email')?.value,
        CEP: this.fornecedorForm.get('CEP')?.value,
        TipoPessoa: this.fornecedor.TipoPessoa,
      };

      if (this.fornecedor.TipoPessoa == TipoPessoa.PessoaFisica.valueOf()) {
        fornecedor.DataNascimento = this.fornecedorForm.get('DataNascimento')?.value;
        fornecedor.RG = this.fornecedorForm.get('RG')?.value;
      }

      if (this.fornecedor.TipoPessoa == TipoPessoa.PessoaJuridica.valueOf()) {
        fornecedor.DataNascimento = null;
        fornecedor.RG = null;
      }

      this.fornecedorService.atualizar(this.fornecedor.Id!, fornecedor).subscribe(resposta => {
        this.mostrarMensagemSucesso(`Fornecedor ${resposta.Nome} atualizado com sucesso.`);
      })

      this.router.navigate(['/fornecedores']);
    }
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
