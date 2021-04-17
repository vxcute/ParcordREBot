# ParcordREBot

About:
------
Paracord Is a Discord Bot That Works As An Assembler & Disassmbler It Also Contains Basic Utils That Will Ease Your RE Process

Libraries Used: 
---------------
[Discord.Net](https://github.com/discord-net/Discord.Net) [SharpDisasm](https://github.com/spazzarama/SharpDisasm) [Capstone.Net](https://github.com/9ee1/Capstone.NET) [Json.Net](https://www.newtonsoft.com/json)

Building:
---------

Windows-x64: dotnet publish ParacordREBot.sln -r win-x64 --self-contained true -o windows 

Linux-x64: dotnet publish ParacordREBot.sln -r linux-x64 --self-contained true -o linux

For x86 Change `-x64` to `x86`

