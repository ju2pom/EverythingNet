![](https://ci.appveyor.com/api/projects/status/dosd2rd023jgl8at?svg=true)

# EverythingNet

## What

EverythingNet is a C# library that wraps the great library from voidtools name SearchEverything.
This library let you search for files, folders and more incredibly fast.
For more information jump to the [official page](https://www.voidtools.com/)

## Why

EverythingNet provides a simple .NET api to search for file using indexing. The wrapped library doesn't not rely on Windows Search but on a specific service which is super light and super fast.

## How

Let's have a look at a simplep piece of code:

```csharp
Everything everything = new Everything();
everything.SearchText = "a_file_name.txt";
everything.MatchWholeWord = true;
ErrorCode errorCode = everything.Search(true);
```
