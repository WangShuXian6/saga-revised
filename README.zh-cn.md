以下是中文翻译：

---

# Saga 修订版

> **Saga 修订版**是《仙境传说2：世界之门》的服务器模拟器。

Saga 修订版是改进后的新版本。新系统基于输入数据，而不再依赖过多硬编码。

我们提供的数据是官方数据，主要通过一个庞大的数据包日志数据库提取。官方服务器上的玩家数量有所减少，我们的数据包日志也减少了，因此数据不完全可靠，可能与最新的计算结果不兼容。

## 目录

1. [开始使用](#开始使用)
    1. [源代码编译](#源代码编译)
    1. [设置 SQL 数据库](#设置-SQL-数据库)
    1. [设置模拟器](#设置模拟器)
2. [常见问题](#常见问题)
3. [已知问题](#已知问题)
4. [关于](#关于)

## 开始使用

### 源代码编译

#### Windows 安装说明

你需要安装 [.NET Framework 3.5](http://www.microsoft.com/en-us/download/details.aspx?id=21)，才能编译模拟器。  
然后只需在项目根目录下运行 `build.bat` 脚本，等待一切完成。

#### Linux 安装说明

要在 Linux 上编译 Saga 修订版，你必须安装 [Mono](http://www.mono-project.com/)。  
较新的 Linux 发行版不再默认包含 .NET Framework 3.5，但可以使用 .NET Framework 4.5 编译 Saga 修订版。

为此，安装 `mono-complete` 包并创建符号链接 4.5 -> 3.5，命令如下：

```bash
sudo apt install mono-complete
sudo mv /usr/lib/mono/3.5 /usr/lib/mono/3.5~
sudo ln -s 4.5 /usr/lib/mono/3.5
```

然后，运行 `build.sh` 来编译 Saga 修订版。

接下来，下载并编译 Mono 用的 Lua，方法如下：

```bash
sudo apt install lua5.1 liblua5.1-dev mono-devel
mkdir -p ~/devel
cd ~/devel
git clone https://github.com/stevedonovan/MonoLuaInterface.git
cd MonoLuaInterface/src
./configure
make
sudo ./install /usr/local/bin
```

将 `~/devel/MonoLuaInterface/bin` 中的所有文件复制到服务器二进制目录，并替换现有文件。

> Saga.Tools.Tasks 目前无法在 Mono 上编译。

### 设置 SQL 数据库

#### 安装

下载 [MySQL 安装程序](http://dev.mysql.com/downloads/windows/installer/)，并至少安装 MySQL Server 和 MySQL Workbench。然后启动 MySQL Workbench，检查 `startup / shutdown` 是否显示 MySQL Server 正在运行。

> 不支持 MySQL 版本低于 5.1。

#### 创建数据库

在 MySQL Workbench 中，创建一个名为 "saga" 的新架构（数据库），打开它并运行模拟器目录 `Database\Mysql` 中的所有 SQL 脚本。这将创建所有必要的表。

### 设置模拟器

#### Saga.Map

运行它。你将被要求创建一个世界服务器，选择 ID 1，设置最大玩家人数和密码。设置利率，输入 1 加载所有插件，最后两次选择 "No"，然后对最后一个问题选择 "Yes"。输入数据库设置，如下所示：

```bash
database name: saga
database username: root
database password: <root_password>
database port: 3306
database host: localhost
```

关闭它。

#### Saga.Authentication

运行它并输入 1 加载插件。接着对接下来的两个问题选择 "No"，最后一个问题选择 "Yes"。使用与上述相同的数据库设置：

```bash
database name: saga
database username: root
database password: <root_password>
database port: 3306
database host: localhost
```

关闭它。

#### Saga.Gateway

运行它并对所有问题选择 "No"。然后关闭它。

#### 将世界服务器添加到数据库

打开 `Saga.Map.config`，找到类似 `<Saga.Manager.NetworkSettings world="1" playerlimit="50" proof="2C6CFC5F906506F46HHKIU679Y6J8Y7K">` 的行，并将 `proof` 复制到某处。打开 MySQL Workbench，创建一个新的查询标签，执行以下命令：

```sql
INSERT INTO `list_worlds` (`Id`, `Name`, `Proof`)
VALUES (1, '<world_name>', '<proof>');
```

将 `<world_name>` 替换为你想要的任何名称，`<proof>` 替换为在 `Saga.Map.config` 中找到的相同 proof 值。  
现在完成了，可以启动服务器。

## 常见问题

### 我无法在 Windows 上编译模拟器

有时 `build.bat` 脚本不能直接工作。请检查 `MSBuild.exe` 的路径是否正确，可以编辑脚本或尝试以管理员身份运行。

### 我在哪里可以获取游戏客户端？

你可以下载最新的 [DiviniaRO2 完整客户端](https://mega.co.nz/#!yZhlkB5S!j6zia8kE_uLZ65WaJavDS-nVvq7-vyDgtGfRIbcmm9E)。

### 如何创建一个玩家账户？

在 `Saga.Authentication` 控制台输入 `account -create <username> <password> male` 来创建一个男性账户，替换 `<username>` 和 `<password>` 为你想要的内容（使用 `female` 创建女性账户）。

### 如何用我的游戏客户端连接到服务器？

你需要带上一些选项运行游戏可执行文件。例如，你可以使用以下批处理脚本运行命令：`"System\RagII.exe" ServerIP=127.0.0.1 ServerPort=64000`。

### 我无法登录我的玩家账户

如果你无法登录到你的玩家账户，重启服务器并在尝试登录之前，在 `Saga.Gateway` 控制台输入 `host -connect`。每次启动服务器后只需要执行一次该命令，之后登录就不需要重复操作。

## 已知问题

- 没有 Lua 命令将新武器添加到你的物品栏。我们确实拥有所有的数据包，并知道如何使用它们。
  
- 在对其他人施放表情时存在目标锁定问题。我们需要调试此问题的原因。  
  要调试服务器，从二进制文件夹启动服务器，在 Visual Studio 中选择 `Tools -> Attach to process -> Saga.Map.Exe`。  
  在执行攻击操作的函数处设置断点，使用中间窗口查看哪个表达式返回为 `false`。

- AI 仍然有点笨。我们需要找到一种方法来读取所有具有多个高度平面的封闭房间的高度信息以修复此问题。  
  这将花费很长时间，所以暂时忍受这个笨拙的 AI。

- 如果计算机在加载 Lua 任务文件时极度负载，可能会导致问题。  
  Lua 使用 ASCII 编码读取文件，因此如果你用 Unicode 编码保存文件，也会出现问题。  
  解决方法是预加载所有任务文件，并添加一个重新加载命令。

- MySQL 版本低于 5.1 不受支持，且永远不会支持。但 MySQL 6.x 和 5.1 版本是支持的。

- 服务器从未在高负载环境和 Mono 下进行过测试。请注意，在这方面可能会遇到 bug 或不兼容的情况。

## 关于

### 作者

* phr34k, Zenzija，以及所有原始 SagaRevised 贡献者
* [kalel60](https://www.assembla.com/profile/kalel60), [Sebda](https://www.assembla.com/profile/Sebda), 以及所有 [SIN](https://app.assembla.com/spaces/stilleinnorden) 贡献者
* **Darkin** - *开发者* - [JulienGrv](https://github.com/JulienGrv)
* [所有贡献者](https://github.com/JulienGrv/saga-revised/contributors)

### 许可证

本项目采用 **Creative Commons 公共许可证**（CC BY-NC-SA 4.0）许可，详细信息请参见 [LICENSE](https://github.com/JulienGrv/saga-revised/blob/master/LICENSE)。

**[返回目录](#目录)**