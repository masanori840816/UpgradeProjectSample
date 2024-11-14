import * as esbuild from 'esbuild';

await esbuild.build({
    entryPoints: ["ts/signin.page.ts"],
    bundle: true,
    minify: true,
    outfile: "wwwroot/js/signin.page.js",
});
