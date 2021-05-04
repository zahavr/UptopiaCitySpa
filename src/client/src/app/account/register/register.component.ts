import {Component, OnInit} from '@angular/core';
import {AsyncValidatorFn, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {AccountService} from '../account.service';
import {Router} from '@angular/router';
import validate = WebAssembly.validate;
import {of, timer} from 'rxjs';
import {buffer, map, switchMap} from 'rxjs/operators';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  errors: string[];

  constructor(private fb: FormBuilder,
              private accountService: AccountService,
              private router: Router) {
  }

  ngOnInit(): void {
    this.createRegistrationForm();
  }

  createRegistrationForm(): void {
    this.registerForm = this.fb.group({
      login: new FormControl('', [Validators.required]),
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email], [this.validateEmailNotTaken()]),
      phoneNumber: new FormControl('', [Validators.required]),
      birthDate: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required, Validators.pattern(this.passwordRegex())])
    });
  }

  onSubmit(): void {
    this.accountService.register(this.registerForm.value).subscribe(() => {
      this.router.navigateByUrl('/');
    }, error => {
      console.log(error);
    });
  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return control => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) {
            return of(null);
          }
          return this.accountService.checkEmailExists(control.value).pipe(
            map(res => {
              return res ? {emailExists: true} : null;
            })
          );
        })
      );
    };
  }

  private passwordRegex(): string {
    return '(?=^.{8,}$)(?=.*\\d)(?=.*[!@#$%^&*]+)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$';
  }
}
