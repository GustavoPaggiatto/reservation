import { Component, OnInit } from '@angular/core';
import { DynamicLink } from 'src/models/DynamicLink';
import { SharedDefaultService } from '../services/SharedDefaultService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  private _sharedHeaderService: SharedDefaultService;

  _dynamicLink: DynamicLink;
  _navTitle: string;

  //Constructor of a class.
  //Here we get all necessary share data for bind view component.
  constructor(
    shareDefService: SharedDefaultService,
    sharedNavService: SharedDefaultService) {
    this._sharedHeaderService = shareDefService;

    this._sharedHeaderService.getEmittedValue()
      .subscribe(item => this._dynamicLink = item);

    this._sharedHeaderService.getNav()
      .subscribe(item => this._navTitle = item);
  }

  ngOnInit() {

  }
}
