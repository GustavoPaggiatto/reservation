//Represent a Result server enveloper.
export class Result {
    messages: string[];
    error: boolean;
    success: boolean;
}

//Represent a Result server enveloper with 'content' generic type.
export class ResultContent<T> extends Result {
    content: T;
}