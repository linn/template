import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import { reducers as sharedLibraryReducers } from '@linn-it/linn-form-components-library';

const reducer = combineReducers({
    ...sharedLibraryReducers,
    oidc
});

export default reducer;
