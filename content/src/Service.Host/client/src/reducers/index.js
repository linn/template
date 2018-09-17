import { combineReducers } from 'redux';
import { routerReducer as router } from 'react-router-redux';
import { reducer as oidc } from 'redux-oidc';

const rootReducer = combineReducers({
    oidc,
    router,
});

export default rootReducer;