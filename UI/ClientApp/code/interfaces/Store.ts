import { Dispatch } from 'redux';
import { connect } from 'react-redux';

export interface ComponentState {
    loaded: boolean
}

export interface ReduxAction<T>{ type: string, payload: T }

export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => any): void;
}

export function dispatchRequest<T extends ComponentState>(dispatch: any, actionType: string) {
    dispatch(<ReduxAction<T>>{
        type: actionType,
        payload: <T>{
            loaded: false
        }
    });
}

export function dispatchResponse<T extends ComponentState>(dispatch: any, actionType: string, newState: T) {
    dispatch(<ReduxAction<T>>{
        type: actionType,
        payload: newState 
    });
}

export const reduxConnect = function <TState>(stateSelector: (TState) => any = (state) => state, actionCreators) {
    return connect(
        (state: TState, ownProps: any) => ({ ...stateSelector(state), ...ownProps }),
        actionCreators
    )
} 