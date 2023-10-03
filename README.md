# Projeto Desafio Fullstack Accenture

![Logo do Projeto](https://logodownload.org/wp-content/uploads/2014/05/accenture-logo-4.png)

Este é um projeto de teste para DEV na Accenture, feito 31/09 ~ 01/10, 


## CI
  [![.NET](https://github.com/Wowtz/accenture-dev/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Wowtz/accenture-dev/actions/workflows/dotnet.yml)
  [![Docker Image CI](https://github.com/Wowtz/accenture-dev/actions/workflows/docker-image.yml/badge.svg)](https://github.com/Wowtz/accenture-dev/actions/workflows/docker-image.yml)

## Tecnologias usadas
  Usados .Net 7, Angular 11 e SQL Server.

## Desafio: Full-Stack

O objetivo deste teste é entender suas habilidades de desenvolvimento, estética e técnicas.

### 1. Entidades bases:

#### a. Empresa
   - CNPJ
   - Nome Fantasia
   - CEP

#### b. Fornecedor
   - CNPJ ou CPF
   - Nome
   - E-mail
   - CEP

### 2. Requisitos

#### a. CRUD de todas as entidades (Front-end e Back-end)

#### b. Uma empresa pode ter mais de um fornecedor

#### c. Um fornecedor pode trabalhar para mais de uma empresa

#### d. O CNPJ e CPF devem ser valores únicos

#### e. Caso o fornecedor seja pessoa física, também é necessário cadastrar o RG e a data de nascimento

#### f. Caso a empresa seja do Paraná, não permitir cadastrar um fornecedor pessoa física menor de idade

#### g. A listagem de fornecedores deverá conter filtros por Nome e CPF/CNPJ

#### h. Validar CEP na API [http://cep.la/api](http://cep.la/api), a validação também deve ser feita no Front-end

#### i. Pode adicionar novas colunas, classes, heranças, entidades de relacionamentos e demais recursos que julgar necessário

#### j. Teste de unidade (opcional)

#### k. Implementar Dockerfile (opcional)

## Recursos Principais
- Usada a API https://viacep.com.br/ws/, api recomendada pelo dessafio não se encontrava disponível
- Usado EntityFrameWork para Banco de dados
- Usado PrimeNG para estilização do Front
- Os testes foram feitos em xUnit e somente para as principais controllers

## Como Usar

1. Clone o repositório: `git clone https://github.com/seu-usuario/seu-projeto.git`
2. Instale as dependências: `npm install`
3. Execute o projeto: `npm start`
4. Existe o sln na raíz do projeto para os testes e solução principal
5. É necessário que se rode as migrations do banco (estão todas já mapeadas)

---

Feito com ❤️ por [Walter de Camargo](https://github.com/Wowtz)
