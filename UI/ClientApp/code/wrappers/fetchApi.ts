import { fetch } from 'domain-task';
import * as Const from '../config/Constants';
import AppSettings from './../config/AppSettings';

export interface FetchOptions {
    method: string,
    headers: any,
    body?: any
}

export interface HttpResponseError {
    message: string,
    statusCode: number,
    statusText: string
}

export class FetchApi {

    private baseUrl: string;
    private defaultOptions: FetchOptions = {
        method: Const.httpMethod.GET,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Pragma': 'no-cache',
        }
    }

    constructor(baseUrl: string) {
        this.baseUrl = baseUrl;
        this.fetch = this.fetch.bind(this);
        this.makeRequest = this.makeRequest.bind(this);
        this.handleResponse = this.handleResponse.bind(this);
    }

    public fetch(resourcePath: string, options? : any) {
        let opt = Object.assign({}, this.defaultOptions, options);
        let url = this.baseUrl + resourcePath;
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
        var contentType = response.headers.get("content-type")
        if (response.ok) {
            switch (contentType) {
                case Const.httpContentTypes.json:
                    return response.json() as Promise<any>;
                case Const.httpContentTypes.html:
                    return response.text() as Promise<any>;
                default:
                    return Promise.resolve(response);
            }
        }

        switch (contentType) {
            case Const.httpContentTypes.json:
                return response.json().then((data: any) => {
                    throw <HttpResponseError>{
                        message: data,
                        statusCode: response.status,
                        statusText: response.statusText
                    }
                });
            case Const.httpContentTypes.html:
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