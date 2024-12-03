﻿import React from 'react';
import { createRoot } from 'react-dom/client';
import { SnackbarProvider } from 'notistack';
import { ThemeProvider, StyledEngineProvider } from '@mui/material/styles';
import { linnTheme } from '@linn-it/linn-form-components-library';
import { AuthProvider } from 'react-oidc-context';
import { WebStorageStateStore } from 'oidc-client-ts';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';
import Root from './components/Root';
import 'typeface-roboto';
import config from './config';
import history from './history';

const container = document.getElementById('root');
const root = createRoot(container);

const host = window.location.origin;

const oidcConfig = {
    authority: config.authorityUri,
    client_id: 'app2',
    response_type: 'code',
    scope: 'openid profile email associations',
    redirect_uri: `${host}/template`,
    post_logout_redirect_uri: `${config.proxyRoot}/authentication/Account/Logout`,
    onSigninCallback: () => {
        const redirect = sessionStorage.getItem('auth:redirect');
        if (redirect) {
            history.push(redirect);
            sessionStorage.removeItem('auth:redirect');
        } else {
            history.push(`${host}/template`);
        }
    },
    userStore: new WebStorageStateStore({ store: window.localStorage })
};

const render = Component => {
    root.render(
        //eslint-disable-next-line react/jsx-props-no-spreading
        <AuthProvider {...oidcConfig}>
            <StyledEngineProvider injectFirst>
                <ThemeProvider theme={linnTheme}>
                    <SnackbarProvider dense maxSnack={5}>
                        <LocalizationProvider dateAdapter={AdapterMoment} locale="en-GB">
                            <Component />
                        </LocalizationProvider>
                    </SnackbarProvider>
                </ThemeProvider>
            </StyledEngineProvider>
        </AuthProvider>
    );
};

document.body.style.margin = '0';

render(Root);
