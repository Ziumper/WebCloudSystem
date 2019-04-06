import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { AlertService } from 'src/app/services/alert.service';
import { ActivationModel } from 'src/app/models/activation.model';

@Component({
    selector: 'app-activation',
    templateUrl: './activation.component.html',
    styleUrls: ['./activation.component.css']
})
export class ActivationComponent implements OnInit {
    loading = false;
    submitted = false;
    registerForm: FormGroup;
    private activationModel: ActivationModel;
    private userId: number;

    constructor(private formBuilder: FormBuilder,
        private router: Router,
        private userService: UserService,
        private alertService: AlertService,
        private activatedRoute: ActivatedRoute) {
        this.userId = 0;
        this.activationModel = new ActivationModel();
    }


    ngOnInit(): void {
        this.userId = this.activatedRoute.snapshot.params['id'];
        this.registerForm = this.formBuilder.group({
            code: ['', [Validators.required, Validators.minLength(4)]],
        });
    }

    // convenience getter for easy access to form fields
    get f() { return this.registerForm.controls; }


    public onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.registerForm.invalid) {
            return;
        }

        this.loading = true;
        const acitviationCode = this.registerForm.get('code').value;
        this.activationModel.code = acitviationCode;
        this.activationModel.id = this.userId;
        this.userService.activate(this.activationModel)
            .subscribe(
                data => {
                    this.alertService.success('Your account is active, you can login', true);
                    this.router.navigate(['/login']);
                },
                error => {
                    this.alertService.error(error);
                    this.loading = false;
                });
    }
}
