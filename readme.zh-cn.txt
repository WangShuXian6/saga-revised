翻译如下：

**Saga**，以前被称为修订版代号**Saga**，是我们改进后的新版本（我们相信）。这个项目的新系统基于输入数据，而不是过多的硬编码。

我们提供的数据是官方数据，主要通过一个巨大的数据包日志数据库提取。官方服务器上的玩家数量有所减少，我们的数据包日志也有所减少，因此数据并不完全可靠，可能与最新的计算结果不兼容。

**开始使用：**

如果你是第一次编译Saga，我们建议你设置MSBuild，设置方法请参见这里：
[http://en.csharp-online.net/MSBuild:_By_Example%E2%80%94Integrating_MSBuild_into_Visual_Studio](http://en.csharp-online.net/MSBuild:_By_Example%E2%80%94Integrating_MSBuild_into_Visual_Studio)。然后使用`Build.Development.Proj`构建文件来编译所有所需的依赖项目/解决方案和默认插件。

要使用bat文件编译，可以使用以下命令：
`"%WINDIR%\Microsoft.NET\Framework\v3.5\MSBuild.exe" Build.Development.proj`

**已知问题：**

- 没有Lua命令来将新武器添加到你的物品栏。我们确实有所有的包，并且知道如何使用它们。
  
- 在对其他人施放表情时存在目标锁定问题。需要调试这个问题。  
  要调试服务器，从二进制文件夹运行服务器，在Visual Studio中选择`Tools -> Attach to process -> Saga.Map.Exe`，并在执行攻击操作的函数处设置断点，使用中间窗口检查哪些表达式返回了false。

- AI仍然有点笨。我们需要找到一种方法来读取所有包含多个高度平面的封闭房间的高度信息来解决这个问题。这将花费很长时间，所以请暂时忍受这个笨拙的AI。

- 如果计算机在加载Lua任务文件时极度负载，可能会出现问题。Lua读取文件时使用的是ASCII编码，因此如果你用Unicode编码保存文件，也会导致问题。一个解决办法是预加载所有任务文件，并添加重新加载命令。

- 不支持Mysql版本低于5.1的版本。也永远不会支持。不过，Mysql 6.x和5.1版本是支持的。

- 服务器从未在高负载环境和Mono下测试过。请注意，可能会遇到 bug 或不兼容的情况。