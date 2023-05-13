<h1 align="center">
  F Sharp libraries
</h1>

### Solution

| ID  | Solution                    | Note             |
| :-- | :-------------------------- | :--------------- |
| 1   | [MyPractice](./MyPractice/) | Simple libraries |
| 2   | [SimpleAPI](./SimpleAPI/)   | Simple API       |

### Format

The project use `pre-commit` and python environment to install some useful package that check your code before pushing code in github. If you want to try this solution, you must create python virtual environment firstly

```shell
python -m venv venv
```

And active your environment

```shell
source venv/bin/activate
```

After that, install pre-commit package

```shell
pip3 install -r requirements.txt
```

Install pre-commit to github hook

```shell
pre-commit install
```

Try to run

```shell
pre-commit run --all-file
```
