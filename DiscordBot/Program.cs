using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace DiscordBot
{
    public class Program
    {

        private DiscordSocketClient _client;

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            //Environment.SetEnvironmentVariable("TESSDATA_PREFIX", @"C:\PRCl_C#\DiscordBot\DiscordBot");
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.Ready += Ready;
            _client.MessageReceived += MessageReceivedAsync;

            await _client.LoginAsync(TokenType.Bot, "ODQ3MTU1MTY2OTgxNjUyNDkw.YK58uw.kQoK9HqX5eHIbwVL5RaPOojgto8");
            await _client.StartAsync();

            await Task.Delay(Timeout.Infinite);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private Task Ready()
        {
            Console.WriteLine("Bot launched");
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(SocketMessage socketMessage)
        {
            if (socketMessage.Author.Id == _client.CurrentUser.Id)
                return;

            if (socketMessage.Content == ".ping")
                await socketMessage.Channel.SendMessageAsync("pong!");

            var messageGuild = socketMessage.Channel as SocketGuildChannel;

            if (socketMessage.Content == ".rolecolors")
            {
                await socketMessage.Channel.SendMessageAsync("Adding the roles...");
                await messageGuild.Guild.CreateRoleAsync("Blue", messageGuild.Guild.EveryoneRole.Permissions, new Color(0x0000ff), false, null);
                await messageGuild.Guild.CreateRoleAsync("Deepskyblue", messageGuild.Guild.EveryoneRole.Permissions, new Color(0x5dbcd2), false, null);
                await messageGuild.Guild.CreateRoleAsync("Light cyan", messageGuild.Guild.EveryoneRole.Permissions, new Color(0x00ffff), false, null);
                await messageGuild.Guild.CreateRoleAsync("Aquamarine", messageGuild.Guild.EveryoneRole.Permissions, new Color(0x7fffd4), false, null);
                await messageGuild.Guild.CreateRoleAsync("Green", messageGuild.Guild.EveryoneRole.Permissions, new Color(0x008000), false, null);
                await messageGuild.Guild.CreateRoleAsync("Lime", messageGuild.Guild.EveryoneRole.Permissions, new Color(0x00ff00), false, null);
                await messageGuild.Guild.CreateRoleAsync("Spring green", messageGuild.Guild.EveryoneRole.Permissions, new Color(0x00ff7f), false, null);
                await messageGuild.Guild.CreateRoleAsync("Indigo", messageGuild.Guild.EveryoneRole.Permissions, new Color(0x4b0082), false, null);
                await messageGuild.Guild.CreateRoleAsync("Purple", messageGuild.Guild.EveryoneRole.Permissions, new Color(0x800080), false, null);
                await messageGuild.Guild.CreateRoleAsync("Blue violet", messageGuild.Guild.EveryoneRole.Permissions, new Color(0x8a2be2), false, null);
                await messageGuild.Guild.CreateRoleAsync("Amethyst", messageGuild.Guild.EveryoneRole.Permissions, new Color(0x9966cc), false, null);
                await messageGuild.Guild.CreateRoleAsync("Crimson", messageGuild.Guild.EveryoneRole.Permissions, new Color(0xda004e), false, null);
                await messageGuild.Guild.CreateRoleAsync("Deeppink", messageGuild.Guild.EveryoneRole.Permissions, new Color(0xff1493), false, null);
                await messageGuild.Guild.CreateRoleAsync("Medium violet red", messageGuild.Guild.EveryoneRole.Permissions, new Color(0xc71585), false, null);
                await messageGuild.Guild.CreateRoleAsync("Magenta", messageGuild.Guild.EveryoneRole.Permissions, new Color(0xff00ff), false, null);
                await messageGuild.Guild.CreateRoleAsync("Hot pink", messageGuild.Guild.EveryoneRole.Permissions, new Color(0xf660ab), false, null);
                await messageGuild.Guild.CreateRoleAsync("Violet", messageGuild.Guild.EveryoneRole.Permissions, new Color(0xee82ee), false, null);
                await messageGuild.Guild.CreateRoleAsync("Plum", messageGuild.Guild.EveryoneRole.Permissions, new Color(0xdda0dd), false, null);
                await messageGuild.Guild.CreateRoleAsync("Lavender", messageGuild.Guild.EveryoneRole.Permissions, new Color(0xc1a8e7), false, null);
                await messageGuild.Guild.CreateRoleAsync("Thistle", messageGuild.Guild.EveryoneRole.Permissions, new Color(0xd8bfd8), false, null);
                await messageGuild.Guild.CreateRoleAsync("Orange", messageGuild.Guild.EveryoneRole.Permissions, new Color(0xff6f00), false, null);
                await messageGuild.Guild.CreateRoleAsync("Mustard", messageGuild.Guild.EveryoneRole.Permissions, new Color(0xffa500), false, null);
                await messageGuild.Guild.CreateRoleAsync("Yellow", messageGuild.Guild.EveryoneRole.Permissions, new Color(0xffff00), false, null);
                await messageGuild.Guild.CreateRoleAsync("Goldenrod", messageGuild.Guild.EveryoneRole.Permissions, new Color(0xedda74), false, null);
                await messageGuild.Guild.CreateRoleAsync("Light yellow", messageGuild.Guild.EveryoneRole.Permissions, new Color(0xffff9f), false, null);
                await messageGuild.Guild.CreateRoleAsync("Dark red", messageGuild.Guild.EveryoneRole.Permissions, new Color(0x8b0000), false, null);
                await messageGuild.Guild.CreateRoleAsync("Red", messageGuild.Guild.EveryoneRole.Permissions, new Color(0xff0000), false, null);
                await messageGuild.Guild.CreateRoleAsync("Fire brick", messageGuild.Guild.EveryoneRole.Permissions, new Color(0xb22222), false, null);
                await messageGuild.Guild.CreateRoleAsync("Black", messageGuild.Guild.EveryoneRole.Permissions, new Color(0x030202), false, null);
                await messageGuild.Guild.CreateRoleAsync("Dark grey", messageGuild.Guild.EveryoneRole.Permissions, new Color(0x333333), false, null);
                await messageGuild.Guild.CreateRoleAsync("Grey", messageGuild.Guild.EveryoneRole.Permissions, new Color(0x666565), false, null);
                await messageGuild.Guild.CreateRoleAsync("White", messageGuild.Guild.EveryoneRole.Permissions, new Color(0xffffff), false, null);
                await socketMessage.Channel.SendMessageAsync("Color roles are added!");
            }

            if (socketMessage.Content == ".findtext" && socketMessage.Attachments.Count > 0)
            {
                foreach (Attachment attachment in socketMessage.Attachments)
                {
                    if (!attachment.Filename.EndsWith("jpeg") && !attachment.Filename.EndsWith("jpg") && !attachment.Filename.EndsWith("png"))
                        return;

                    using (WebClient webClient = new WebClient())
                    {
                        webClient.DownloadFile(new Uri(attachment.Url), $"{attachment.Filename}");
                    }

                    Console.WriteLine(@"./tessdata");
                    List<TextPoint> textPoints = OcrWrapper.GetTextPointsFromFile($"{attachment.Filename}");
                    var foundWords = textPoints.Select(ooga => ooga.Text).ToArray();
                    await socketMessage.Channel.SendMessageAsync($"Found **{textPoints.Count}** results in file **{attachment.Filename}** {Environment.NewLine}{string.Join(", ",foundWords)}");

                    File.Delete($"{attachment.Filename}");
                }
            }

            if (socketMessage.Content == ".help")
            {
                await socketMessage.Channel.SendMessageAsync("Bot prefix: `.`\n>>> **ping**\n`Pong!`\n\n**rolecolors**\n`This will automatically create 32 color roles for you of any color`\n\n**findtext**\n`An AI that can read words in images`");
            }
               



        }

    }
}
