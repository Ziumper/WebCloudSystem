import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FileService } from 'src/app/services/file.service';
import { first } from 'rxjs/operators';
import { AlertService } from 'src/app/services/alert.service';

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

    constructor(private fileService: FileService, private alertService: AlertService) { }

    ngOnInit(): void {
        this.reset();
    }

    public submit(): void {
        const formData = new FormData();
        formData.append('file', this.fileToUpload);
        this.fileService.saveFile(formData)
        .subscribe(
            data => {
                this.alertService.success('Upload successfull!', false);
            },
            error => {
                this.alertService.error(error);
        });
        this.reset();
    }

    public handleFileUpload(files: FileList): void {
        this.fileToUpload = files.item(0);
        this.isUploaded = true;
    }

    public deleteFile(): void {
       this.reset();
    }

    private reset(): void {
        this.isUploaded = false;
        this.myInputVariable.nativeElement.value = '';
        this.fileToUpload = undefined;
    }
 }
