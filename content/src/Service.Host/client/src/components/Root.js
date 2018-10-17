import React, { Component } from 'react';
import { Provider } from 'react-redux';
import { Route, Redirect } from 'react-router';
import { OidcProvider } from 'redux-oidc';
import { Router } from 'react-router-dom';
import history from '../history';
import Navigation from './Navigation';
import App from './App';
import Callback from '../containers/Callback';
import userManager from '../helpers/userManager';

class Root extends Component {
    render() {
        const { store } = this.props;

        return (
            <Provider store={store}>
                <OidcProvider store={store} userManager={userManager}>
                    <Router history={history}>
                        <div>
                            <Navigation />

                            <Route path="/" render={() => { document.title = 'Template'; return false; }} />
                            <Route exact path="/" render={() => <Redirect to="/template" />} />
                            <Route exact path="/template" component={App} />
                            <Route exact path="/template/signin-oidc-client" component={Callback} />
                        </div>
                    </Router>
                </OidcProvider>
            </Provider>
        );

    }
}

export default Root;