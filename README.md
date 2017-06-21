[![Build status](https://img.shields.io/appveyor/ci/ju2pom/everythingnet/master.svg?style=flat)](https://ci.appveyor.com/project/ju2pom/everythingnet/branch/master)
[![Coverage Status](https://coveralls.io/repos/github/ju2pom/EverythingNet/badge.svg?branch=master)](https://coveralls.io/github/ju2pom/EverythingNet?branch=master)
[![NuGet](https://img.shields.io/nuget/v/EverythingNet.svg?style=flat)]()

# EverythingNet ![](http://www.voidtools.com/forum/styles/prosilver/theme/images/site_logo.gif)

## What

EverythingNet is a C# library that wraps the great library from voidtools name SearchEverything.
This library let you search for files, folders and more incredibly fast.
For more information jump to the [official page](https://www.voidtools.com/)

## Why

EverythingNet provides a simple .NET api to search for file using indexing. The wrapped library doesn't not rely on Windows Search but on a specific service which is super light and super fast.

## How

The library exposes a fluent API that ease access to specific search functions.

1. Logic

```csharp
Everything everything = new Everything();
var searchResult = everything.Search()
 .Name("Report")
 .Or()
 .Name("Results");
```

```csharp
Everything everything = new Everything();
var searchResult = everything.Search()
 .Not.Name("temp")
 .And()
 .Not.Name("tmp");
```

2. Name

```csharp
Everything everything = new Everything();
var searchResult = everything.Search()
 .Name()
 .StartWith("progra");
```


```csharp
Everything everything = new Everything();
var searchResult = everything.Search()
 .Name()
 .EndstWith("result");
```

3. Date

```csharp
Everything everything = new Everything();
var searchResult = everything.Search()
 .ModificationDate() // Can be AccessDate, RunDate, CreationDate
 .Equal(Dates.Today);  // Can be many many values !
```

```csharp
Everything everything = new Everything();
var searchResult = everything.Search()
 .CreationDate()
 .Before(Dates.Yesterday);
```

```csharp
Everything everything = new Everything();
var searchResult = everything.Search()
 .CreationDate()
 .Equal(Dates.ThisMonth);
```

```csharp
Everything everything = new Everything();
var searchResult = everything.Search()
 .CreationDate()
 .Last(Dates.Week);
```

```csharp
Everything everything = new Everything();
var searchResult = everything.Search()
 .AccessDate()
 .Between(Dates.LastWeek, Dates.ThisWeek);
```

4. File size

```csharp
Everything everything = new Everything();
var searchResult = everything.Search()
 .Size()
 .Equal(100); // By default the unit is kb
```

```csharp
Everything everything = new Everything();
var searchResult = everything.Search()
 .Size()
 .GreaterThan(100, SizeUnit.Mb); // Unit is optional
```

```csharp
Everything everything = new Everything();
var searchResult = everything.Search()
 .Size()
 .Between(100, 200, SizeUnit.Mb); // Unit is optional
```

5. Music files

```csharp
Everything everything = new Everything();
var searchResult = everything.Search()
 .Music()
 .Artist("RadioHead");
```

```csharp
Everything everything = new Everything();
var searchResult = everything.Search()
 .Music()
 .Genre("Rock");
```

```csharp
Everything everything = new Everything();
var searchResult = everything.Search()
 .Music()
 .Artist("RadioHead")
 .Album("Pablo Honey")
 .Track().Between(1, 3);
```

6. File info

```csharp
Everything everything = new Everything();
var searchResult = everything
 .File()
 .Only();
```

```csharp
Everything everything = new Everything();
var searchResult = everything
 .File()
 .Audio(); // Can be zip, video, picture, executable, document
```

```csharp
Everything everything = new Everything();
var searchResult = everything
 .File()
 .Extension("csproj");
```

```csharp
Everything everything = new Everything();
var searchResult = everything
 .File()
 .Extensions(new string[] {"csproj", "cs", "xaml"});
```

## Roadmap

The fluent API provides specific methods to easily handle
- [x] thread safety
- [x] logical operators
- [x] file size
- [x] picture files (standard format and properties)
- [x] music files (ID3 Tags)
- [x] file dates (soon)
- [ ] file content (soon)
