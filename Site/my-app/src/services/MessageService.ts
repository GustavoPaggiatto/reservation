///This service encapsulate message resource, in this case 'Swal'.
///When we like to change 'Swal' to another we only change this functions (encapsulation).

import { ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import Swal from 'sweetalert2/dist/sweetalert2.js';

///show error function...
export function showError(message: string): void {
    Swal.fire({
        title: "Ops...",
        text: message,
        icon: "error",
        type: "error"
    });
}

///show success function...
///here we have 'callback' parameter that correponding a anything action after server operation.
export function showSuccess(message: string, callback: () => void) {
    if (callback != null) {
        Swal.fire({
            title: 'Success!',
            text: message,
            icon: "success",
            type: "success",
            onClose: function () {
                callback();
            }
        });
    }
    else {
        Swal.fire({
            title: 'Success!',
            text: message,
            icon: "success",
            type: "success"
        });
    }
}

export function showQuestion(callback: () => void) {
    Swal.fire({
        title: $localize`:@@questionMessage:Do you really want to proceed?`,
        icon: 'question',
        iconHtml: 'question',
        confirmButtonText: 'Yes',
        cancelButtonText: 'No',
        showCancelButton: true,
        showCloseButton: true
    }).then((result) => {
        if (result.isConfirmed)
            callback();
    });
}