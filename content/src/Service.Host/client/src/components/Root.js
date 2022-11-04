﻿import React from 'react';
import { Provider } from 'react-redux';
import { Route, Routes } from 'react-router';
import { OidcProvider } from 'redux-oidc';
import { Navigation } from '@linn-it/linn-form-components-library';
import { BrowserRouter, Navigate } from 'react-router-dom';
import PropTypes from 'prop-types';
import App from './App';
import Callback from './Callback';
import userManager from '../helpers/userManager';
import 'typeface-roboto';
import NotFoundPage from './NotFoundPage';

function Root({ store }) {
    return (
        <div>
            <div className="padding-top-when-not-printing">
                <Provider store={store}>
                    <OidcProvider store={store} userManager={userManager}>
                        <div>
                            <Navigation />
                            <BrowserRouter>
                                <Routes>
                                    <Route
                                        exact
                                        path="/"
                                        element={<Navigate to="/template" replace />}
                                    />
                                    <Route path="/template" element={<App />} />
                                    <Route exact path="/template/:id" element={<App />} />
                                    <Route
                                        exact
                                        path="/template/signin-oidc-client"
                                        element={<Callback />}
                                    />
                                    <Route element={<NotFoundPage />} />
                                </Routes>
                            </BrowserRouter>
                        </div>
                    </OidcProvider>
                </Provider>
            </div>
        </div>
    );
}

Root.propTypes = {
    store: PropTypes.shape({}).isRequired
};

export default Root;
