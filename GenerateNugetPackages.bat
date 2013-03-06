C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe Glitch.Notifier.sln /property:Configuration=Release

Tools\ILMerge.exe /targetplatform:2 /ndebug /internalize /out:"nuget\Glitch.Notifier\lib\35\Glitch.Notifier.dll" "Glitch.Notifier\bin\Release\Glitch.Notifier.dll" "Glitch.Notifier\bin\Release\ServiceStack.Text.dll"

copy Glitch.Notifier.AspNet\bin\Release\Glitch.Notifier.AspNet.dll nuget\Glitch.Notifier.AspNet\lib\35

copy Glitch.Notifier.AspNet.Mvc\bin\Release\Glitch.Notifier.AspNet.Mvc.dll nuget\Glitch.Notifier.AspNet.Mvc\lib\35

copy Glitch.Notifier.AspNet.WebApi\bin\Release\Glitch.Notifier.AspNet.WebApi.dll nuget\Glitch.Notifier.AspNet.WebApi\lib\40

cd nuget\Glitch.Notifier
..\..\tools\nuget.exe pack
cd ..\Glitch.Notifier.AspNet
..\..\tools\nuget.exe pack
cd ..\Glitch.Notifier.AspNet.Mvc
..\..\tools\nuget.exe pack
cd ..\Glitch.Notifier.AspNet.WebApi
..\..\tools\nuget.exe pack