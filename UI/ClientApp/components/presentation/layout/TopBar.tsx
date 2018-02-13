import * as React from 'react';
import { connect } from 'react-redux';
import * as Oidc from 'providers/oidc';
import { OidcActionCreators, actionCreators } from 'providers/oidc';

import { ApplicationState } from 'store/index';
import { appSettings } from 'config/AppSettings'

type ComponentProps = Oidc.UserState & OidcActionCreators

export class TopBarComponent extends React.Component<ComponentProps, any>
{
    constructor(props: any) {
        super(props);
        this.isAuthenticated = this.isAuthenticated.bind(this);
        this.renderLogin = this.renderLogin.bind(this);
        this.renderLogout = this.renderLogout.bind(this);
    }

    public render() {

        if (!this.props.userLoaded) return null; 

        return (
            <nav className="navbar navbar-toggleable-md navbar-inverse bg-inverse fixed-top">
                <div className="form-inline my-2 my-lg-0" style={{ float: 'right' }}>
                    <ul className="menu" style={{ listStyleType: 'none' }}>
                        {this.renderLogin()}
                        {this.renderLogout()}
                    </ul>
                </div>
            </nav>
        )
    }

    private renderLogin() {
        if (this.isAuthenticated()) return null;
        return (
            <li style={{ float: 'left' }}>
                <button onClick={() => this.login()}>Login</button>
            </li>
        );
    }

    private renderLogout () {
        if (!this.isAuthenticated()) return null;
        return <li style={{ float: 'left' }}><button onClick={() => this.logout()}>Logout</button></li>
    }

    private isAuthenticated(): boolean {
        return !!this.props.user;
    }

    private login() {
        this.props.signin();
    }

    private logout() {
        this.props.signout();
    }
}

export const TopBar = connect(
    (state: ApplicationState, ownProps: any) => ({ ...state.oidc, ...ownProps }),
    actionCreators
)(TopBarComponent)