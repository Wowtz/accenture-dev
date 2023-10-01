import { TipoPessoa } from "../enums/tipo-pessoa.enum";

export class Fornecedor {
    Id?: number;
    CNPJCPF: string;
    Nome: string;
    Email: string;
    CEP: string;
    RG?: string | null;
    DataNascimento?: string | null;
    TipoPessoa?: TipoPessoa ;

    constructor(){
        this.CNPJCPF = '',
        this.Nome = '',
        this.Email = '',
        this.CEP = ''
    }
}