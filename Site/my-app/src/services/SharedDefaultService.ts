import { Component, Injectable, Input, Output, EventEmitter } from '@angular/core';
import { DynamicLink } from '../models/DynamicLink';

@Injectable({
    providedIn: 'root'
})
//This service is used for sharing common data between componentes (app.component and childrens).
export class SharedDefaultService {
    @Output() fire: EventEmitter<DynamicLink> = new EventEmitter(); //emitter for sharing link parameters
    @Output() navFire: EventEmitter<string> = new EventEmitter(); // emitter for sharing title to base component

    constructor() {
        console.log('shared service started');
    }

    //dispatch for 'fire' event emitter
    change(value: DynamicLink) {
        console.log('change started');
        this.fire.emit(value);
    }

    //getter of 'fire' event emitter result
    getEmittedValue() {
        return this.fire;
    }

    //dispatch for 'navFire' event emitter
    setNav(value: string) {
        console.log('nav started');
        this.navFire.emit(value);
    }

    //getter of 'navFire' event emitter result
    getNav() {        
        return this.navFire;
    }
} 