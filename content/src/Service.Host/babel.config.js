module.exports = {
    presets: [
        ['@babel/preset-env', { targets: 'node 8', modules: 'commonjs' }],
        '@babel/preset-react'
    ],
    plugins: ['@babel/proposal-class-properties', '@babel/proposal-object-rest-spread']
};
