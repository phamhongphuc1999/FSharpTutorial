### F Sharp libraries

| ID  | Project                             | Type     | Note                           |
| :-: | :---------------------------------- | :------- | :----------------------------- |
|  1  | [ConsoleApp](./ConsoleApp/)         | console  | Test libraries in console line |
|  2  | [MyLibrary](./MyLibrary/)           | classlib |
|  3  | [MyNumber](./MyNumber/)             | classlib | My simple big number library   |
|  4  | [MyPracticeTest](./MyPracticeTest/) | nuint    | My uint test                   |

## How to package with docker

- I has write a simple [script](./scripts/main.sh) for run all necessary steps

  - First, it untag old image if it find a current image
  - Then, it build a new image
  - It stop and remove old container
  - After that, it continue remove old image
  - Finally, it run new container

- To run docker, you can run

```shell
./scripts/main.sh
```
