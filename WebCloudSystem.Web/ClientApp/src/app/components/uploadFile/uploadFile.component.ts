import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FileService } from 'src/app/services/file.service';
import { first } from 'rxjs/operators';
import { AlertService } from 'src/app/services/alert.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-upload-file',
    templateUrl: './uploadFile.component.html',
    styleUrls: ['./uploadFile.component.css']
})
export class UploadFileComponent implements OnInit {
    public fileToUpload: File;
    public isUploaded: Boolean;
    @ViewChild('myInput')
    myInputVariable: ElementRef;

    constructor(private fileService: FileService, private alertService: AlertService, private router: Router) { }

    ngOnInit(): void {
        this.isUploaded = false;
    }

    public submit(): void {
        const formData = new FormData();
        formData.append('file', this.fileToUpload);
        this.fileService.updateFile(formData).pipe(first())
        .subscribe(
            data => {
                this.alertService.success('Upload successfull!', false);
            },
            error => {
                this.alertService.error(error);
        });
    }

    public handleFileUpload(files: FileList): void {
        this.fileToUpload = files.item(0);
        this.isUploaded = true;
    }

    public deleteFile(): void {
        this.isUploaded = false;
        this.myInputVariable.nativeElement.value = '';
    }
 }
