import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user.model';



@Injectable()
export class UserService {
    private apiUrl: string;
    constructor(private http: HttpClient) {
        this.apiUrl = 'api/users';
    }

    getAll() {
        return this.http.get<User[]>(this.apiUrl);
    }

    getById(id: number) {
        return this.http.get(this.apiUrl + '/' + id);
    }

    register(user: User) {
        return this.http.post( this.apiUrl +  '/register', user);
    }

    update(user: User) {
        return this.http.put(this.apiUrl + '/' + user.id, user);
    }

    delete(id: number) {
        return this.http.delete(this.apiUrl + '/' + id);
    }
}
