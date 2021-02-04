import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { ListReservationComponent } from './list-reservation/list-reservation.component';
import { RatingModule } from 'ng-starrating';
import { JwPaginationModule } from 'jw-angular-pagination';
import { ContactComponent } from './contact/contact.component';
import { DpDatePickerModule } from 'ng2-date-picker';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSortModule } from '@angular/material/sort';
import { NgxLoadingModule } from 'ngx-loading';
import { ReservationComponent } from './reservation/reservation.component';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { RichTextEditorAllModule } from '@syncfusion/ej2-angular-richtexteditor';

@NgModule({
  declarations: [
    AppComponent,
    ListReservationComponent,
    ContactComponent,
    ReservationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    RatingModule,
    JwPaginationModule,
    DpDatePickerModule,
    BrowserAnimationsModule,
    MatDatepickerModule,
    MatInputModule,
    MatNativeDateModule,
    MatSortModule,
    MatAutocompleteModule,
    FormsModule,
    RichTextEditorAllModule,
    NgxLoadingModule.forRoot({})
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
