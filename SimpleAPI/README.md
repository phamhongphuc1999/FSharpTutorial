<h1 align="center">
  F Sharp simple api
</h1>

| ID  | Project               | Type   | Note           |
| :-- | :-------------------- | :----- | :------------- |
| 1   | [UserAPI](./UserAPI/) | webapi | Simple web api |

### Database testing

- How to execute database docker container

  ```shell
  docker exec -it dev_fsharp_sql_container bash
  ```

- Login default user with password is fsharp

  ```shell
  mysql -u root -p
  ```

- Switching to simple_app database

  ```shell
  use simple_app;
  ```

- Thông tin chi tiết tại [đây](./documents/fundametals.md)
