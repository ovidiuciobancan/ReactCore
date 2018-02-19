import * as React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux'
import { UserInfo } from 'providers/Oidc'
import { RaisedButton, FlatButton } from 'material-ui'
import { resources } from 'resources/resources'
import { ApplicationState } from 'store/index'
import { actionCreators as oidcActionCreators } from 'providers/oidc'

interface OwnComponentProps {

}

interface ComponentProps extends OwnComponentProps {
    user?: UserInfo
    userLoaded: boolean
    onLoginClick: () => void
    onLogoutClick: () => void
}

const AuthComponent = (props: ComponentProps| any) => {
    if (!props.userLoaded) return null;

    const renderLogin = () => {
        if (props.user) return null;
        return <RaisedButton label={resources.auth.loginButton} onClick={props.onLoginClick} />
    }

    const renderGreetings = () => {
        if (!props.user) return null;
        const userProfile = (props.user as UserInfo).profile;

        return `${resources.auth.greetingsMessage} ${userProfile.given_name} ${userProfile.family_name}`;
    }

    const renderLogout = () => {
        if (!props.user) return null;
        return (
            <div>
                <RaisedButton label={resources.auth.logoutButton} onClick={props.onLogoutClick} />
            </div>
        );
    }

    return (
        <div className='row auth-container'>
            <div className='col-md-7 auth-greetings'>
                {renderGreetings()}
            </div>
            <div className='col-md-5 auth-button'>
                {renderLogin()}
                {renderLogout()}
            </div>
        </div>
    );
}

export const Auth = connect(
    (state: ApplicationState, ownProps: OwnComponentProps) => ({ ...{ user: state.oidc.user, userLoaded: state.oidc.userLoaded }, ...ownProps }),
    (dispatch) => bindActionCreators({
        onLoginClick: oidcActionCreators.signin,
        onLogoutClick: oidcActionCreators.signout
    }, dispatch)
)(AuthComponent)