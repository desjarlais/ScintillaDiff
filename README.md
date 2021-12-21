# ScintillaDiff
A class library for comparing two text files with the ScintillaNET control.

[![Nuget](https://img.shields.io/nuget/v/ScintillaDiff.NET)](https://www.nuget.org/packages/ScintillaDiff.NET/)

### Features
* Customizable indicator images
* Customizable indicator colors
* A List and a side-by-side views
* Access to the underlying Scintilla controls for styling, etc...
* **NOTE:** This is a Windows Forms control

## Used libraries
* [DiffPlex](https://github.com/mmanela/diffplex)
* [ScintillaNET](https://github.com/jacobslusser/ScintillaNET)
* [Some of my own](https://github.com/VPKSoft)

## The .NET package
The package ending with .NET is depended upon the new Scintilla 5 series [Scintilla.NET](https://www.nuget.org/packages/Scintilla.NET/). The other packages Scintilla dependency is no longer being maintained even though the codebase of the dependent software may be maintained.

## Thanks to
* [bobhodge](https://github.com/bobhodge) For implementing horizontal scrolling sync; plus mousewheel scrolling.
* [XJmichaelw](https://github.com/XJmichaelw) For adding IsEntireLineHighlighted attribute, which when set to true, will set the entire background of a line to a given colour corresponding to the change type for that line. And a bug fix leading to a crash when a form containing the control was minimized.
* [avstri](https://github.com/avstri) for pointing out the speed up via a StringBuilder [#1](https://github.com/VPKSoft/ScintillaDiff/issues/1).

* [igitur](https://github.com/igitur) For pointing out the .NET Framework 4 level-drop requirement

* [goroggy](https://github.com/goroggy) For pointing out the need for a character diff ([#7](https://github.com/VPKSoft/ScintillaDiff/issues/7)).

* [JetBrains](http://www.jetbrains.com) for their open source license(s).

* [![VPKSoft](https://circleci.com/gh/VPKSoft/ScintillaDiff.svg?style=shield)](https://app.circleci.com/pipelines/github/VPKSoft/ScintillaDiff)

![JetBrains](http://www.vpksoft.net/site/External/JetBrains/jetbrains.svg)

## Screen-shots
_**A side-by-side diff**_

![image](https://user-images.githubusercontent.com/40712699/58415622-b230e580-8087-11e9-913e-7c95572416a5.png)

**_A list diff_**

![image](https://user-images.githubusercontent.com/40712699/58415657-d096e100-8087-11e9-8f87-d4a5e459fc9c.png)
