import * as React from 'react';
import { Action, Reducer, Dispatch, bindActionCreators } from 'redux';
import { UserManager, UserManagerSettings } from 'oidc-client';

export interface UserProfile {
    sid: string;
    sub: string;
    auth_time: number;
    idp: string;
    amr: string[];
    given_name: string;
    family_name: string;
}

export interface UserInfo {
    id_token: string;
    session_state: string;
    access_token: string;
    token_type: string;
    scope: string;
    profile: UserProfile;
    expires_at: number;
}

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface UserState {
    user: UserInfo | null,
    userLoaded: boolean,
    userManager: UserManager | null
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
// Use @typeName and isActionType for type detection that works even after serialization/deserialization.

const actionNames = {
    INIT: 'OIDC_INIT',
    USER_LOADED: 'OIDC_USER_LOADED',
    USER_LOADING: 'OIDC_USER_LOADING',
}

interface ResponseAction<T> { type: string, payload: T }
interface UserLoadedAction { type: string, payload: UserState }
interface UserLoadingAction { type: string, payload: UserState }

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = UserLoadedAction | UserLoadingAction | any;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export interface OidcActionCreators {
    init: (settings: UserManagerSettings) => void,
    signinSilent: () => void,
    signin: () => void,
    signout: () => void
}

export const actionCreators = {
    init: (settings: UserManagerSettings): AppThunkAction<KnownAction> => (dispatch: any, getState: any) => {
        let userManager = getState().oidc.userManager;
        if (!userManager) {
            dispatch(<ResponseAction<UserState>>{
                type: actionNames.INIT,
                payload: { userManager: new UserManager(settings) }
            })
        }
    },
    signinSilent: (): AppThunkAction<KnownAction> => (dispatch: any, getState: any) => {
        let userManager = getState().oidc.userManager;
        if (!userManager) return;

        userManager.signinSilent()
            .then((response: UserInfo) => {
                dispatchUserLoaded(dispatch, response)
            })
            .catch((error: any) => {
                if (error.error == "login_required") {
                    dispatchUserLoaded(dispatch, null);
                }
            });

        userManager.signinSilentCallback();

        dispatchUserLoading(dispatch);
    },
    signin: (): AppThunkAction<KnownAction> => (dispatch: any, getState: any) => {
        let userManager = getState().oidc.userManager;
        if (!userManager) return;

        userManager.signinRedirect()
            .then((response: UserInfo) => {
                dispatchUserLoaded(dispatch, response)
            })
            .catch((error: any) => {
                //TODO
            });

        userManager.signinRedirectCallback();

        dispatchUserLoading(dispatch);

    },
    signout: (): AppThunkAction<KnownAction> => (dispatch: any, getState: any) => {
        let userManager = getState().oidc.userManager;
        if (!userManager) return;

        userManager.signoutRedirect()
            .then((response: UserInfo) => {
                //dispatchUserLoaded(dispatch, response)
            })
            .catch((error: any) => {
                //TODO
            });

        userManager.signoutRedirectCallback();

        dispatchUserLoading(dispatch);
    },
};

const dispatchUserLoading = (dispatch: any) => {
    dispatch(<UserLoadingAction>{
        type: actionNames.USER_LOADING,
        payload: { userLoaded: false }
    })
}

const dispatchUserLoaded = (dispatch: any, response: UserInfo | null) => {
    dispatch(<UserLoadedAction>{
        type: actionNames.USER_LOADED,
        payload: {
            user: response,
            userLoaded: true
        }
    })
}

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

export const reducer: Reducer<UserState> = (state: UserState, action: KnownAction) => {
    switch (action.type) {
        case actionNames.INIT:
            return { ... (<ResponseAction<UserState>>action).payload }
        case actionNames.USER_LOADED:
            return { ...state, ...(<UserLoadedAction>action).payload };
        case actionNames.USER_LOADING:
            return { ...state, ...(<UserLoadingAction>action).payload };
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            return { ...(state || initialState) }
    }
};

const initialState: UserState = <UserState>{
    user: null,
    userLoaded: false,
    userManager: null,
}

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => any): void;
}