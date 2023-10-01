import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { EmpresaService } from 'src/app/services/empresa-service'; // Importe o serviço da empresa
import { Empresa } from 'src/app/models/empresa.vm'; // Importe o modelo de empresa
import { ValidacaoCEP } from 'src/app/models/validacao-cep.vm';
import { ValidationService } from 'src/app/services/validation-service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-editar-empresa',
  templateUrl: './editar-empresa.component.html',
  styleUrls: ['./editar-empresa.component.css']
})
export class EditarEmpresaComponent implements OnInit {
  empresaForm!: FormGroup;
  empresa = new Empresa();
  exibirErroTipoPessoa: boolean = false;
  cepExiste!: ValidacaoCEP;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private empresaService: EmpresaService,
    private validationService: ValidationService,
    private messageService: MessageService,
  ) {}

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id')!;
    const id = parseInt(idParam, 10);

    this.empresaForm = new FormGroup({
      CNPJ: new FormControl('', [Validators.required, Validators.pattern(/^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$/)]),
      NomeFantasia: new FormControl('', [Validators.required]),
      CEP: new FormControl('', [Validators.required, Validators.pattern(/^\d{5}-\d{3}$/)]),
    });
  
    this.empresaService.pegarPorId(id).subscribe((empresa) => {
      this.empresa = empresa;
  
      if (this.empresa) {
        this.empresaForm.patchValue({
          CNPJ: this.empresa.CNPJ,
          NomeFantasia: this.empresa.NomeFantasia,
          CEP: this.empresa.CEP,
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
    },
  };

  atualizarEmpresa() {
    console.log('tteste teste')
    console.log('tteste teste', this.empresaForm.valid)

    if (this.empresaForm.valid) {
      if (!this.cepExiste?.cepValido) { 
        this.mostrarMensagemAlerta("CEP inválido.");
        return 
      }

      let empresa: Empresa = {
        ...this.empresa,
        CNPJ: this.empresaForm.get('CNPJ')?.value,
        NomeFantasia: this.empresaForm.get('NomeFantasia')?.value,
        CEP: this.empresaForm.get('CEP')?.value,
      };

      this.empresaService.atualizar(this.empresa.Id!, empresa).subscribe(resposta => {
        this.mostrarMensagemSucesso(`Empresa ${resposta.NomeFantasia} atualizada com sucesso.`);
      })

      this.router.navigate(['/empresas']);
    }
  }

  validarCep() {
    let cep = this.empresaForm.get('CEP')?.value?.toString();
    if (cep) {
      this.validationService.validarCep(cep).subscribe(
        (resultado) => {
          this.cepExiste = resultado;
        }
      );
    }
  }
}
