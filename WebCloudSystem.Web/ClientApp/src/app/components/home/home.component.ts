import { Component, OnInit } from '@angular/core';
import { FileService } from 'src/app/services/file.service';
import { FileQueryModel } from 'src/app/models/file-query.model';
import { FilePagedModel } from 'src/app/models/file-paged.model';
import { first } from 'rxjs/operators';
import { AlertService } from 'src/app/services/alert.service';
import { FileModel } from 'src/app/models/file.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  fileQuery: FileQueryModel;
  filePagedModel: FilePagedModel;
  files: Array<FileModel>;


  constructor(private fileService: FileService, private alertService: AlertService) {
    this.fileQuery = new FileQueryModel(1, 10, 1, true, '');
    this.files = new Array<FileModel>();
  }

  ngOnInit(): void {
    this.getFiles();
  }


  public onPageChange(page: number): void {
    this.fileQuery.page = page;
    this.getFiles();
  }

  public getFiles(): void {
    this.fileService.getFileListPaged(this.fileQuery)
      .subscribe(
        data => {
          this.filePagedModel = data;
          this.files = data.entities;
        },
        error => {
          this.alertService.error(error);
        });
  }
}
