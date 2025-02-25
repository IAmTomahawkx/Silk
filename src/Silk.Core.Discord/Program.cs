using System;
using DSharpPlus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using Silk.Core.Discord.Utilities;
using Silk.Core.Discord.Utilities.Bot;

namespace Silk.Core.Discord
{
    public static class Program
    {
        public static DateTime Startup { get; } = DateTime.Now;
        public static string HttpClientName { get; } = "Silk";

        public const string Version = "1.5.1-alpha";
        private const string LogFormat = "[{Timestamp:h:mm:ss ff tt}] [{Level:u3}] [{SourceContext}] {Message:lj} {Exception:j}{NewLine}";


        private static readonly DiscordConfiguration _clientConfig = new()
        {
            Intents = DiscordIntents.Guilds | // Caching
                      DiscordIntents.GuildMembers | // Auto-mod/Auto-greet
                      DiscordIntents.DirectMessages | // DM Commands
                      DiscordIntents.GuildPresences | // Auto-Mod Anti-Status-Invite
                      DiscordIntents.GuildMessages | // Commands & Auto-Mod
                      DiscordIntents.GuildMessageReactions | // Role-menu
                      DiscordIntents.DirectMessageReactions | // Interactivity in DMs
                      DiscordIntents.GuildVoiceStates,
            LogTimestampFormat = "h:mm:ss ff tt",
            MessageCacheSize = 1024,
            MinimumLogLevel = LogLevel.None,
        };

        // Setting this in the prop doesn't work; it'll have a 2s discrepancy
        //static Program() => Startup = DateTime.Now;

        public static void Start(IHostBuilder host)
        {
            _ = Startup;
            Console.WriteLine($"Started! The current time is {DateTime.Now:h:mm:ss ff tt}");

            ConfigureHost(host);
        }

        private static void ConfigureHost(IHostBuilder host)
        {
            host.ConfigureLogging((builder, _) =>
                {
                    if (int.TryParse(builder.Configuration["Shards"] ?? "1", out int shards))
                        _clientConfig.ShardCount = shards;
                })
                .ConfigureServices((context, services) =>
                {
                    IConfiguration config = context.Configuration;
                    _clientConfig.Token = config.GetConnectionString("discord");

                    services!.AddSingleton(new DiscordShardedClient(_clientConfig));

                    Discord.Startup.AddServices(services);

                    services.AddMemoryCache(option => option.ExpirationScanFrequency = TimeSpan.FromSeconds(30));

                    services.AddHttpClient(HttpClientName, client =>
                    {
                        client.DefaultRequestHeaders.UserAgent.ParseAdd("Silk Project by VelvetThePanda / v1.4");
                    });

                    // Sub out the default implementation filter with custom filter
                    services.Replace(ServiceDescriptor.Singleton<IHttpMessageHandlerBuilderFilter, CustomLoggingFilter>());

                    /* Can remove all filters with this line */
                    // services.RemoveAll<IHttpMessageHandlerBuilderFilter>();

                    services.AddSingleton(_ => new BotConfig(config));


                })
                .UseSerilog();
        }
    }
}