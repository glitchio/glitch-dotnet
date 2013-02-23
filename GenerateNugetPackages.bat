C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe Glitch.Notifier.sln /property:Configuration=Release

cd nuget\Glitch.Notifier
..\nuget.exe pack
cd ..\Glitch.Notifier.AspNet
..\nuget.exe pack