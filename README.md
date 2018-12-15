[![Build status](https://img.shields.io/appveyor/ci/ju2pom/everythingnet/master.svg?style=flat)](https://ci.appveyor.com/project/ju2pom/everythingnet/branch/master)
[![Coverage Status](https://coveralls.io/repos/github/ju2pom/EverythingNet/badge.svg?branch=master)](https://coveralls.io/github/ju2pom/EverythingNet?branch=master)
[![NuGet](https://img.shields.io/nuget/v/EverythingNet.svg?style=flat)]()

# EverythingNet ![](media/site_logo.gif)

## Check demo app

https://github.com/ju2pom/EverythingNetDemo

## What

EverythingNet is a C# library that wraps the great library from voidtools named Everything. This library lets you search for files and folders incredibly fast. For more information jump to the [official page](https://www.voidtools.com/)

EverythingNet provides a simple .NET API that wraps aforementioned library (which is coded in C). It doesn't rely on Windows Search at all but on a specific service which is much faster and lighter.

EverythingNet exposes a fluent API that ease access to specific search functions.

## Features

The fluent API provides the following set of features:
- [x] thread safety
- [x] search for files only or folders only or both
- [x] name contains/start with/end with
- [x] search by extension or list of extensions
- [x] logical operators (Not, And, Or)
- [x] size search criteria
- [x] picture properties search criteria (format, dimensions)
- [x] audio search criteria (ID3 Tags)
- [x] dates search criteria (creation, modification, access, execution)
- [ ] file content (maybe later)

## How

The library exposes a fluent API that ease access to specific search functions.
Here is a very simple example:

```csharp
IEverything everything = new Everything();
var results = everything.Search().Name.Contains("temp");
```

## Wiki
[Wiki](https://github.com/ju2pom/EverythingNet/wiki)
