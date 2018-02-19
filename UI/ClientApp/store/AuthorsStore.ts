import { Action, Reducer, ActionCreator } from 'redux';

import { IAuthorDTO } from 'models/authors';
import { services } from 'services/index'

import {
    AppThunkAction,
    ReduxAction,
    ComponentState,
    dispatchRequest,
    dispatchResponse,
} from 'interfaces/index';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export const ActionType = {
    RequestAction: 'AUTHORS_REQUEST_ACTION',
    ResponseAction: 'AUTHORS_RESPONSE_ACTION'
}

export interface AuthorsState extends ComponentState {
    authors: IAuthorDTO[],
    author: IAuthorDTO
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = ReduxAction<AuthorsState>

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    getAuthors: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        let state = getState().authors as AuthorsState;

        services.authors.getAuthors()
            .then((response: any) => {
                let newState = <AuthorsState>{
                    ...state,
                    authors: response.value,
                    loaded: true
                }
                dispatchResponse(dispatch, ActionType.ResponseAction, newState);
            })
            .catch(error => {
                let newState = <AuthorsState>{
                    ...state,
                    authors: [],
                    loaded: true
                }
                dispatchResponse(dispatch, ActionType.ResponseAction, newState);
            })

        dispatchRequest(dispatch, ActionType.RequestAction);
    },
    getAuthor: (id: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        let state = getState().authors as AuthorsState;

        services.authors.getAuthor(id)
            .then((response: any) => {
                let newState = <AuthorsState>{
                    ...state,
                    author: response,
                    loaded: true
                }
                dispatchResponse(dispatch, ActionType.ResponseAction, newState);
            })
            .catch(error => {
                let newState = <AuthorsState>{
                    ...state,
                    author: <IAuthorDTO>{},
                    loaded: true
                }
                dispatchResponse(dispatch, ActionType.ResponseAction, newState);
            })

        dispatchRequest(dispatch, ActionType.RequestAction);
    },
    createAuthor: (author: IAuthorDTO): AppThunkAction<KnownAction> => (dispatch, getState) => {
    },
    deleteAuthor: (id: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
    },
    updateAuthor: (id: string, author: IAuthorDTO): AppThunkAction<KnownAction> => (dispatch, getState) => {
    },
    partialUpdateAuthor: (id: string, author: IAuthorDTO): AppThunkAction<KnownAction> => (dispatch, getState) => {
    },
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

export const reducer: Reducer<AuthorsState> = (state: AuthorsState, action: Action) => {

    switch (action.type) {
        case ActionType.RequestAction:
            return { ...state, ...(<ReduxAction<AuthorsState>>action).payload }
        case ActionType.ResponseAction:
            return { ...state, ...(<ReduxAction<AuthorsState>>action).payload }
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            return state || initialState;
    }
};

const initialState = <AuthorsState>{
    loaded: false,
    authors: [],
    author: <IAuthorDTO>{}
}
