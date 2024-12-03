import React from 'react';
import { Route, Routes } from 'react-router';
import { Navigate, unstable_HistoryRouter as HistoryRouter } from 'react-router-dom';
import App from './App';
import 'typeface-roboto';
import NotFoundPage from './NotFoundPage';
import history from '../history';
import useSignIn from '../hooks/useSignIn';
import Navigation from '../containers/Navigation';

function Root() {
    useSignIn();
    return (
        <div>
            <div className="padding-top-when-not-printing">
                <Navigation />
                <HistoryRouter history={history}>
                    <Routes>
                        <Route exact path="/" element={<Navigate to="/template" replace />} />
                        <Route path="/template" element={<App />} />

                        <Route element={<NotFoundPage />} />
                    </Routes>
                </HistoryRouter>
            </div>
        </div>
    );
}

export default Root;
