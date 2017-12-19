class LocalStorage {

    private static _instance: LocalStorage = new LocalStorage();
    private subscribers: Function[] = [];

    private constructor() {
        if (LocalStorage._instance) {
            throw new Error("Error: Instantiation failed: Use method getInstance() instead of new.");
        }

        LocalStorage._instance = this;

        this.get = this.get.bind(this);
        this.set = this.set.bind(this);
        this.remove = this.remove.bind(this);
        this.clear = this.clear.bind(this);

        this.notify = this.notify.bind(this);
        this.onChange = this.onChange.bind(this);

        window.addEventListener("storage", this.notify, false);
    }

    public static getInstance(): LocalStorage {
        return LocalStorage._instance;
    }

    private notify() {
        this.subscribers.map(callback => callback());
    }

    public onChange(subscriber: Function) {
        this.subscribers.push(subscriber);
    }

    public get<T>(key: string): T | null {
        let result = localStorage.getItem(key);
        if (result) {
            return JSON.parse(result) as T;
        }
        return null;
    }
    public set<T>(key: string, data: T): void {
        if (data) {
            localStorage.setItem(key, JSON.stringify(data));
        }
        else {
            localStorage.removeItem(key);
        }
    }
    public remove(key: string) {
        localStorage.removeItem(key);
    }
    public clear() {
        localStorage.clear();
    }
}

export default LocalStorage.getInstance();