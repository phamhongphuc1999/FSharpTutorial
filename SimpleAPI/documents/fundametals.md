<h1 align="center">
  :sparkles::sparkles::sparkles:Fundametals:sparkles::sparkles::sparkles:
</h1>

## Mục lục

1. [Overview](#overview)
2. [Host](#host)
   1. [Generic Host](#generic_host)
   2. [Web Host](#web_host)
3. [Configuration](#configuration)
4. [Database](#database)
   1. [Mysql Docker Container](#mysql_docker_container)
   2. [MySqlConnector](#mysqlconnector)

---

### 1. Overview <a name="overview"></a>

Nội dung bài viết này sẽ cũng cấp các thông tin về kiến trúc, thành phần của một ASP.NET Core API theo ý hiểu của tác giả cũng như các công nghệ khác được sử dụng trong chương trình. Phần lớn nội dung được viết dựa trên tài liệu của [Microsoft](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/?view=aspnetcore-6.0&tabs=macos)

---

### 2. Host <a name="host"></a>

Ứng dụng ASP.NET sẽ xây dựng một host. Nó sẽ đóng gói tất cả các tài nguyên của chương trình, bao gồm

- An HTTP server implementation
- Middleware components
- Logging
- [Dependency injection (DI) services](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0)
- Configuration

Có ba loại hosts khác nhau

- .NET WebApplication Host, hay còn gọi là Minimal Host
- .NET Generic Host
- ASP.NET Core Web Host

Trong đó chương trình Simple API sử dung .NET Core, ASP.NET Core Web Host

#### Generic Host <a name="generic_host"></a>

- ASP.NET Core templates cung cấp [WebApplicationBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplicationbuilder?view=aspnetcore-6.0) và [WebApplication](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication?view=aspnetcore-6.0), những class này sẽ cung cấp các cách để config và run ứng dụng web mà không cần Starup class.

#### Web Host <a name="web_host"></a>

##### Setup a host

```shell
module Program =
    let CreateHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webBuilder ->
                webBuilder.UseStartup<Startup>() |> ignore
            )

    [<EntryPoint>]
    let Main args =
        CreateHostBuilder(args).Build().Run()
        0
```

---

### 3. Configuration <a name="configuration"></a>

---

### 4. Database <a name="database"></a>

Chương trình sử dụng cơ sở dữ liệu mysql thông qua docker. Phần 2 này sẽ đề cập đến hai vấn đề là

- Setup cho Mysql docker container
- Kết nối API với mysql

#### Mysql Docker Container <a name="mysql_docker_container"></a>

```shell
docker-compose -f docker-compose-mysql.yaml up -d
```

- file `docker-compose-mysql.yaml` chứa các config cho mysql
  - MYSQL_ROOT_PASSWORD: đặt password cho user root là fsharp
  - ports: đặt port là 3306
  - volumes: sử dụng để khởi tạo một dữ liệu cho database

Thông tin chi tiết tại [đây](https://hub.docker.com/_/mysql)

###### Chạy thử container

- Execute container

```shell
docker exec -it dev_fsharp_sql_container bash
```

- Đăng nhập với user root

```shell
mysql -u root -p
```

- Chuyển sang cơ sở dữ liệu FsharpApp

```shell
use FsharpApp
```

- Truy vấn vào bảng Employees

```shell
SELECT * FROM Employees;
```

#### MySqlConnector <a name="mysqlconnector"></a>

```shell
dotnet add package MySqlConnector
```

- Cách thức tạo connection tại [đây](https://github.com/phamhongphuc1999/FSharpTutorial/tree/main/SimpleAPI/UserAPI/Connector)
- Thông tin chi tiết tại [đây](https://mysqlconnector.net/)
