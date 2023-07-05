import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ClassesService } from './Services/classes.service';
import { ClassesComponent } from './Components/classes/classes.component';
import { TiposService } from './Services/tipos.service';
import { TiposComponent } from './Components/tipos/tipos.component';
import { IndicadotagsService } from './Services/indicadotags.service';
import { IndicadoTagsComponent } from './Components/indicado-tags/indicado-tags.component';
import { ContraindicadotagsService } from './Services/contraindicadotags.service';
import { ContraindicadotagsComponent } from './Components/contraindicadotags/contraindicadotags.component';
import { MedicamentosService } from './Services/medicamentos.service';
import { MedicamentosComponent } from './Components/medicamentos/medicamentos.component';
@NgModule({
  declarations: [
    AppComponent,
    ClassesComponent,
    TiposComponent,
    IndicadoTagsComponent,
    ContraindicadotagsComponent,
    MedicamentosComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    ModalModule.forRoot()
  ],
  providers: [HttpClientModule, ClassesService, TiposService, IndicadotagsService,
    ContraindicadotagsService, MedicamentosService],
  bootstrap: [AppComponent]
})
export class AppModule { }
