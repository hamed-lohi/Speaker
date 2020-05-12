import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { User } from "../_models/user";
import { WINDOW } from "src/app/shared/base.utility";

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient, @Inject(WINDOW) protected window: Window) { }

    getAll() {
        return this.http.get<User[]>(`/users`);
    }

    register(user: any) {
        const endpoint = `${this.window.location.origin}/Api/Account/Register`;
        return this.http.post(endpoint, user);
    }

}
