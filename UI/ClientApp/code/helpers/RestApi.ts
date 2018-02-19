import { FetchApi, HttpMethods } from './FetchApi';
import * as queryString from 'query-string';
import * as oDataClient from 'odata-client';

export class RestApi<TGetDto, TCreateDto, TUpdateDto> {

    private fetchApi: FetchApi; 

    constructor( resourcePath: string) {

        this.fetchApi = new FetchApi(resourcePath);

        this.getCollection = this.getCollection.bind(this);
        this.get = this.get.bind(this);
        this.post = this.post.bind(this);
        this.put = this.put.bind(this);
        this.delete = this.delete.bind(this);
    }

    public getCollection(query = null): Promise<TGetDto[]> {
        return this.fetchApi.fetch() as Promise<TGetDto[]>;
    }

    //public getCollection(filter: any = null): Promise<T[]> {
    //    let qs = filter ? `?${queryString.stringify(filter)}` : '';
    //    return super.fetch(this.resourcePath + qs) as Promise<T[]>;
    //}

    //public getCollection(filter: any = null): Promise<T[]> {
    //    let qs = filter ? `?${queryString.stringify(filter)}` : '';
    //    return super.fetch(this.resourcePath + qs) as Promise<T[]>;
    //}

    public get(id: string): Promise<TGetDto> {
        return this.fetchApi.fetch(id) as Promise<TGetDto>;
    }

    public post(data: TCreateDto): Promise<void> {
        let fetchOptions = {
            method: HttpMethods.POST,
            body: JSON.stringify(data)
        }
        return this.fetchApi.fetch(null, fetchOptions);
    }

    public put(id: string, data: TUpdateDto): Promise<void> {
        let fetchOptions = {
            method: HttpMethods.PUT,
            body: JSON.stringify(data)
        }
        return this.fetchApi.fetch(id, fetchOptions);
    }

    public patch(id: string, data: TUpdateDto): Promise<void> {
        let fetchOptions = {
            method: HttpMethods.PATCH,
            body: JSON.stringify(data)
        }
        return this.fetchApi.fetch(id, fetchOptions);
    }

    public delete(id: any): Promise<void> {
        let fetchOptions = {
            method: HttpMethods.DELETE,
        }
        return this.fetchApi.fetch(id, fetchOptions);
    }
}