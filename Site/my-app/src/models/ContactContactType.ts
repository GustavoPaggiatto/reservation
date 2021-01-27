import { ContactType } from "./ContactType";
import { Contact } from "./ReserveContact";

///Represent an server adapter for Contact and Contact Types.
export class ContactContactType {
    contact: Contact;
    contactType: ContactType;
    birthDateFormated: string;
}