import { fetch, addTask, baseUrl } from 'domain-task';
import * as AppSettings from 'config/AppSettings';
import { FetchApi, HttpMethods } from 'providers/FetchApi';
import * as queryString from 'query-string';

export class RestApi<T> extends FetchApi {

    private resource: string;

    constructor(resource: string) {
        super('');

        this.resource = resource;

        this.getCollection = this.getCollection.bind(this);
        this.get = this.get.bind(this);
        this.post = this.post.bind(this);
        this.put = this.put.bind(this);
        this.delete = this.delete.bind(this);
    }

    public getCollection(filter: any = null): Promise<T[]> {
        let qs = filter ? `?${queryString.stringify(filter)}` : '';
        return super.fetch(this.resource + qs) as Promise<T[]>;
    }

    public get(key: any = ''): Promise<T> {
        let url = this.resource + '/' + key;
        return super.fetch(url) as Promise<T>;
    }

    public post(data: any): Promise<T> {
        let fetchOptions = {
            method: HttpMethods.POST,
            body: JSON.stringify(data)
        }
        return super.fetch(this.resource, fetchOptions);
    }

    public put(key: any, data: any): Promise<T> {
        let url = this.resource + '/' + key;
        let fetchOptions = {
            method: HttpMethods.PUT,
            body: JSON.stringify(data)
        }
        return super.fetch(url, fetchOptions);
    }

    public delete(key: any): Promise<T> {
        let url = this.resource + '/' + key;
        let fetchOptions = {
            method: HttpMethods.DELETE,
        }
        return super.fetch(url, fetchOptions);
    }
}