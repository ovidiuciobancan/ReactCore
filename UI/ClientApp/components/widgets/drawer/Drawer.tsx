import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { Drawer, AppBarProps } from 'material-ui'

import { ApplicationState } from 'store/index';
import * as Store from './DrawerStore';

interface OwnComponentProps{
    children?: any
    config?: AppBarProps
}

type ComponentProps = OwnComponentProps & Store.DrawerState & Store.DrawerActionCreators;

class DrawerComponent extends React.Component<ComponentProps, {}> {

    public componentWillMount() {
        this.props.init(this.props.config || {});
    }

    public render() {
        return <Drawer {...this.props.muiConfig}>
            {this.props.children}
        </Drawer>
    }
}

// Wire up the React component to the Redux store
export const DrawerWidget = connect(
    (state: ApplicationState, ownProps: OwnComponentProps) => ({...state.drawer, ...ownProps}),
    Store.actionCreators
)(DrawerComponent);