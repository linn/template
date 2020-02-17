﻿const webpack = require('webpack');

const WebpackDevServer = require('webpack-dev-server');

const config = require('./webpack.config');

new WebpackDevServer(webpack(config), {
    publicPath: config.output.publicPath,
    hot: true,
    historyApiFallback: true,
    proxy: {
        '/template/assets': {
            target: 'http://localhost:51101',
            secure: false
        }
    }
}).listen(3000, 'localhost', err => {
    if (err) {
        return console.log(err);
    }

    console.log('Listening at http://localhost:3000/');

    return 0;
});
