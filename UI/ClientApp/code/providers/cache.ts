

const CacheTableKey: string = 'CacheTableKey';

export class CacheProvider {

    public CacheItems : any = {
        DynamicRoutes: 'DynamicRoutes',
    }

    private static _instance : CacheProvider;

    public static getInstance(): CacheProvider {
        if (!CacheProvider._instance) {
            CacheProvider._instance = new CacheProvider();
        }

        return CacheProvider._instance;
    }

    private constructor() {
        this.init();
    }

    public static clear(): void {

    }

    public readonly Duration = 3600000; 

    public getOrFetch(key: string, getter: Function) {
        let item = this.get(key);
        if (item === null) {
            item = getter();
            this.set(key, item);
        }

        return item;
    }

    public get(key: string) {
        return this.getItem(key);
    }

    public set(key: string, data: any): void{
        this.setItem(key, data);
    }

    public invalidate(key: string) : void {
        if (this.hasKey(key)) {
            this.updateExpirationDate(key, -this.Duration);
        }
    }

    public invalidateAll() : void {
        for (var key in this.CacheItems) {
            this.invalidate(key);
        }
    }

    private getItem(key: string) : any {
        if (this.hasKey(key)) {
            if (this.hasExpired(key)) {
                this.removeItem(key);
            }
            return JSON.parse(sessionStorage.getItem(key) || '');
        }

        return null;
    }

    private setItem(key: string, item: any) {
        if (this.hasKey(key)) {
            sessionStorage.setItem(key, JSON.stringify(item));
            this.updateExpirationDate(key);
        }
    }

    private removeItem(key: string) {
        if (this.hasKey(key)) {
            sessionStorage.removeItem(key);
        }
    }

    private hasKey(key: string): boolean {
        return this.CacheItems[key] !== undefined;
    }

    private getTable() {
        return JSON.parse(sessionStorage.getItem(CacheTableKey) || '');
    }

    private updateExpirationDate(key: string, duration: number = this.Duration) {
        if (this.hasKey(key)) {
            var cacheTable = this.getTable();
            cacheTable[key] = new Date(Date.now() + duration);
            sessionStorage.setItem(CacheTableKey, cacheTable);
        }
    }

    private hasExpired(key: string): boolean {
        if (this.hasKey(key)) {
            var cacheTable = this.getTable();
            return cacheTable[key] < Date.now();
        }

        return false;
    }

    private init() {
        if (sessionStorage.getItem(CacheTableKey) === null) {
            var date = new Date(Date.now() + this.Duration);
            var table = <any>{};

            for (var key in this.CacheItems) {
                table[key] = date;
            }

            sessionStorage.setItem(CacheTableKey, JSON.stringify(table));
        }
    }
}

export default CacheProvider.getInstance();