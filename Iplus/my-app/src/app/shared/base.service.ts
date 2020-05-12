import { Observable, of, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpClient, HttpHeaders, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { WINDOW } from "src/app/shared/base.utility";
import { Inject } from "@angular/core";
import { MessageService } from "src/app/general/message.service";
import { Config } from "protractor/built";

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

//@Injectable()
export class BaseService {

    public apiUrl = this.window.location.origin;  // URL to web api

    constructor(protected messageService: MessageService,
        protected http: HttpClient,
        protected snackBar: MatSnackBar,
        @Inject(WINDOW) protected window: Window
    ) { }

    openSnackBar(mssage, label) {
        this.snackBar.open(mssage, label, { duration: 5000, direction: "ltr" });
    }

    setApiUrl(apiName: string) {
      this.apiUrl = this.window.location.origin + `/api/${apiName}/`;
    }

  //++++++++++++++++++++++++++++++

  postData(record: any, method: string): Observable<any> {

    var url = this.apiUrl + method;
    return this.http.post<any>(url, record, httpOptions).pipe(
      tap((hero: any) => this.log(`added record`)),
      catchError(this.handleError<any>('addHero'))
    );

  }

  getData(method: string, paramsQuerySt: string = ""): Observable<any[]> {
    var url = `${this.apiUrl}${method}?${paramsQuerySt}`;

    return this.http.get<any[]>(url)
      .pipe(
        tap(_ => this.log('fetched GetDataTable')),
        catchError(this.handleError('GetDataTable', []))
      );
  }

    /** GET heroes from the server */
    getDataTable(paramsQuerySt: string = ""): Observable<any[]> {

        return this.getData('GetDataTable', paramsQuerySt);
    }

    /** GET heroes from the server */
    getAll(): Observable<any[]> {
        var url = this.apiUrl + 'GetAll';

        return this.http.get<any[]>(url)
            .pipe(
            tap(_ => this.log('fetched heroes')),
            catchError(this.handleError('getAll', []))
            );
    }

    /** GET hero by id. Return `undefined` when id not found */
    getByIdNo404<Data>(id: number): Observable<any> {

        const url = `${this.apiUrl}GetById?id=${id}`;
        //const url = `${this.apiUrl}/?id=${id}`;
        return this.http.get<any[]>(url)
            .pipe(
            map(heroes => heroes[0]), // returns a {0|1} element array
            tap(h => {
                const outcome = h ? `fetched` : `did not find`;
                this.log(`${outcome} hero id=${id}`);
            }),
            catchError(this.handleError<any>(`getHero id=${id}`))
            );
    }

    /** GET hero by id. Will 404 if id not found */
    getById(id: number): Observable<any> {
        const url = `${this.apiUrl}GetById?id=${id}`;
        //const url = `${this.apiUrl}/${id}`;
        return this.http.get<any>(url).pipe(
            tap(_ => this.log(`fetched hero id=${id}`)),
            catchError(this.handleError<any>(`getHero id=${id}`))
        );
    }

    /** GET hero by id. Will 404 if id not found */
    getForEdit(id: number): Observable<any> {
        const url = `${this.apiUrl}GetForEdit?id=${id}`;
        //const url = `${this.apiUrl}/${id}`;
        return this.http.get<any>(url).pipe(
            tap(_ => this.log(`fetched hero id=${id}`)),
            catchError(this.handleError<any>(`getHero id=${id}`))
        );
    }

    /* GET heroes whose name contains search term */
    searchAll(term: string): Observable<any[]> {
        if (!term.trim()) {
            // if not search term, return empty hero array.
            return of([]);
        }
        return this.http.get<any[]>(`${this.apiUrl}/?name=${term}`).pipe(
            tap(_ => this.log(`found heroes matching "${term}"`)),
            catchError(this.handleError<any[]>('searchHeroes', []))
        );
    }

    //////// Save methods //////////

    /** POST: add a new hero to the server */
    add(record: any): Observable<any> {

        return this.postData(record, 'Save');
        //return this.http.post<any>(url, record, httpOptions).pipe(
        //    tap((hero: any) => this.log(`added hero w/ id=${(hero as any).Id}`)),
        //    catchError(this.handleError<any>('addHero'))
        //);
    }

    /** DELETE: delete the hero from the server */
    delete(record: any | number): Observable<any> {
        const id = typeof record === 'number' ? record : record.Id;
        var url = `${this.apiUrl}/Delete/${id}`;
        return this.http.delete<any>(url, httpOptions).pipe(
            tap(_ => this.log(`deleted hero id=${id}`)),
            catchError(this.handleError<any>('deleteHero'))
        );
    }

    /** PUT: update the hero on the server */
    update(hero: any): Observable<any> {
        return this.http.put(this.apiUrl, hero, httpOptions).pipe(
            tap(_ => this.log(`updated hero id=${hero.Id}`)),
            catchError(this.handleError<any>('updateHero'))
        );
    }

    /**
     * Handle Http operation that failed.
     * Let the app continue.
     * @param operation - name of the operation that failed
     * @param result - optional value to return as the observable result
     */
    protected handleError<T>(operation = 'operation', result?: T) {
        return (error: HttpErrorResponse): Observable<T> => {

            // TODO: send the error to remote logging infrastructure
            console.error(error); // log to console instead

            // TODO: better job of transforming error for user consumption
            this.log(`${operation} failed: ${error}`);

            this.openSnackBar(error, '')

            // Let the app keep running by returning an empty result.
            //return of(result as T);
            return throwError(result as T);

            // return an observable with a user-facing error message
            //return throwError(error.message);

        };
    }

    /** Log a HeroService message with the MessageService */
    protected log(message: string) {
        this.messageService.add(`BaseService: ${message}`);
    }

    /** GET DropDownList */
    getSelectOptions(selectApiName: string, params: string = ""): Observable<any[]> {
        var url = `${this.window.location.origin}/Api/${selectApiName}/GetSelectOptions?${params}`;

        return this.http.get<any[]>(url)
            .pipe(
            tap(_ => this.log('GetSelectOptions')),
            catchError(this.handleError('GetSelectOptions', []))
            );
    }

    postFile(fileToUpload: File, typeId: number): Observable<any> {
        const endpoint = `${this.window.location.origin}/Api/FileApi/UploadSingleFile`;
        const fileHttpOptions = {
            headers: new HttpHeaders({})//'Content-Type': 'text/plain;charset=UTF-8' 
        };

        const formData: FormData = new FormData();
        formData.append('fileKey', fileToUpload, fileToUpload.name);
        formData.set('keys', fileToUpload.name);
        formData.set('typeId', typeId.toString());
        return this.http.post(endpoint, formData, fileHttpOptions)
            .pipe(
            //map(() => { return true; }),
            catchError(this.handleError<any>('File'))
            );
    }

    /** DELETE: delete the hero from the server */
    deleteFile(id: number): Observable<any> {
        const endpoint = `${this.window.location.origin}/Api/FileApi/Delete/${id}`;

        return this.http.delete<any>(endpoint, httpOptions).pipe(
            tap(_ => this.log(`deleted hero id=${id}`)),
            catchError(this.handleError<any>('deleteHero'))
        );
    }

    //isLoggedIn = false;

    //login(info: any){

    //    const endpoint = `${this.window.location.origin}/Api/Account/LoginUser`;

    //    return this.post(info, endpoint)
    //      //.subscribe(
    //      //  (data: Config) => this.config = { ...data }, // success path
    //      //  error => this.error = error // error path
    //      //);
    //      .subscribe(a => {
    //          if (a.status == 200) {
    //            this.isLoggedIn = true;
    //            alert("خوش آمدید");
    //          } else {
    //            alert("اطلاعات صحیح نمی باشد");
    //          }

    //      });
    //}

    //logout() {

    //  this.isLoggedIn = false;
    //}

    /**
     * Ask user to confirm an action. `message` explains the action and choices.
     * Returns observable resolving to `true`=confirm or `false`=cancel
     */
    confirm(message?: string): Observable<boolean> {
        const confirmation = window.confirm(message || 'Is it OK?');

        return of(confirmation);
    };

}
