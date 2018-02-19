import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { AppBar as MuiAppBar, AppBarProps } from 'material-ui'

import { ApplicationState } from 'store/index';
import * as Store from './AppBarStore'

interface OwnComponentProps{
    config: AppBarProps,
}

type ComponentProps = OwnComponentProps & Store.AppBarState & Store.AppBarActionCreators;

class AppBarComponent extends React.Component<ComponentProps, {}> {

    public componentWillMount() {
        this.props.init(this.props.config);
    }

    public render() {
        return <MuiAppBar {...this.props.muiConfig} />
    }
}

// Wire up the React component to the Redux store
export const AppBarWidget = connect(
    (state: ApplicationState, ownProps: OwnComponentProps) => ({...state.appBar, ...ownProps}),
    Store.actionCreators
)(AppBarComponent);