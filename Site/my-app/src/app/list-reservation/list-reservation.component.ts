import { Component, OnInit } from '@angular/core';
import { DynamicLink } from 'src/models/DynamicLink';
import { SharedDefaultService } from '../../services/SharedDefaultService';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { ConfigService } from 'src/services/ConfigService';
import { ReserveContact } from 'src/models/ReserveContact';
import { Result, ResultContent } from 'src/models/Result';
import { StarRatingComponent } from 'ng-starrating';
import { JwPaginationComponent } from 'jw-angular-pagination';
import { showError } from '../../services/MessageService';

@Component({
  selector: 'app-list-reservation',
  templateUrl: './list-reservation.component.html',
  styleUrls: ['./list-reservation.component.css']
})
export class ListReservationComponent implements OnInit {
  private _sharedDefService: SharedDefaultService;
  private _http: HttpClient;
  private _config: ConfigService;
  _reservs: ResultContent<ReserveContact[]>;
  _pageOfItems: ReserveContact[] = [];
  _loading: boolean;
  _orderType: number;

  //Constructor of a class.
  //Here we set all necessary share data with app component and do all necessary load ajax requests.
  constructor(shareDefService: SharedDefaultService, http: HttpClient, config: ConfigService) {
    this._http = http;
    this._config = config;
    this._sharedDefService = shareDefService;

    var modelHeader = new DynamicLink();
    modelHeader.text = $localize`:@@createReservation:Create Reservation`;
    modelHeader.href = "/reservation";

    this._sharedDefService.change(modelHeader);
    this._sharedDefService.setNav($localize`:@@listReservation:Reservation List`);

    this._loading = true;
    this._orderType = 0;

    //ajax 'GET' http request for loading all reserve/contact (server adaptee model) effetuated...
    //ajax was implemented with error pipe ('error => {}').
    this._http.get<ResultContent<ReserveContact[]>>(this._config.getBaseUrl() + "Reserve/GetAll")
      .subscribe(
        result => {
          this._loading = false;

          if (result.error) {
            showError(result.messages[0]);
            this._reservs.content = [];
            return;
          }

          this._reservs = result;
        },
        error => {
          this._loading = false;

          console.log("Error while 'GET' request (view logs for more details)...");
          showError($localize`:@@errorGetReserves:An error occuring while get reserves, please retry.`);
        });
  }

  //Set ranking of reserve with stars change value
  //This method is used by 'StarRatingComponent' component.
  onRate($event: {
    oldValue: number;
    newValue: number;
    starRating: StarRatingComponent;
  }, reserveId: number) {
    //Ajax 'POST' request for seting ranking with error pipe.
    this._http.post<Result>(this._config.getBaseUrl() + "Reserve/SetRanking", {
      id: reserveId,
      ranking: $event.newValue
    })
      .subscribe(
        result => {
          console.log("Ranking was saved (reserve: " + reserveId + ").");

          if (result.error) {
            for (var reserve of this._reservs.content) {
              if (reserve.reserveInfo.id == reserveId) {
                reserve.reserveInfo.ranking = $event.oldValue;
                break;
              }
            }

            showError(result.messages[0]);
            return;
          }

          for (var reserve of this._reservs.content) {
            if (reserve.reserveInfo.id == reserveId) {
              reserve.reserveInfo.ranking = $event.newValue;
              break;
            }
          }
        },
        error => {
          console.log("Error while 'POST' ranking request (view logs for more details)...");

          for (var reserve of this._reservs.content) {
            if (reserve.reserveInfo.id == reserveId) {
              reserve.reserveInfo.ranking = $event.oldValue;
              break;
            }
          }

          showError($localize`:@@errorSetRanking:An error occuring while set ranking, please retry.`);
        });
  }

  //Set favorite, or not, of reserve on favorite icon was clicked.
  doFavorite(reserveId: number, oldFavotire: boolean) {
    //ajax 'POST' http request with error pipe...
    this._http.post<Result>(this._config.getBaseUrl() + "Reserve/SetFavorite", {
      id: reserveId,
      favorite: oldFavotire
    })
      .subscribe(
        result => {
          if (result.error) {
            showError(result.messages[0]);
            return;
          }

          console.log("Favorite was saved (reserve: " + reserveId + ").");

          for (var reserve of this._reservs.content) {
            if (reserve.reserveInfo.id == reserveId) {
              reserve.reserveInfo.favorite = oldFavotire;
              break;
            }
          }
        },
        error => {
          console.log("Error while 'POST' favorite request (view logs for more details)...");
          showError($localize`:@@errorSetFavorite:An error occuring while set favorite, please retry.`);
        });
  }

  //This method is used by pagination component ('JwPaginationComponent').
  onChangePage(pageOfItems: ReserveContact[]) {
    this._pageOfItems = pageOfItems;
  }

  order() {
    if (this._orderType == 0 || this._reservs.content.length == 0)
      return;

    this._reservs.content = this._reservs.content.sort((a, b) => {
      if (this._orderType == 1) { //date asc
        return this.compare(a.reserveInfo.schedule, b.reserveInfo.schedule, true);
      }
      else if (this._orderType == 2) { //date desc
        return this.compare(a.reserveInfo.schedule, b.reserveInfo.schedule, false);
      }
      else if (this._orderType == 3) { //alpha asc
        return this.compare(a.contactInfo.name, b.contactInfo.name, true);
      }
      else if (this._orderType == 4) { //alpha desc
        return this.compare(a.contactInfo.name, b.contactInfo.name, false);
      }
      else { //ranking
        return this.compare(a.reserveInfo.ranking, b.reserveInfo.ranking, true);
      }
    });

    this._pageOfItems = [
      this._reservs.content[0],
      this._reservs.content[1],
      this._reservs.content[2],
      this._reservs.content[3]];
  }

  compare(a: number | string | Date, b: number | string | Date, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  ngOnInit(): void {

  }
}
