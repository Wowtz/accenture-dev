import { NgModule, CUSTOM_ELEMENTS_SCHEMA  } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './geral/header/header.component';
import { MenubarModule } from 'primeng/menubar';
import { MenuModule } from 'primeng/menu';
import { TabMenuModule } from 'primeng/tabmenu';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { ButtonModule } from 'primeng/button';
import { CadastroFornecedorComponent } from './cadastros/cadastro-fornecedor/cadastro-fornecedor.component';
import { EmpresaService } from './services/empresa-service';
import { HttpClientModule } from '@angular/common/http';
import { TableModule } from 'primeng/table';
import { FornecedorService } from './services/fornecedor-service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { KeyFilterModule } from 'primeng/keyfilter';
import { DialogModule } from 'primeng/dialog';
import { CadastroEmpresaComponent } from './cadastros/cadastro-empresa/cadastro-empresa.component';
import { AdicionarEmpresaComponent } from './Adicionar/adicionar-empresa/adicionar-empresa.component';
import { AdicionarFornecedorComponent } from './Adicionar/adicionar-fornecedor/adicionar-fornecedor.component';
import { CommonModule } from '@angular/common';
import { InputTextModule } from 'primeng/inputtext';
import { DividerModule } from 'primeng/divider';
import { RadioButtonModule } from 'primeng/radiobutton';
import { CalendarModule } from 'primeng/calendar';
import { CpfCnpjFormatDirective } from './directive/CpfCnpjFormatDirective';
import { CepFormatDirective } from './directive/CepFormatDirective';
import { ValidationService } from './services/validation-service';
import { TagModule } from 'primeng/tag';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { EditarFornecedorComponent } from './editar/editar-fornecedor/editar-fornecedor.component';
import { EditarEmpresaComponent } from './editar/editar-empresa/editar-empresa.component';
import { VincularEmpresaComponent } from './Vincular/vincular-empresa/vincular-empresa.component';
import { VinculacaoService } from './services/vinculacao-service';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { VincularFornecedorComponent } from './Vincular/vincular-fornecedor/vincular-fornecedor.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    CadastroFornecedorComponent,
    CadastroEmpresaComponent,
    AdicionarEmpresaComponent,
    AdicionarFornecedorComponent,
    CpfCnpjFormatDirective,
    CepFormatDirective,
    EditarFornecedorComponent,
    EditarEmpresaComponent,
    VincularEmpresaComponent,
    VincularFornecedorComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MenubarModule,
    MenuModule,
    ToggleButtonModule,
    TabMenuModule,
    BrowserAnimationsModule,
    TabMenuModule,
    ButtonModule,
    HttpClientModule,
    TableModule,
    FormsModule,
    KeyFilterModule,
    DialogModule,
    CommonModule,
    FormsModule,
    InputTextModule,
    ButtonModule,
    TagModule,
    RadioButtonModule,
    DividerModule,
    ReactiveFormsModule,
    CalendarModule,
    ToastModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA], 
  providers: [
    EmpresaService, 
    FornecedorService,
    ValidationService,
    MessageService,
    VinculacaoService,
    
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
