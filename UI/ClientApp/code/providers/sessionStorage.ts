export class SessionStorage {
    public static get<T>(key: string): T | null {
        let result = sessionStorage.getItem(key);
        if (result) {
            return JSON.parse(result) as T;
        }
        return null;
    }
    public static set<T>(key: string, data: T): void {
        if (data) {
            sessionStorage.setItem(key, JSON.stringify(data));
        }
        else {
            sessionStorage.removeItem(key);
        }
    }
    public static remove(key: string) {
        sessionStorage.removeItem(key);
    }
    public static clear() {
        sessionStorage.clear();
    }
}