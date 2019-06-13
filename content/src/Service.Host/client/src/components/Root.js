import React from 'react';
import { Provider } from 'react-redux';
import { Route, Redirect, Switch } from 'react-router';
import { OidcProvider } from 'redux-oidc';
import { Router } from 'react-router-dom';
import CssBaseline from '@material-ui/core/CssBaseline';
import PropTypes from 'prop-types';
import history from '../history';
import App from './App';
import Callback from '../containers/Callback';
import userManager from '../helpers/userManager';
import 'typeface-roboto';
import NotFound from './NotFound';

const Root = ({ store }) => (
    <div>
        <div style={{ paddingTop: '40px' }}>
            <Provider store={store}>
                <OidcProvider store={store} userManager={userManager}>
                    <Router history={history}>
                        <div>
                            <CssBaseline />

                            <Route
                                exact
                                path="/"
                                render={() => <Redirect to="/template" />}
                            />

                            <Route
                                path="/"
                                render={() => {
                                    document.title = 'Template';
                                    return false;
                                }}
                            />

                            <Switch>
                                <Route exact path="/template" component={App} />

                                <Route
                                    exact
                                    path="/template/signin-oidc-client"
                                    component={Callback}
                                />

                                <Route component={NotFound} />
                            </Switch>
                        </div>
                    </Router>
                </OidcProvider>
            </Provider>
        </div>
    </div>
);

Root.propTypes = {
    store: PropTypes.shape({}).isRequired
};

export default Root;
