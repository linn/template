import { Page } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import React from 'react';
import history from '../history';
import config from '../config';

function TestPage() {
    return (
        <Page history={history} config={config}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    Hello
                </Grid>
            </Grid>
        </Page>
    );
}

export default TestPage;
