import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TextInputComponent } from './components/text-input/text-input.component';
import {ReactiveFormsModule} from '@angular/forms';
import { DateInputComponent } from './components/date-input/date-input.component';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import {NgxMaskModule} from 'ngx-mask';
import {BsDropdownModule} from 'ngx-bootstrap/dropdown';
import {TabsModule} from 'ngx-bootstrap/tabs';
import {ModalModule} from 'ngx-bootstrap/modal';
import { PaginationComponent } from './components/pagination/pagination.component';
import {PaginationModule} from 'ngx-bootstrap/pagination';



@NgModule({
  declarations: [
    TextInputComponent,
    DateInputComponent,
    PaginationComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgxMaskModule.forRoot(),
    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ModalModule.forRoot(),
    PaginationModule.forRoot()
  ],
  exports: [
    TextInputComponent,
    ReactiveFormsModule,
    TextInputComponent,
    BsDatepickerModule,
    DateInputComponent,
    NgxMaskModule,
    BsDropdownModule,
    TabsModule,
    ModalModule,
    PaginationModule,
    PaginationComponent
  ]
})
export class SharedModule { }
