import { HttpClient } from '@angular/common/http';
import { Component, ViewChild, OnInit } from '@angular/core';
import { DynamicLink } from 'src/models/DynamicLink';
import { ConfigService } from 'src/services/ConfigService';
import { SharedDefaultService } from 'src/services/SharedDefaultService';
import { Sort } from '@angular/material/sort';
import { Contact, Reserve } from 'src/models/ReserveContact';
import { ContactType } from 'src/models/ContactType';
import { Result, ResultContent } from 'src/models/Result';
import { ContactContactType } from 'src/models/ContactContactType';
import { JwPaginationComponent } from 'jw-angular-pagination';
import { ToolbarService, LinkService, ImageService, HtmlEditorService, RichTextEditorComponent } from '@syncfusion/ej2-angular-richtexteditor';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { showError, showSuccess, showQuestion } from 'src/services/MessageService';
import { ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { DatePickerComponent } from 'ng2-date-picker';

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.css']
})
export class ReservationComponent implements OnInit {
  private _sharedDefService: SharedDefaultService;
  private _config: ConfigService;
  private _http: HttpClient;
  _loading: boolean;
  _showContacts: boolean;
  _contacts: Contact[];
  _contactsComplete: Contact[];
  _contactTypes: ContactType[];
  _contactContactTypes: ContactContactType[];
  _contactsPerPage: ContactContactType[];
  _contactId: number;
  _contactName: string;
  _contactTypeDescription: string;
  _contactPhone: string;
  _contactDate: string;
  _richValue: string = null;
  _idReserve: number;
  _reserveId: number;
  _reserveRanking: number;
  _reserveFavorite: boolean;
  _router: ActivatedRoute;
  _birthdate: Date;

  @ViewChild('fromRTE')
  private rteEle: RichTextEditorComponent;

  //Constructor of a class.
  //Here we set all necessary share data with app component and do all necessary load ajax requests.
  constructor(
    shareDefService: SharedDefaultService,
    configService: ConfigService,
    httpClient: HttpClient,
    route: ActivatedRoute,
    routerRedirect: Router) {
    this._sharedDefService = shareDefService;
    this._config = configService;
    this._http = httpClient;
    this._loading = false;
    this._showContacts = false;
    this._router = route;

    var modelHeader = new DynamicLink();
    modelHeader.text = $localize`:@@listReservation:Reservation List`;
    modelHeader.href = "/";

    this._sharedDefService.change(modelHeader);
    this._sharedDefService.setNav($localize`:@@reservationOnly:Reservation.`);

    //http GET request for load contact types (with error pipe)...
    this._http.get<ResultContent<ContactType[]>>(this._config.getBaseUrl() + "ContactType/GetAll")
      .subscribe(
        result => {
          if (result.error) {
            showError(result.messages[0]);
            this._contactTypes = [];
            return;
          }

          this._contactTypes = result.content;

          //http GET request for load contacts (with error pipe)...
          this._http.get<ResultContent<Contact[]>>(this._config.getBaseUrl() + "Contact/GetAll")
            .subscribe(
              result => {
                if (result.error) {
                  showError(result.messages[0]);
                  this._contacts = [];
                  return;
                }

                this._contacts = result.content;
                this._contactsComplete = this._contacts;
                this._contactContactTypes = [];

                for (var c of this._contacts) {
                  let ct = this._contactTypes.find(ct => ct.id == c.contactTypeId);
                  let cct = new ContactContactType();

                  cct.contact = c;
                  cct.contactType = ct;
                  cct.birthDateFormated = new Date(c.birthDate).toDateString();

                  this._contactContactTypes.push(cct);
                }

                //http get reserve request...
                this._reserveId = 0;
                this._reserveRanking = 0;
                this._reserveFavorite = false;

                if (route.snapshot.queryParamMap.get("id") != null) {
                  this._reserveId = Number(route.snapshot.queryParamMap.get("id"));

                  //http GET request for load reserve based on 'id' passed via querystring (with error pipe)...
                  this._http.get<ResultContent<Reserve>>(this._config.getBaseUrl() + "Reserve/GetById?id=" + this._reserveId)
                    .subscribe(
                      result => {
                        if (result.error) {
                          showError(result.messages[0]);
                          return;
                        }

                        let item = this._contacts.find(c => c.id == result.content.contactId);
                        let itemType = this._contactTypes.find(ct => ct.id == item.contactTypeId);

                        this._contactId = item.id;
                        this._contactPhone = item.phone;
                        this._contactName = item.name;
                        this._contactDate = new Date(item.birthDate).toLocaleDateString();
                        this._birthdate = item.birthDate;
                        this._contactTypeDescription = itemType.description;

                        this._reserveRanking = result.content.ranking;
                        this._reserveFavorite = result.content.favorite;
                        this._richValue = result.content.description;
                      },
                      error => {
                        console.log("Error while 'GET' request (view logs for more details)...");
                        showError($localize`:@@errorReserveData:An error occuring while get reserve data, please retry.`);
                      }
                    );
                }
              },
              error => {
                this._contacts = [];

                console.log("Error while 'GET' request (view logs for more details)...");
                showError($localize`:@@errorContactData:An error occuring while get contacts, please retry.`);
              }
            );
        },
        error => {
          this._contactTypes = [];

          console.log("Error while 'GET' request (view logs for more details)...");
          showError($localize`:@@errorContactTypes:An error occuring while get contact types, please retry.`);
        });
  }

  //method used by pagination component.
  onChangePage(pageOfItems: ContactContactType[]) {
    this._contactsPerPage = pageOfItems;
  }

  //used to display/hide contact list.
  showContacts() {
    this._showContacts = !this._showContacts;
  }

  //used for sort data in contact list.
  sortData(sort: Sort) {
    const data = this._contactsPerPage.slice();

    if (!sort.active || sort.direction === '') {
      this._contactsPerPage = data;
      return;
    }

    this._contactsPerPage = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';

      switch (sort.active) {
        case 'name': return this.compare(a.contact.name, b.contact.name, isAsc);
        case 'phone': return this.compare(a.contact.phone, b.contact.phone, isAsc);
        case 'birthDate': return this.compare(a.contact.birthDate, b.contact.birthDate, isAsc);
        case 'descType': return this.compare(a.contactType.description, b.contactType.description, isAsc);
        default: return 0;
      }
    });
  }

  compare(a: number | string | Date, b: number | string | Date, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  //used for filterring contacts by name while user input text (autocomplete requirement).
  filter() {
    if (this._contactName.length === 0) {
      this._contactsComplete = this._contacts;
      return;
    }

    this._contactsComplete = this._contacts.filter(contact => contact.name.includes(this._contactName));
  }

  //set contact data when user getout from input contact name.
  setContact() {
    let contact: ContactContactType = this._contactContactTypes.find(c => c.contact.name == this._contactName);

    if (contact != null) {
      this._contactId = contact.contact.id;
      this._contactPhone = contact.contact.phone;
      this._contactDate = new Date(contact.contact.birthDate).toLocaleDateString();
      this._contactTypeDescription = contact.contactType.description;

      if (this._birthdate == null)
        this._birthdate = contact.contact.birthDate;
    }
  }

  //do Reserve method.
  //Here we do necessary validations and, after this, 'POST' ajax http request to the server.
  doReserve() {
    if (this._contactId == null || this._contactId == 0) {
      showError($localize`:@@contactNotSelected:Contact was not selected.`);
      return;
    }

    if (this._richValue == null || this._richValue.length == 0) {
      showError($localize`:@@reservaDescriptionNotFill:Reserve description is not fill.`);
      return;
    }

    this._loading = true;
    let contact: ContactContactType = this._contactContactTypes.find(c => c.contact.name == this._contactName);

    let dateTemporary = this._birthdate;
    dateTemporary.setDate(dateTemporary.getDate() + 1);

    this._http.post<Result>(this._config.getBaseUrl() + (this._reserveId == 0 ? "Reserve/Create" : "Reserve/Edit"), {
      id: Number(this._reserveId),
      contactId: Number(this._contactId),
      description: this._richValue,
      schedule: dateTemporary,
      ranking: this._reserveRanking,
      favorite: this._reserveFavorite
    })
      .subscribe(
        result => {
          this._loading = false;

          if (result.error) {
            showError(result.messages[0]);
            return;
          }

          showSuccess($localize`:@@reservaCreationSuccess:Reservation was created with success!`, function () {
            window.location.href = "/";
          });
        },
        error => {
          this._loading = false;

          console.log("Error while 'POST' request (view logs for more details)...");
          showError($localize`:@@errorCreateReserve:An error occuring while sending reserve, please retry.`);
        }
      );
  }

  //Called on button 'Delete' was cliked in contact list.
  //here we dispath a confirmation message before do ajax http 'POST' request.
  //It is also worth mentioning that error pipe of ajax is implemented.
  deleteContact(contactId: number) {
    showQuestion(() => {
      this._loading = true;

      this._http.post<Result>(this._config.getBaseUrl() + "Contact/Delete", {
        id: contactId
      })
        .subscribe(
          result => {
            this._loading = false;

            if (result.error) {
              showError(result.messages[0]);
              return;
            }

            var ixDeleted = this._contactContactTypes.findIndex(ix => ix.contact.id == contactId);
            this._contactContactTypes.splice(ixDeleted, 1);

            ixDeleted = this._contactsPerPage.findIndex(ix => ix.contact.id == contactId);
            this._contactsPerPage.splice(ixDeleted, 1);

            showSuccess($localize`:@@contactDeleteSuccess:Contact was deleted with success!`, function () {
              console.log("contact was deleted (id: " + contactId + ")");
            });
          },
          error => {
            this._loading = false;

            console.log("Error while 'POST' request (view logs for more details)...");
            showError($localize`:@@errorContactCreate:An error occuring while deleting contact, please retry.`);
          }
        );
    });
  }

  ngOnInit(): void {
  }
}
