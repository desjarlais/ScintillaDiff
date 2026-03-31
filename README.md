# ScintillaDiff
A class library for comparing two text files with the ScintillaNET control.

[![Nuget](https://img.shields.io/nuget/v/ScintillaDiff5.NET)](https://www.nuget.org/packages/ScintillaDiff5.NET/)
[![.NET Desktop](https://github.com/desjarlais/ScintillaDiff/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/desjarlais/ScintillaDiff/actions/workflows/dotnet-desktop.yml)

### Features
* Customizable indicator images
* Customizable indicator colors
* A List and a side-by-side views
* Access to the underlying Scintilla controls for styling, etc...
* **NOTE:** This is a Windows Forms control

## Used libraries
* [DiffPlex](https://github.com/mmanela/diffplex)
* [ScintillaNET](https://github.com/desjarlais/ScintillaNET)
* [Some of my own](https://github.com/VPKSoft)

## The .NET package
The package ending with .NET is depended upon the new Scintilla 5 series [Scintilla.NET](https://www.nuget.org/packages/Scintilla5.NET/) and is in active development. The other package's Scintilla dependency is no longer being maintained even though the codebase of the dependent software may be maintained.

## Screen-shots
_**A side-by-side diff**_

![image](https://user-images.githubusercontent.com/40712699/58415622-b230e580-8087-11e9-913e-7c95572416a5.png)

**_A list diff_**

![image](https://user-images.githubusercontent.com/40712699/58415657-d096e100-8087-11e9-8f87-d4a5e459fc9c.png)
