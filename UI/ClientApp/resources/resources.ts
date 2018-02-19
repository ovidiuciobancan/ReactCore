export interface Resources {
    menu: {
        home: string,
        authors: string,
        counter: string,
        weather: string
    },
    auth: {
        loginButton: string,
        logoutButton: string,
        greetingsMessage: string
    }
}

export const resources: Resources = {
    menu: {
        home: 'Home',
        authors: 'Authors',
        counter: 'Counter',
        weather: 'Weather'
    },

    auth: {
        loginButton: 'Signin',
        logoutButton: 'Signout',
        greetingsMessage: 'Hello, '
    }
}