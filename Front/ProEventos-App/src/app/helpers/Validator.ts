import { AbstractControl, FormGroup } from '@angular/forms';

export class Validator {
  static MustMatch(controlName: string, matchingControlName: string): any{
    return (group: AbstractControl) => {
      const frmGroup = group as FormGroup;
      const control = frmGroup.controls[controlName];
      const matchingControl = frmGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors.mustMatch){
        return null;
      }

      if (control.value !== matchingControl.value){
        matchingControl.setErrors({mustMatch: true});
      }
      else{
        matchingControl.setErrors(null);
      }
      return null;
    };

  }
}
