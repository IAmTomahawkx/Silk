﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SilkBot;

namespace SilkBot.Migrations
{
    [DbContext(typeof(SilkDbContext))]
    [Migration("20200905213841_QuickFix")]
    partial class QuickFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0-preview.8.20407.4");

            modelBuilder.Entity("SilkBot.Database.Models.TicketMessageHistoryModel", b =>
                {
                    b.Property<decimal>("Sender")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<int?>("TicketModelId")
                        .HasColumnType("integer");

                    b.HasKey("Sender");

                    b.HasIndex("TicketModelId");

                    b.ToTable("TicketMessageHistoryModel");
                });

            modelBuilder.Entity("SilkBot.Database.Models.TicketModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("Closed")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Opened")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("Opener")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("SilkBot.Database.Models.TicketResponderModel", b =>
                {
                    b.Property<decimal>("ResponderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("TicketId")
                        .HasColumnType("integer");

                    b.HasKey("ResponderId");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketResponderModel");
                });

            modelBuilder.Entity("SilkBot.Models.Ban", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("GuildId")
                        .HasColumnType("text");

                    b.Property<int?>("GuildId1")
                        .HasColumnType("integer");

                    b.Property<string>("Reason")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<int>("UserInfoId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GuildId1");

                    b.HasIndex("UserInfoId");

                    b.ToTable("Ban");
                });

            modelBuilder.Entity("SilkBot.Models.DiscordUserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("Cash")
                        .HasColumnType("integer");

                    b.Property<int>("Flags")
                        .HasColumnType("integer");

                    b.Property<int?>("GuildId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastCashIn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("UserId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SilkBot.Models.Guild", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<decimal>("DiscordGuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal?>("GeneralLoggingChannel")
                        .HasColumnType("numeric(20,0)");

                    b.Property<bool>("LogMemberJoinOrLeave")
                        .HasColumnType("boolean");

                    b.Property<bool>("LogMessageChanges")
                        .HasColumnType("boolean");

                    b.Property<bool>("LogRoleChange")
                        .HasColumnType("boolean");

                    b.Property<decimal?>("MemberLeaveJoinChannel")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal?>("MessageEditChannel")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal?>("MuteRoleID")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<decimal?>("RoleChangeLogChannel")
                        .HasColumnType("numeric(20,0)");

                    b.Property<bool>("WhiteListInvites")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Guilds");
                });

            modelBuilder.Entity("SilkBot.Models.SelfAssignableRole", b =>
                {
                    b.Property<decimal>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)");

                    b.Property<int?>("GuildId")
                        .HasColumnType("integer");

                    b.HasKey("RoleId");

                    b.HasIndex("GuildId");

                    b.ToTable("SelfAssignableRole");
                });

            modelBuilder.Entity("SilkBot.Models.WhiteListedLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("GuildId")
                        .HasColumnType("integer");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.ToTable("WhiteListedLink");
                });

            modelBuilder.Entity("SilkBot.Database.Models.TicketMessageHistoryModel", b =>
                {
                    b.HasOne("SilkBot.Database.Models.TicketModel", "TicketModel")
                        .WithMany("History")
                        .HasForeignKey("TicketModelId");
                });

            modelBuilder.Entity("SilkBot.Database.Models.TicketResponderModel", b =>
                {
                    b.HasOne("SilkBot.Database.Models.TicketModel", "Ticket")
                        .WithMany("Responders")
                        .HasForeignKey("TicketId");
                });

            modelBuilder.Entity("SilkBot.Models.Ban", b =>
                {
                    b.HasOne("SilkBot.Models.Guild", "Guild")
                        .WithMany("Bans")
                        .HasForeignKey("GuildId1");

                    b.HasOne("SilkBot.Models.DiscordUserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SilkBot.Models.DiscordUserInfo", b =>
                {
                    b.HasOne("SilkBot.Models.Guild", "Guild")
                        .WithMany("DiscordUserInfos")
                        .HasForeignKey("GuildId");
                });

            modelBuilder.Entity("SilkBot.Models.SelfAssignableRole", b =>
                {
                    b.HasOne("SilkBot.Models.Guild", null)
                        .WithMany("SelfAssignableRoles")
                        .HasForeignKey("GuildId");
                });

            modelBuilder.Entity("SilkBot.Models.WhiteListedLink", b =>
                {
                    b.HasOne("SilkBot.Models.Guild", "Guild")
                        .WithMany("WhiteListedLinks")
                        .HasForeignKey("GuildId");
                });
#pragma warning restore 612, 618
        }
    }
}
