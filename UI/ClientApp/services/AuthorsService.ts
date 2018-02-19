import { RestApi } from 'helpers/RestApi';
import { IAuthorDTO, IAuthorCreateDTO, IAuthorUpdateDTO } from 'models/authors';

export const AuthorsResourcePath: string = 'authors';

export class AuthorsService {

    private api: RestApi<IAuthorDTO, IAuthorCreateDTO, IAuthorUpdateDTO>;


    constructor(serviceUrl: string) {
        let url = `${serviceUrl}/${AuthorsResourcePath}`
        this.api = new RestApi<IAuthorDTO, IAuthorCreateDTO, IAuthorUpdateDTO>(url);
    }

    public getAuthors(query = null): Promise<IAuthorDTO[]> {
        return this.api.getCollection(query) as Promise<IAuthorDTO[]>;
    }

    public getAuthor(id: string): Promise<IAuthorDTO>  {
        return this.api.get(id) as Promise<IAuthorDTO>;
    }

    public createAuthor(author: IAuthorCreateDTO): Promise<void> {
        return this.api.post(author) as Promise<void>;
    }

    public updateAuthor(id: string, author: IAuthorUpdateDTO): Promise<void> {
        return this.api.put(id, author) as Promise<void>;
    }

    public partialUpdateAuthor(id: string, patchAuthor: IAuthorUpdateDTO | any) {
        return this.api.patch(id, patchAuthor) as Promise<void>;
    }

    public delete(id: any): Promise<void> {
        return this.api.delete(id) as Promise<void>;
    }
}