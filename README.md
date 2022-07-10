F Sharp tutorial

### How to create your tutorial

1. Create solution and projects
```shell
cd ./FSharpTutorial
```

```shell
dotnet new sln
```

```shell
dotnet new console -lang "F#" -o ConsoleTest
```

```shell
dotnet new classlib -lang "F#" -o BigNumber
```

```shell
dotnet new classlib -lang "F#" -o MyLibrary
```

2. referent library to console test project
```shell
cd ConsoleTest
```

```shell
dotnet add reference ../BigNumber
```

```shell
dotnet add reference ../MyLibrary
```

3. Build and Run console test
```shell
dotnet run
```
