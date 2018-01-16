import * as Raven from 'raven-js';
import { ErrorHandler, Inject, NgZone, isDevMode } from "@angular/core";
import { ToastyService } from "ng2-toasty";


export class AppErrorHandler implements ErrorHandler {

/**
 *
 */
constructor(
    @Inject(ToastyService) private toastyService: ToastyService,
    @Inject(NgZone) private ngZone: NgZone) {

}

handleError(error: any): void {
    if (!isDevMode)
        Raven.captureException(error.originalError || error);
    else throw error;

    this.ngZone.runOutsideAngular(() => {
        this.toastyService.error({
            title: 'Error',
            msg: 'Some error',
            theme: 'bootstrap',
            showClose: true,
            timeout: 5000
        });
    })
}
    
}