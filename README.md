![](https://ci.appveyor.com/api/projects/status/dosd2rd023jgl8at?svg=true)

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
var searchResult = everyThing
 .Is("John")
 .And()
 .Is("Doe")
 .Search(true);
```

```csharp
Everything everything = new Everything();
var searchResult = everyThing
 .Is("*.jpg")
 .Or()
 .Is("*.png")
 .Search(true);
```

```csharp
Everything everything = new Everything();
var searchResult = everyThing
 .Is("*.jpg")
 .And().Not()
 .Is("test.png")
 .Search(true);
```

2. File size

```csharp
Everything everything = new Everything();
var searchResult = everyThing
 .Size()
 .Is(100) // By default the unit is kb
 .Search(true);
```

```csharp
Everything everything = new Everything();
var searchResult = everyThing
 .Size()
 .GreaterThan(100).Mb()
 .Search(true);
```

```csharp
Everything everything = new Everything();
var searchResult = everyThing
 .Size()
 .Between(100, 200, SizeUnit.Mb)
 .Search(true);
```

3. Music files

```csharp
Everything everything = new Everything();
var searchResult = everyThing
 .Artist("RadioHead")
 .Search(true);
```

```csharp
Everything everything = new Everything();
var searchResult = everyThing
 .Genre("Rock")
 .Search(true);
```

```csharp
Everything everything = new Everything();
var searchResult = everyThing
 .Artist("RadioHead")
 .Album("Pablo Honey")
 .Track().Between(1, 3)
 .Search(true);
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
