import { AppSettings } from 'config/AppSettings';

import { AuthorsService } from 'services/AuthorsService';

class Services {

    constructor(serviceUrl: string) {

        this.authors = new AuthorsService(serviceUrl);
    }

    public authors: AuthorsService;
}

export const services = new Services(AppSettings.common.apiUrl);