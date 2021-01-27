import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListReservationComponent } from './list-reservation/list-reservation.component';
import { ContactComponent } from './contact/contact.component';
import { ReservationComponent } from './reservation/reservation.component';

const routes: Routes = [
  { path: '', component: ListReservationComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'reservation', component: ReservationComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
