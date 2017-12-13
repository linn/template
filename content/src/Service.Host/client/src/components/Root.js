import React, { Component } from 'react';
import { Provider } from 'react-redux';
import { Route, Redirect } from 'react-router';
import { ConnectedRouter as Router } from 'react-router-redux';
import history from '../history';
import Navigation from './Navigation';
import App from './App';

class Root extends Component {
    render() {
        const { store } = this.props;

        return (
            <Provider store={store}>
                <Router history={history}>
                    <div>
                        <Navigation />

                        <Route path="/" render={() => { document.title = 'Template'; return false; }} />
                        <Route exact path="/" render={() => <Redirect to="/template" />} />
                        <Route exact path="/template" component={App} />
                    </div>
                </Router>
            </Provider>      
        );

    }
}

export default Root;