import { Component, OnInit } from '@angular/core';
import { FileService } from 'src/app/services/file.service';
import { FileModel } from 'src/app/models/file.model';
import { ActivatedRoute } from '@angular/router';
import { AlertService } from 'src/app/services/alert.service';

@Component({
    selector: 'app-edit-file',
    templateUrl: './editFile.component.html',
    styleUrls: ['./editFile.component.css']
})
export class EditFileComponent implements OnInit {

    public model: FileModel;
    private fileId: number;

    constructor(private fileService: FileService,
        private route: ActivatedRoute,
        private alertService: AlertService) {

         }

    ngOnInit(): void {
        this.fileId = this.route.snapshot.params['id'];
        this.model = new FileModel();
        this.getFile();
    }

    public submit(model: FileModel) {
        this.updateFile(model);
    }

    private getFile(): void {
        this.fileService.getFileById(this.fileId).subscribe(
            data => {
                this.model = data;
            },
            error => {
                this.alertService.error(error, false);
            }
        );
    }

    private updateFile(fileModel: FileModel): void {
        this.fileService.updateFile(fileModel).subscribe(data => {
            this.model = data;
            this.alertService.success('Update succesful!');
        }, error => {
            this.alertService.error(error);
        });
    }

}
