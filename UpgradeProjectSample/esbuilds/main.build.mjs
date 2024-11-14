import * as esbuild from 'esbuild';

await esbuild.build({
    entryPoints: ["ts/main.page.ts"],
    bundle: true,
    minify: true,
    outfile: "wwwroot/js/main.page.js",
});
