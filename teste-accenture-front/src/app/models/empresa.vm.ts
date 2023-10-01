export class Empresa {
    Id?: number;
    CNPJ: string;
    NomeFantasia: string;
    CEP: string;

    constructor(){
        this.CNPJ = '',
        this.NomeFantasia = '',
        this.CEP = ''
    }
}