var path = require('path');

module.exports = {
    mode: 'development',
    entry: {
        "main.page": "./ts/main.page.ts",
        "signin.page": "./ts/signin.page.ts",
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/
            }
        ]
    },
    resolve: {
        extensions: [ '.tsx', '.ts', '.js' ]
    },
    output: {
        filename: '[name].js',
        path: path.resolve(__dirname, "wwwroot/js"),
        library: 'Page',
        libraryTarget: 'umd'
    }
};