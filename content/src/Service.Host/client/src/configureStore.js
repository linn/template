import { createStore, applyMiddleware, compose } from 'redux';
import { routerMiddleware } from 'react-router-redux';
import thunkMiddleware from 'redux-thunk';
import history from './history';

export default function createStoreFunction(reducer, initialState) {

    const middleware = applyMiddleware(thunkMiddleware, routerMiddleware(history));

    const enhancers = window.__REDUX_DEVTOOLS_EXTENSION__ && process.env.NODE_ENV !== 'production'
        ? compose(
            middleware,
            window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
        )
        : compose(
            middleware);

    return createStore(reducer, initialState, enhancers);
};