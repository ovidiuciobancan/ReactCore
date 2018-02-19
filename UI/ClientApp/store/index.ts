import * as WeatherForecasts from './WeatherForecasts'
import * as Counter from './Counter'
import * as Oidc from 'providers/oidc/OidcStore'
import * as AppBar from 'widgets/appBar/AppBarStore'
import * as Drawer from 'widgets/drawer/DrawerStore'

import * as Authors from 'store/AuthorsStore'

// The top-level state object
export interface ApplicationState {
    //providers
    oidc: Oidc.UserState

    //widgets
    appBar: AppBar.AppBarState
    drawer: Drawer.DrawerState

    //pages
    authors: Authors.AuthorsState

    //test
    counter: Counter.CounterState
    weatherForecasts: WeatherForecasts.WeatherForecastsState
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
    //providers
    oidc: Oidc.reducer,

    //widgets
    appBar: AppBar.reducer,
    drawer: Drawer.reducer,

    //pages
    authors: Authors.reducer,

    //test
    counter: Counter.reducer,
    weatherForecasts: WeatherForecasts.reducer
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
