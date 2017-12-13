import React from 'react';
import ReactDOM from 'react-dom';
import createStoreFunction from './configureStore';
import Root from './components/Root';
import reducer from './reducers';
import { AppContainer } from 'react-hot-loader';

import 'bootstrap/dist/css/bootstrap.css';
import './css/index.scss';
import '../assets/kaboom/kaboom.css'

const initialState = {};

const store = createStoreFunction(reducer, initialState);

const render = Component => {
    ReactDOM.render(
        <AppContainer>
            <Component store={store} />
        </AppContainer>,
        document.getElementById('root')
    );
};

render(Root);

// Hot Module Replacement API
if (module.hot) {
    //module.hot.accept('./reducers', () => store.replaceReducer(reducer));
    module.hot.accept('./components/Root', () => {
        const NextRoot = require('./components/Root').default;
        render(NextRoot);
    });
}
