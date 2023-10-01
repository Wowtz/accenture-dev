import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CadastroFornecedorComponent } from './cadastros/cadastro-fornecedor/cadastro-fornecedor.component';
import { CadastroEmpresaComponent } from './cadastros/cadastro-empresa/cadastro-empresa.component';
import { AdicionarFornecedorComponent } from './Adicionar/adicionar-fornecedor/adicionar-fornecedor.component';
import { AdicionarEmpresaComponent } from './Adicionar/adicionar-empresa/adicionar-empresa.component';
import { EditarFornecedorComponent } from './editar/editar-fornecedor/editar-fornecedor.component';
import { EditarEmpresaComponent } from './editar/editar-empresa/editar-empresa.component';

const routes: Routes = [
  { path: 'empresas', component: CadastroEmpresaComponent },
  { path: 'empresas/adicionar', component: AdicionarEmpresaComponent },
  { path: 'empresas/editar/:id', component: EditarEmpresaComponent },
  { path: 'fornecedores', component: CadastroFornecedorComponent },
  { path: 'fornecedores/editar/:id', component: EditarFornecedorComponent },
  { path: 'fornecedores/adicionar', component: AdicionarFornecedorComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
