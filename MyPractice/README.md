<h1 align="center">
  F Sharp libraries
</h1>

### 1. Structure

| ID  | Project                             | Type     | Note                                    |
| :-- | :---------------------------------- | :------- | :-------------------------------------- |
| 1   | [ConsoleApp](./ConsoleApp/)         | console  | Test libraries in console line          |
| 2   | [MyLibrary](./MyLibrary/)           | classlib |
| 3   | [MyNumber](./MyNumber/)             | classlib | Simple big number library               |
| 4   | [MyPracticeTest](./MyPracticeTest/) | nuint    | Simple uint test for big number library |

---

### 2. How to package with docker

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
