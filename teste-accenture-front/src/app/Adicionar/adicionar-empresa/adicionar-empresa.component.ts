// adicionar-empresa.component.ts
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { EmpresaService } from 'src/app/services/empresa-service';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { Empresa } from 'src/app/models/empresa.vm';
import { ValidacaoCEP } from 'src/app/models/validacao-cep.vm';
import { ValidationService } from 'src/app/services/validation-service';

@Component({
  selector: 'app-adicionar-empresa',
  templateUrl: './adicionar-empresa.component.html',
  styleUrls: ['./adicionar-empresa.component.css']
})
export class AdicionarEmpresaComponent implements OnInit {
  empresa = new Empresa();
  cepExiste?: ValidacaoCEP;

  constructor(
    private empresaService: EmpresaService,
    private messageService: MessageService,
    private ValidationService : ValidationService,
    private router: Router
  ) {}

  empresaForm = new FormGroup({
    CNPJ: new FormControl('', [Validators.required, Validators.pattern(/^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$/)]),
    NomeFantasia: new FormControl('', [Validators.required]),
    CEP: new FormControl('', [Validators.required, Validators.pattern(/^\d{5}-\d{3}$/)])
  });

  errorMessages = {
    CNPJ: {
      required: 'Campo CNPJ é obrigatório.',
      pattern: 'O CNPJ deve estar no formato correto.'
    },
    NomeFantasia: {
      required: 'Campo Nome Fantasia é obrigatório.'
    },
    CEP: {
      required: 'Campo CEP é obrigatório.',
      pattern: 'O CEP deve estar no formato correto.'
    }
  };

  ngOnInit(): void {}

  adicionarEmpresa() {
    this.validarCep();
    if (this.empresaForm.valid) {
      if (!this.cepExiste?.cepValido) { 
        this.mostrarMensagemAlerta("CEP inválido.");
        return 
      }
      this.empresa = this.criarEmpresaAPartirDoFormulario();

      this.empresaService.adicionar(this.empresa).subscribe(
        (resposta) => {
          this.mostrarMensagemSucesso('Empresa adicionada com sucesso.');
          this.router.navigate(['/empresas']);
        },
        (error) => {
          this.mostrarMensagemAlerta('Erro ao adicionar a empresa.');
        }
      );
    }
  }

  criarEmpresaAPartirDoFormulario(): Empresa {
    const empresa = new Empresa();
    empresa.CNPJ = this.empresaForm.get('CNPJ')?.value || '';
    empresa.NomeFantasia = this.empresaForm.get('NomeFantasia')?.value || '';
    empresa.CEP = this.empresaForm.get('CEP')?.value || '';

    return empresa;
  }

  mostrarMensagemAlerta(message: string) {
    this.messageService.add({ severity: 'error', summary: 'Erro', detail: message });
  }

  mostrarMensagemSucesso(message: string) {
    this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: message });
  }

  validarCep() {
    let cep = this.empresaForm.get('CEP')?.value?.toString();
    if (cep) {
      this.ValidationService.validarCep(cep).subscribe(
        (resultado) => {
          console.log(resultado)
          this.cepExiste = resultado;
        }
      );
    }
  }
}
