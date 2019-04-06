import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { FileModel } from '../models/file.model';
import { HttpClient } from '@angular/common/http';
import { FilePagedModel } from '../models/file-paged.model';
import { FileQueryModel } from '../models/file-query.model';

@Injectable()
export class FileService {


    constructor(private http: HttpClient) {
    }

    private fileApi = 'api/files';

    saveFile(form: FormData): Observable<FileModel> {
        return this.http.post<FileModel>(this.fileApi + '/upload', form);
    }

    updateFile(fileModel: FileModel): Observable<FileModel> {
        return this.http.put<FileModel>(this.fileApi, fileModel);
    }

    deleteFile(id: number): Observable<FileModel> {
        return this.http.delete<FileModel>(this.fileApi + '/' + id);
    }

    getFileListPaged(filePagedQuery: FileQueryModel): Observable<FilePagedModel> {
        const params = filePagedQuery.getParams();
        return this.http.get<FilePagedModel>(this.fileApi + '/user', {params: params});
    }

    getFileById(fileId: number): Observable<FileModel> {
        return this.http.get<FileModel>(this.fileApi + '/' + fileId);
    }

    downloadFile(id: number): Observable<any> {
        return this.http.get<any>(this.fileApi + '/download/' + id, {responseType: 'blob' as 'json', observe: 'response' as 'body'});
    }
}
