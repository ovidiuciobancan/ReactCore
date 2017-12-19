interface IAppSettings {
   
}

class AppSettings {

    public settings: IAppSettings = {};

    constructor() {
        this.settings = (document as any)["settings"] as IAppSettings;
    }
}

export default new AppSettings().settings;