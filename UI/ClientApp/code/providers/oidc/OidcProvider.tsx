import * as React from 'react';
import { bindActionCreators } from 'redux'; 
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router-dom';
import { UserManagerSettings } from 'oidc-client';

import * as Store from './OidcStore';

interface OwnComponentProps {
    userManagerSettings: UserManagerSettings,
    children?: any
}

type ComponentProps = Store.UserState & OwnComponentProps & Store.OidcActionCreators

class OidcProviderComponent extends React.Component<ComponentProps, any> {

    constructor(props: any) {
        super(props);
    }

    public componentWillMount() {
        this.props.init(this.props.userManagerSettings);
        this.props.signinSilent();
    }

    public render() {
        return <div>{this.props.children}</div>
    }
}

export const OidcCallback = (props: RouteComponentProps<{}>) => {
    props.history.push('');
    return null;
}

export const OidcProvider = connect(
    (state: any, ownProps: OwnComponentProps) => ({ ...state.oidc, ...ownProps }),
    Store.actionCreators
)(OidcProviderComponent)