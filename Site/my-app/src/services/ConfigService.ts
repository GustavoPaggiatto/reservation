import { Injectable } from '@angular/core';
@Injectable({
    providedIn: 'root'
})
//Represent a Service for any necessary configurations
export class ConfigService {
    ///return base configuration of 'url' for http ajax calls
    getBaseUrl(): string {
        return "https://localhost:44351/";
    }
}