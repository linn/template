import React from 'react';
import Typography from '@mui/material/Typography';
import { useSelector, useDispatch } from 'react-redux';
import { Page } from '@linn-it/linn-form-components-library';
import { useParams } from 'react-router';
import { Link } from 'react-router-dom';

import getName from '../selectors/userSelectors';
import actions from '../actions';
import config from '../config';
import history from '../history';

function App() {
    const name = useSelector(state => getName(state));
    const dispatch = useDispatch();

    const { id } = useParams();

    dispatch(actions.testAction());

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Typography variant="h6">App</Typography>
            <Typography>Hello {name}</Typography>
            <Typography>id is {id}</Typography>
            <Link to="/template/test-page"> test page </Link>
        </Page>
    );
}

export default App;
