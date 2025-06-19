import React from 'react';
import Typography from '@mui/material/Typography';
import { Link } from 'react-router-dom';
import List from '@mui/material/List';
import { Grid } from '@mui/material';

import ListItem from '@mui/material/ListItem';
import Page from '../containers/Page';

function App() {
    return (
        <Page showBreadcrumbs={false}>
            <Grid container spacing={3}>
                <Grid size={12}>
                    <Typography variant="h4">Template</Typography>
                </Grid>
                <Grid size={12}>
                    <List>
                        <ListItem component={Link} to="/test">
                            <Typography color="primary">Test Page</Typography>
                        </ListItem>
                    </List>
                </Grid>
            </Grid>
        </Page>
    );
}

export default App;
