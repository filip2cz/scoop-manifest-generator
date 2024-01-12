# scoop-manifest-generator
Scoop manifest generator originaly created for https://github.com/ur-fault/TMaze


## What is this:
Tool for automation, basically alternative for official scoop update script.

## What this is NOT:
Tool for manually creating manifests. If you want to create manifest, use `scoop create {url}` command.

## Config file
There is one config file: `config.json`. It is simple JSON file. It is working in same way as scoop manifest file. See example below:

```JSON
{
    "manifestName": "gifski.json",
    "version": "1.6.4",
    "description": "GIF encoder based on libimagequant (pngquant).",
    "homepage": "https://gif.ski",
    "license": "AGPL-3.0-or-later",
    "url": "https://gif.ski/gifski-1.6.4.zip",
    "bin": "gifski.exe"
}
```

If you include `versionCommand` in config.json, you can include console command to get version as string. You can then replace text of version with {version}. Command should be created for OS you are running, example below is for powershell:
```JSON
{
    "manifestName": "gifski.json",
    "description": "GIF encoder based on libimagequant (pngquant).",
    "homepage": "https://gif.ski",
    "license": "AGPL-3.0-or-later",
    "url": "https://gif.ski/gifski-{version}.zip",
    "bin": "gifski.exe",
    "versionCommand": "$gifskiVerFull = .\\gifski.exe --version; $gifskiVer = $gifskiVerFull.Replace(\\\"gifski \\\", \\\"\\\"); Write-Output $gifskiVer"
}
```

Yes, you need to use `/` three times!