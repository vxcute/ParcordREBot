using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using Newtonsoft;
using Microsoft.Extensions.DependencyInjection;

namespace paracord
{
    class Program : ModuleBase<SocketCommandContext>
    {
        public static string token = string.Empty;

        static void Main(string[] args)
        {
            new Program().RunBotAsync().GetAwaiter().GetResult();
        }

        private DiscordSocketClient DiscordClient;
        private CommandService Commands;
        private IServiceProvider Services;

        public void PrintLogo()
        {
            string[] logo = new string[]
                 {
                        "   ▄▄▄· ▄▄▄· ▄▄▄   ▄▄▄·  ▄▄·       ▄▄▄  ·▄▄▄▄  ",
                        "  ▐█ ▄█▐█ ▀█ ▀▄ █·▐█ ▀█ ▐█ ▌▪▪     ▀▄ █·██▪ ██ ",
                        "   ██▀·▄█▀▀█ ▐▀▀▄ ▄█▀▀█ ██ ▄▄ ▄█▀▄ ▐▀▀▄ ▐█· ▐█▌",
                        "   ▐█▪·•▐█ ▪▐▌▐█•█▌▐█ ▪▐▌▐███▌▐█▌.▐▌▐█•█▌██. ██ ",
                        "   .▀    ▀  ▀ .▀  ▀ ▀  ▀ ·▀▀▀  ▀█▄▀▪.▀  ▀▀▀▀▀▀• ",
                };
    
          foreach(var Logo in logo) Console.WriteLine($"\t\t\t\t{Logo}");
        }

        class TokenInfo
        {
            public string token_value;  
        }; 

        public static string LoadJson(string jsonfile)
        {
            TokenInfo token_info = JsonConvert.DeserializeObject<TokenInfo>(File.ReadAllText(jsonfile));
            string token = token_info.token_value;
            return token;
        }

        public async Task RunBotAsync()
        {
            token = LoadJson("appsettings.json");
            PrintLogo();

            DiscordClient = new DiscordSocketClient();
         
            Commands = new CommandService();

            Services = new ServiceCollection()
                .AddSingleton(DiscordClient)
                .AddSingleton(Commands)
                .BuildServiceProvider();


            DiscordClient.Log += Log;

            await RegisterCommandsAsync();

            await DiscordClient.LoginAsync(TokenType.Bot, token);

            await DiscordClient.StartAsync();

            await DiscordClient.SetStatusAsync(UserStatus.Online);

            await DiscordClient.SetGameAsync("!about For RE Nerds");

            await Task.Delay(Timeout.Infinite);
        }

        private Task Log(LogMessage arg)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\t\t\t\t"+arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            DiscordClient.MessageReceived += HandleCommandAsync;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), Services);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;
            
            var context = new SocketCommandContext(DiscordClient, msg);
            
            if (msg.Author.IsBot) return;

            if (msg.Author.Id == context.Channel.Id)
                return;

            int argPos = 0;
            if (msg.HasStringPrefix("!", ref argPos))
            {
                var result = await Commands.ExecuteAsync(context, argPos, Services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
                if (result.Error.Equals(CommandError.UnmetPrecondition)) await msg.Channel.SendMessageAsync(result.ErrorReason);
            }
            else
            {
                await ReplyAsync("Use ! Prefix Before Every Command :smile:"); 
            }
        }
    }
}