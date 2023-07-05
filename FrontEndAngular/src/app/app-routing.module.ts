import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClassesComponent } from './Components/classes/classes.component';
import { TiposComponent } from './Components/tipos/tipos.component';
import { IndicadoTagsComponent } from './Components/indicado-tags/indicado-tags.component';
import { ContraindicadotagsComponent } from './Components/contraindicadotags/contraindicadotags.component';
import { MedicamentosComponent } from './Components/medicamentos/medicamentos.component';

const routes: Routes = [
  {
    path: 'classes', component: ClassesComponent
  },
  {
    path: 'tipos', component: TiposComponent
  },
  {
    path: 'indicadotags', component: IndicadoTagsComponent
  },
  {
    path: 'contraindicadotags', component: ContraindicadotagsComponent
  },
  {
    path: 'medicamentos', component: MedicamentosComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
