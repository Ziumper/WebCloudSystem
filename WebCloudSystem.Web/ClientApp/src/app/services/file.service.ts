import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { FileModel } from '../models/file.model';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class FileService {

    constructor(private http: HttpClient) {
    }

    private fileApi = 'api/files';

    saveFile(form: FormData): Observable<FileModel> {
        return this.http.post<FileModel>(this.fileApi, form);
    }

    updateFile(form: FormData): Observable<FileModel> {
        return this.http.put<FileModel>(this.fileApi, form);
    }

    deleteFile(id: number): Observable<FileModel> {
        return this.http.delete<FileModel>(this.fileApi + '/' + id);
    }

    getFileListPaged(): Observable<Array<FileModel>> {
        return this.http.get<Array<FileModel>>(this.fileApi);
    }

    // downloadFile(id: number): Observable<FileModel> {
    //     return this.http.get<FileModel>(this.fileApi + '/' + id);
    // }
}
