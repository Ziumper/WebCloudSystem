import { Component, OnInit } from '@angular/core';
import { FileService } from 'src/app/services/file.service';
import { FileQueryModel } from 'src/app/models/file-query.model';
import { FilePagedModel } from 'src/app/models/file-paged.model';
import { first } from 'rxjs/operators';
import { AlertService } from 'src/app/services/alert.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  fileQuery: FileQueryModel;
  filePagedModel: FilePagedModel;

  constructor(private fileService: FileService, private alertService: AlertService) {
    this.fileQuery = new FileQueryModel(1, 10, 1, true, '');
  }

  ngOnInit(): void {
    // this.fileService.getFileListPaged(this.fileQuery)
    // .pipe(first())
    // .subscribe(
    //   data => {
    //     this.filePagedModel = data;
    //   },
    //   error => {
    //     this.alertService.error(error);
    //   });
  }
}
