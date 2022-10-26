import React from 'react';
import Typography from '@mui/material/Typography';
import { useSelector, useDispatch } from 'react-redux';
import { Page } from '@linn-it/linn-form-components-library';

import getName from '../selectors/userSelectors';
import actions from '../actions';
import config from '../config';
import history from '../history';

function App() {
    const name = useSelector(state => getName(state));
    const dispatch = useDispatch();

    dispatch(actions.testAction());

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Typography variant="h6">App</Typography>
            <Typography>Hello {name}</Typography>
        </Page>
    );
}

export default App;
