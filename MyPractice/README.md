F Sharp libraries

### How to create your library

1. Create solution and projects
```shell
cd ./FSharpTutorial/MyPractice
```

```shell
dotnet new sln
```

```shell
dotnet new console -lang "F#" -o ConsoleApp
```

```shell
dotnet new classlib -lang "F#" -o MyNumber
```

```shell
dotnet new classlib -lang "F#" -o MyLibrary
```

2. referent library to console test project
```shell
cd ConsoleTest
```

```shell
dotnet add reference ../MyNumber
```

```shell
dotnet add reference ../MyLibrary
```

3. Build and Run console test
```shell
dotnet run
```
