<h1 align="center">
  Fundametals
</h1>

## Mục lục

1. [Overview](#overview)
2. [Host](#host)
   1. [Generic Host](#generic_host)
   2. [Web Host](#web_host)
3. [Configuration](#configuration)
4. [Swagger](#swagger)
5. [Database](#database)
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

- [.NET WebApplication Host](https://docs.microsoft.com/en-us/aspnet/core/migration/50-to-60?view=aspnetcore-6.0&tabs=visual-studio#new-hosting-model), hay còn gọi là Minimal Host
- [.NET Generic Host](#generic_host)
- [ASP.NET Core Web Host](#web_host)

#### 2.1. Generic Host <a name="generic_host"></a>

- ASP.NET Core templates cung cấp [WebApplicationBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplicationbuilder?view=aspnetcore-6.0) và [WebApplication](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication?view=aspnetcore-6.0), những class này sẽ cung cấp các cách để config và run ứng dụng web mà không cần Starup class.

#### 2.2. Web Host <a name="web_host"></a>

#### _2.2.1. Setup a host_

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

- Cấu hình ứng dụng ASP.NET được thể hiện bằng cách sử dụng một hoặc nhiều [configuration providers](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-6.0#cp). Các Configuration provider này đọc dữ liệu từ các cặp key-value từ nhiều nguồn khác nhau

  - Settings files, such as `appsettings.json`
  - Environment variables
  - Azure Key Vault
  - Azure App Configuration
  - Command-line arguments
  - Custom providers, installed or created
  - Directory files
  - In-memory .NET objects

- Ứng dụng Simple API sử dụng các đơn giản nhất là dùng file cấu hình `appsettings.json`. Bằng việc sử dụng [ConfigurationBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationbuilder?view=dotnet-plat-ext-6.0), chúng ta có thể lưu cấu hình thành file json sau đó lấy chúng ra trong lúc `run time`.

```shell
let CreateDbConnection () =
        let config = ConfigurationBuilder().AddJsonFile("appsettings.json", false).Build()
        let sqlConfig = config.GetSection("MySqlSetting")
        APIConnection.GetConnection(sqlConfig)
```

- Ở ví dụ trên, sử dụng ConfigurationBuilder để đọc dữ liệu trong file appsettings.json. Sau đó có thể sử dụng hàm GetSection để lấy ra các phần của các dữ liệu dưới dạng object.

---

### 4. Swagger <a name="swagger"></a>

---

### 5. Database <a name="database"></a>

Chương trình sử dụng cơ sở dữ liệu mysql thông qua docker. Phần này sẽ đề cập đến hai vấn đề như sau

- [Setup cho Mysql docker container](#mysql_docker_container)
- [Kết nối API với mysql](#mysqlconnector)

#### 5.1. Mysql Docker Container <a name="mysql_docker_container"></a>

```shell
docker-compose -f docker-compose-mysql.yaml up -d
```

- file [docker-compose-mysql.yaml](../docker-compose-mysql.yaml) chứa các cài đặt cho mysql
  - MYSQL_ROOT_PASSWORD: đặt password cho user root là fsharp
  - ports: đặt port là 3306
  - volumes: sử dụng để khởi tạo một dữ liệu cho database

Thông tin chi tiết tại [đây](https://hub.docker.com/_/mysql)

#### _5.1.1. Chạy thử container_

- Execute container

```shell
docker exec -it dev_fsharp_sql_container bash
```

- Đăng nhập với user root

```shell
mysql -u root -p
```

- Chuyển sang cơ sở dữ liệu simple_app

```shell
use simple_app;
```

- Truy vấn vào bảng Employees

```shell
SELECT * FROM Employees;
```

#### 5.2. MySqlConnector <a name="mysqlconnector"></a>

```shell
dotnet add package MySqlConnector
```

- Cách thức tạo connection tại [đây](https://github.com/phamhongphuc1999/FSharpTutorial/tree/main/SimpleAPI/UserAPI/Connector)
- Thông tin chi tiết tại [đây](https://mysqlconnector.net/)
