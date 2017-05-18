![](https://ci.appveyor.com/api/projects/status/dosd2rd023jgl8at?svg=true)

# EverythingNet ![](http://www.voidtools.com/forum/styles/prosilver/theme/images/site_logo.gif)

## What

EverythingNet is a C# library that wraps the great library from voidtools name SearchEverything.
This library let you search for files, folders and more incredibly fast.
For more information jump to the [official page](https://www.voidtools.com/)

## Why

EverythingNet provides a simple .NET api to search for file using indexing. The wrapped library doesn't not rely on Windows Search but on a specific service which is super light and super fast.

## How

The library exposes a fluent API that lets you for instance use logical operators:

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

## Roadmap

The fluent API provides specific methods to easily handle
- [x] thread safety
- [x] logical operators
- [x] file size
- [x] picture files (standard format and properties)
- [ ] music files (ID3 Tags)
- [ ] file dates (soon)
- [ ] file content (soon)
