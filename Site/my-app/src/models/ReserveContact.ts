//Represent a Reserve and Contact server adapter model.
export class ReserveContact {
    public reserveInfo: Reserve;
    public contactInfo: Contact;
    public formatedSchedule: string;
}

///Represent a Reserve server model.
export class Reserve {
    id: number;
    schedule: Date;
    contactId: number;
    description: string;
    ranking: number;
    favorite: boolean;
}

//Represent a Contact server model.
export class Contact {
    id: number;
    name: string;
    phone: string;
    birthDate: Date;
    contactTypeId: number;
    logo: string;
}