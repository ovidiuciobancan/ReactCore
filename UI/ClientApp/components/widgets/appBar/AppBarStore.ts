import * as React from 'react';
import { Action, Reducer, Dispatch, bindActionCreators } from 'redux';
import { UserManager, UserManagerSettings } from 'oidc-client';
import * as PropTypes from 'prop-types';
import { AppBarProps } from 'material-ui'

import { ReduxAction, AppThunkAction } from 'interfaces/Store'

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface AppBarState {
    muiConfig?: AppBarProps
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
// Use @typeName and isActionType for type detection that works even after serialization/deserialization.

const actionNames = {
    INIT: 'APPBAR_INIT',
    UPDATE: 'APPBAR_UPDATE',
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = ReduxAction<AppBarState>;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export interface AppBarActionCreators {
    init: (config?: AppBarProps) => void,
    update: () => void,
}

export const actionCreators = {
    init: (initialConfig: AppBarProps): AppThunkAction<KnownAction> => (dispatch: Dispatch<any>, getState: any) => {
        let config = getState().appBar.muiConfig;
        if (!config) {
            dispatchConfig(actionNames.INIT, dispatch, initialConfig);
        }
    },
    update: (config: AppBarProps): AppThunkAction<KnownAction> => (dispatch: any, getState: any) => {
        dispatchConfig(actionNames.UPDATE, dispatch, config);
    },
   
};

const dispatchConfig = (actionName: string, dispatch: Dispatch<any>, config: AppBarProps) => {
    dispatch(<ReduxAction<AppBarState>>{
        type: actionName,
        payload: { muiConfig: config }
    })
}

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

export const reducer: Reducer<AppBarState> = (state: AppBarState, action: any) => {
    switch (action.type) {
        case actionNames.INIT:
            return { ...state, ...{ muiConfig: { ...state.muiConfig, ...(<ReduxAction<AppBarState>>action).payload.muiConfig } } }
        case actionNames.UPDATE:
            return { ...state, ...{ muiConfig: { ...state.muiConfig, ...(<ReduxAction<AppBarState>>action).payload.muiConfig } } };
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            return { ...(state || initialState) }
    }
};

const initialState: AppBarState = <AppBarState>{
    muiConfig: undefined,
    //<AppBarProps>{
        //className: '',
        //iconClassNameLeft: '',
        //iconClassNameRight: '',
        //iconElementLeft: null,
        //iconElementRight: null,
        //iconStyleLeft: null,
        //iconStyleRight: null,
        //onLeftIconButtonClick: () => {},
        //onRightIconButtonClick: () => {},
        //onTitleClick: () => {},
        //showMenuIconButton: true,
        //style: {},
        //title: {},
        //titleStyle: {},
        //zDepth: {},
    //}
}

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
