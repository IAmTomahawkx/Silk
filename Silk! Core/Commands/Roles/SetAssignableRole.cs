﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using SilkBot.Exceptions;
using SilkBot.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkBot.Commands.Roles
{
    public class SetAssignableRole : BaseCommandModule
    {
        [Command("Assign")]
        [Aliases("sar", "selfassignablerole", "selfrole")]
        [HelpDescription("Allows you to set self assignable roles. Role menu coming soon:tm:. All Self-Assignable Roles are opt-*in*.")]
        public async Task SetSelfAssignableRole(CommandContext ctx, params DiscordRole[] roles)
        {
            var guild = new SilkDbContext().Guilds.AsQueryable().First(g => g.DiscordGuildId == ctx.Guild.Id);
            if (roles.Count() < 1)
            {
                await ctx.RespondAsync("Roles cannot be empty!");
                return;
            }
            if (!guild.DiscordUserInfos.FirstOrDefault(u => u.UserId == ctx.User.Id).Flags.HasFlag(Models.UserFlag.Staff))
            {
                throw new InsufficientPermissionsException();
            }

            var addedList = new List<string>();
            var removedList = new List<string>();
            var ebStringBuilder = new StringBuilder("Added Roles: ");
            foreach (var role in roles)
            {
                if (!guild.SelfAssignableRoles.Any(r => r.RoleId == role.Id))
                {
                    guild.SelfAssignableRoles.Add(new Models.SelfAssignableRole { RoleId = role.Id });
                    addedList.Add(role.Mention);
                }

                else
                {
                    guild.SelfAssignableRoles.Remove(guild.SelfAssignableRoles.First(r => r.RoleId == role.Id));
                    removedList.Add(role.Mention);
                }
            }

            if (addedList.Any())
            {
                foreach (var addedRole in addedList)
                {
                    ebStringBuilder.Append(addedRole);
                }
            }
            else
            {
                ebStringBuilder.Append("none");
            }

            ebStringBuilder.AppendLine();
            ebStringBuilder.AppendLine("Removed Roles: " + (removedList.Any() ? "" : "none"));

            foreach (var removedRole in removedList)
            {
                ebStringBuilder.Append(removedRole);
            }

            await ctx.RespondAsync(embed: new DiscordEmbedBuilder()
                .WithAuthor(ctx.Member.DisplayName, iconUrl: ctx.Member.AvatarUrl)
                .WithDescription(ebStringBuilder.ToString())
                .WithFooter("Silk", ctx.Client.CurrentUser.AvatarUrl)
                .WithTimestamp(DateTime.Now));


        }
    }
}
