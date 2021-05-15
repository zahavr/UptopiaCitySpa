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
import { HasRoleDirective } from './directives/has-role.directive';
import {TableModule} from 'primeng-lts/table';



@NgModule({
  declarations: [
    TextInputComponent,
    DateInputComponent,
    PaginationComponent,
    HasRoleDirective,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgxMaskModule.forRoot(),
    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    TableModule,
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
        PaginationComponent,
        HasRoleDirective,
        TableModule
    ]
})
export class SharedModule { }
