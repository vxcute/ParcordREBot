using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gee.External.Capstone.Arm64; 
using Discord;
using Discord.Commands;
using System.Text.Encodings;
using Gee.External.Capstone;
using SharpDisasm.Translators;
using SharpDisasm;
using SharpDisasm.Udis86;
using Discord.WebSocket;
using System.Net.Http;
using paracordbot.Bot.Utilities;
using System.IO;
using paracordbot.Bot.Disassmbler;

namespace paracordbot
{
    public class BotCommands : ModuleBase<SocketCommandContext>
    {

        [Command("about")]
        public async Task AboutCommand()
        {
            var builder = new EmbedBuilder()
            .WithTitle("**Paracord RE Bot**")
            
            .WithDescription("**Hello there :smile:, I am Paracord a Discord Bot which is developed to aid reverse engineers,  I aim to support your RE work as an Assembler & Disassembler which currently supports x86, x16, x64, ARM64 architectures. If you need more architectures to be supported or any feedback or issue to be reported,  feel free to ping my creator astr0#8214.**")
            .WithTimestamp(DateTimeOffset.FromUnixTimeMilliseconds(1618153512638))
            .WithFooter(
             footer =>
            {
                footer.WithIconUrl("https://cdn.discordapp.com/embed/avatars/0.png");
            })
            .WithThumbnailUrl("https://pbs.twimg.com/profile_images/1381266030866104325/v7ENidXZ_400x400.jpg")
            .WithAuthor(author 
                
                =>
            {
                author
                    .WithName("astr0")
                    .WithUrl("https://twitter.com/0xastr0")
                    .WithIconUrl("https://pbs.twimg.com/profile_images/1382237973459128320/zkaF5x4Y_400x400.jpg");
            })

            .AddField("!cmds", "Loads help menu and supported commands.")
            .AddField("!about", "Loads a brief info about the bot.")
            .AddField("!invite", "Sends Bot Invite Link")
            .AddField("!utils", "Loads Basic Utilities that will ease your RE Process");
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, embed: embed).ConfigureAwait(false);
        }

        [Command("invite")]
        public async Task InviteCommand()
        {
            var builder = new EmbedBuilder()
            .WithTitle("Thanks For Inviting Me :smile:")
            .WithColor(new Color(0x744BF8))
            .WithDescription("[Add The Bot To Your RE Server](https://discord.com/oauth2/authorize?client_id=830230742877077565&scope=bot)")
            .WithColor(new Color(0x744BF8))
            .WithTimestamp(DateTimeOffset.FromUnixTimeMilliseconds(1618602756303))
            .WithFooter(footer => {
                footer
                    .WithText("Do You Know That RE Is +18 ?")
                    .WithIconUrl("https://pbs.twimg.com/profile_images/1383147870958845956/yg9xOAyy_400x400.jpg");
            });
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(
                null,
                embed: embed)
                .ConfigureAwait(false);
        }

        [Command("cmds")]
        public async Task HelpCommand()
        {
            await ReplyAsync("**Usage: !disasm {arch}(x64, x86,x16, arm64) {opcodes}**");
        }

        [Command("disasm")]
        public async Task DisasmCommand(string arch, [Remainder] string hex = null)
        {
            if (hex == null || arch == null)
                await ReplyAsync("**Usage: !disasm {arch}(x64, x86, x16, arm64) {opcodes}**");


            switch (arch)
            {
                case "x16": { await Disasamx86x64x16(ArchitectureMode.x86_16, hex); break; }
                case "x86": { await Disasamx86x64x16(ArchitectureMode.x86_32, hex); break; }
                case "x64": { await Disasamx86x64x16(ArchitectureMode.x86_64, hex); break; }
                case "arm64": { string armhex = hex; await DisasmArm64Cmd(armhex); break; }
                default: { await ReplyAsync("**Unsupported Architecture or You Entered A Wrong One Currently The Bot Supports x64, x86,x16 and arm64**"); break; }
            }
       }

        [Command("utils")]
        public async Task UtilsCommand(string util = null,  string option = null, [Remainder] string option2 = null)
        {
            if (util == null || option == null) {
                var builder = new EmbedBuilder()
                .WithTitle("Utils Command Page")
                .WithDescription("Basic Utilities that will ease your RE Process")
                .WithUrl("https://discordapp.com")
                .WithColor(new Color(0x82AEA2))
                .WithTimestamp(DateTimeOffset.FromUnixTimeMilliseconds(1618430172179))
                .AddField("Hex2CByteArray", "Convert Hex Bytes To A C Byte Array")
                .AddField("Hex2C#ByteArray", "Convert Hex Bytes To A C# Byte Array")
                .AddField("Hex2RustVec", "Convert Hex Bytes To A Rust !vec")
                .AddField("hex2bin", "Convert Hex To Binary")
                .AddField("bin2hex", "Convert Binary To Hex")
                .AddField("dec2hex", "Convert Decimal To Hex")
                .AddField("bin2dec", "Convert Binary To Decimal")
                .AddField("dec2bin", "Convert Decial To Binary")
                .AddField("hex2dec", "Convert Hex To Decimal")
                .AddField("b64d", "Decode Base64 To Ascii")
                .AddField("b64e", "Encode String To Base64")
                .AddField("ascii2hex", "Converts Ascii Text To Hex")
                .AddField("hex2ascii", "Converts Hex To Ascii Text")
                .AddField("rev", "Reverse A String");
                var embed = builder.Build();
                await Context.Channel.SendMessageAsync(null, embed: embed).ConfigureAwait(false);
            }
            
            switch (util)
            {
                case "Hex2CByteArray": { await ReplyAsync("`char shellcode[] = {" + string.Join(",", BytesToByteArray(option.Replace(" ",""))) + "};`"); break; }
                case "Hex2C#ByteArray": { await ReplyAsync("`byte[] shellcode = new byte[] {" + string.Join(",", BytesToByteArray(option.Replace(" ",""))) + "};`"); break; }
                case "Hex2RustVec": { await ReplyAsync("`let shellcode = vec![" + string.Join(",", BytesToByteArray(option.Replace(" ",""))) + "];`"); break; }
                case "hex2bin": {await ReplyAsync($"`{Utilities.Hex2Bin(option)}`"); break; }
                case "bin2hex": { await ReplyAsync($"`{Utilities.Bin2Hex(option)}`"); break; }
                case "dec2bin": { await ReplyAsync($"`{Utilities.Dec2Bin(option)}`"); break; }
                case "bin2dec": { await ReplyAsync($"`{Utilities.Bin2Dec(option)}`"); break; }
                case "hex2dec": { await ReplyAsync($"`{Utilities.Hex2Dec(option)}`");break; }
                case "dec2hex": { await ReplyAsync($"`{Utilities.Dec2Hex(option)}`"); break; }
                case "ascii2hex": { await ReplyAsync($"`{Utilities.Ascii2Hex(option)}`"); break; }
                case "hex2ascii": { await ReplyAsync($"`{Utilities.Hex2Ascii(option.Replace("0x","").Replace(",",""))}`"); break; }
                case "b64d": { await ReplyAsync($"`{Utilities.Base64Decoded(option)}`"); break; }
                case "b64e": { await ReplyAsync($"`{Utilities.Base64Encode(option)}`"); break; }
                case "rev": { await ReplyAsync($"`{new string(option.Reverse().ToArray())}`");break; }
            }
        }
        public async Task DisasmArm64Cmd([Remainder] string hex = null)
        {
            byte[] bytes = Utilities.HexStringToByteArray(hex.ToString().Replace("0x","").Replace(" ", ""));
            Arm64Instruction[] instructions = new DisassemblerEx().DisasmArm64(bytes);
            string[] ops = new string[instructions.Length];
            for (int i = 0; i < instructions.Length; i++)
                ops[i] = string.Concat("", instructions[i].Mnemonic + " " + instructions[i].Operand);
            await ReplyAsync($"```c\n{string.Join("\n", ops)}```");
        }

        public async Task Disasamx86x64x16( ArchitectureMode mode, [Remainder] string hex = null)
        {
            Disassembler.Translator.IncludeBinary = true;
            var disasm = new Disassembler(Utilities.HexStringToByteArray(hex.ToString().Replace("\\x","").Replace("0x","").Replace(" ", "")), mode, 0, true);
            await ReplyAsync($"```c\n{string.Join("\n", disasm.Disassemble())}```");
        }

        public string[] BytesToByteArray(string hex)
        {
            byte[] bytes = Utilities.HexStringToByteArray(hex);
            string[] bhex = new string[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
                bhex[i] = "0x" + bytes[i].ToString("X");
            return bhex;
        }
    }
}
