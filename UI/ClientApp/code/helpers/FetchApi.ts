﻿import { fetch } from 'domain-task';

export const HttpMethods = {
    GET: 'GET',
    POST: 'POST',
    PUT: 'PUT',
    PATCH: 'PATCH',
    DELETE: 'DELETE'
};

export const HttpContentTypes = {
    json: 'application/json; charset=utf-8',
    html: 'text/html; charset=utf-8'
}
export interface FetchOptions {
    method: string,
    headers: any,
    body?: any,
    mode: 'no-cors' | 'navigate' | 'same-origin' | 'cors' | undefined
}

export interface HttpResponseError {
    message: string,
    statusCode: number,
    statusText: string
}


const defaultOptions: FetchOptions = {
    method: HttpMethods.GET,
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Pragma': 'no-cache',
    },
    mode: 'no-cors'
}

export class FetchApi {

    private url: string;

    constructor(url: string) {
        this.url = url;
        this.fetch = this.fetch.bind(this);
        this.makeRequest = this.makeRequest.bind(this);
        this.handleResponse = this.handleResponse.bind(this);
    }

    public fetch(path: string | null = null, options: any = null) {
        let opt = Object.assign({}, defaultOptions, options);
        let url = path ? `${this.url}/${path}` : this.url;
        return this.makeRequest(url, options);
    }

    private makeRequest(url: string, options: FetchOptions) {
        return fetch(url, options)
            .then(response => {
                return this.handleResponse(response)
            })
            .catch(error => {
                throw error;
            });
    }

    private handleResponse = (response: any) => {
       
        if (response.ok) {
            return this.handleResponseSuccess(response);
        }

        this.handleResponse(response);
    }

    private handleResponseSuccess = (response: any) => {
        var contentType = response.headers.get("content-type")
        switch (contentType) {
            case HttpContentTypes.json:
                return response.json() as Promise<any>;
            case HttpContentTypes.html:
                return response.text() as Promise<any>;
            default:
                return Promise.resolve(response);
        }
    }
    private handleResponseError = (response: any) => {
        var contentType = response.headers.get("content-type");

        switch (contentType) {
            case HttpContentTypes.json:
                return response.json().then((data: any) => {
                    throw <HttpResponseError>{
                        message: data,
                        statusCode: response.status,
                        statusText: response.statusText
                    }
                });
            case HttpContentTypes.html:
                return response.text().then((data: any) => {
                    throw <HttpResponseError>{
                        message: data,
                        statusCode: response.status,
                        statusText: response.statusText
                    };
                });
            default:
                throw Promise.resolve(response);
        }
    }
}