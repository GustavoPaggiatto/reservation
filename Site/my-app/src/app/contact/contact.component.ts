import { Component, OnInit } from '@angular/core';
import { DynamicLink } from 'src/models/DynamicLink';
import { SharedDefaultService } from 'src/services/SharedDefaultService';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { ConfigService } from 'src/services/ConfigService';
import { DatePickerComponent } from 'ng2-date-picker';
import { ContactType } from 'src/models/ContactType';
import { Result, ResultContent } from 'src/models/Result';
import { Contact } from 'src/models/ReserveContact';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { showError, showSuccess } from '../../services/MessageService';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {
  private _shareDefService: SharedDefaultService;
  private _httpClient: HttpClient;
  private _configService: ConfigService;
  _contactTypes: ContactType[];
  _contactId: number;
  _loading: boolean;
  _name: string;
  _birthdate: Date;
  _contactTypeId: number;
  _phone: string;
  _logo: string;

  //Constructor of a class.
  //Here we set all necessary share data with app component and do all necessary load ajax requests.
  constructor(
    shareDefService: SharedDefaultService,
    http: HttpClient,
    config: ConfigService,
    route: ActivatedRoute) {
    this._shareDefService = shareDefService;
    this._configService = config;
    this._httpClient = http;
    this._loading = false;

    var model = new DynamicLink();
    model.href = "/reservation";
    model.text = $localize`:@@createReservation:Create Reservation`;

    this._shareDefService.change(model);
    this._shareDefService.setNav($localize`:@@createContactComplete:Create Contact.`);

    //ajax http 'GET' request for loading all contact types existing in application repository...
    //this follow error pipe('error {}').
    this._httpClient.get<ResultContent<ContactType[]>>(this._configService.getBaseUrl() + "ContactType/GetAll")
      .subscribe(
        result => {
          if (result.error) {
            showError(result.messages[0]);
            this._contactTypes = [];
            return;
          }

          this._contactTypes = [];

          for (var item of result.content)
            this._contactTypes.push(item);
        },
        error => {
          console.log("Error while 'GET' request (view logs for more details)...");
          showError($localize`:@@errorContactTypes:An error occuring while get contact types, please retry.`);
        });

    //if querystring 'id' was filled, set component in edit mode.
    if (route.snapshot.queryParamMap.get("id") != null) {
      this._contactId = Number(route.snapshot.queryParamMap.get("id"));

      //Ajax http 'GET' request for loading contact data...
      this._httpClient.get<ResultContent<Contact>>(this._configService.getBaseUrl() + "Contact/GetById?id=" + this._contactId)
        .subscribe(
          result => {
            if (result.error) {
              showError(result.messages[0]);
              return;
            }

            this._name = result.content.name;
            this._phone = result.content.phone;
            this._birthdate = result.content.birthDate;
            this._contactTypeId = result.content.contactTypeId;
            this._logo = result.content.logo;
          },
          error => {
            console.log("Error while 'GET' request (view logs for more details)...");
            showError($localize`:@@errorGetContact:An error occuring while get contact, please retry.`);
          });
    }
    else
      this._contactId = 0;

    if (this._logo == null) {
      this._logo = "../../assets/imgs/empty_logo.jpg";
    }
  }

  //Method for sending contact information to the server.
  //Here all necessary validations is implemented according with project requirements (.docx).
  sendContact() {
    //validation...
    if (this._name == null) {
      showError($localize`:@@nameEmpty:Name is empty.`);
      return;
    }

    this._name = this._name.trim();

    if (this._name.length == 0) {
      showError($localize`:@@nameEmpty:Name is empty.`);
      return;
    }

    if (this._birthdate == null) {
      showError($localize`:@@birthDateEmpty:Birthdate is empty or invalid.`);
      return;
    }

    if (this._contactTypeId == null || this._contactTypeId == 0) {
      showError($localize`:@@contactTypeEmpty:Contact Type not selected.`);
      return;
    }

    //show loading...
    this._loading = true;

    this._httpClient.post<Result>(
      this._configService.getBaseUrl() +
      "Contact/" +
      (this._contactId != 0 ? "Edit" : "Create"), {
      id: this._contactId,
      name: this._name,
      birthDate: this._birthdate,
      contactTypeId: Number(this._contactTypeId),
      phone: this._phone,
      logo: this._logo
    }).subscribe(
      result => {
        this._loading = false;

        if (result.error) {
          showError(result.messages[0]);
          return;
        }

        showSuccess($localize`:@@contactCreateSuccess:Contact was saved with success!`, function () {
          window.location.href = "/reservation";
        });
      },
      error => {
        this._loading = false;
        showError($localize`:@@postError:Error occured while 'POST' request (view logs for more details).`);
      }
    );
  }

  //The two methods bellow are responsible for loading selected image from file input, reading your content as
  //'base64' string and show to user preview.
  selectFile(evt) {
    var files = evt.target.files;
    var file = files[0];

    if (files && file) {
      var reader = new FileReader();

      reader.onload = this._handleReaderLoaded.bind(this);
      reader.readAsBinaryString(file);
    }
  }

  _handleReaderLoaded(readerEvt) {
    var binaryString = readerEvt.target.result;
    this._logo = "data:image/png;base64," + btoa(binaryString);
  }

  ngOnInit(): void {
  }

}
