﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repres.Infrastructure.Contexts;

namespace Repres.Infrastructure.Data.Migrations
{
    [DbContext(typeof(BlazorHeroContext))]
    [Migration("20220701162232_ProfileLanguageMigration")]
    partial class ProfileLanguageMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", "Identity");
                });

            modelBuilder.Entity("Repres.Application.Models.Chat.ChatHistory<Repres.Infrastructure.Models.Identity.BlazorHeroUser>", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FromUserId")
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<string>("ToUserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("ChatHistory");
                });

            modelBuilder.Entity("Repres.Domain.Entities.ExtendedAttributes.DocumentExtendedAttribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal?>("Decimal")
                        .HasColumnType("numeric(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("EntityId")
                        .HasColumnType("integer");

                    b.Property<string>("ExternalId")
                        .HasColumnType("text");

                    b.Property<string>("Group")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Json")
                        .HasColumnType("text");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<byte>("Type")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.ToTable("DocumentExtendedAttributes");
                });

            modelBuilder.Entity("Repres.Domain.Entities.Misc.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("DocumentTypeId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<string>("URL")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DocumentTypeId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Repres.Domain.Entities.Misc.DocumentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DocumentTypes");
                });

            modelBuilder.Entity("Repres.Domain.Entities.ThirdParty.Api", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClientId")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Scopes")
                        .HasColumnType("text");

                    b.Property<string>("Secret")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Apis");
                });

            modelBuilder.Entity("Repres.Domain.Entities.ThirdParty.ApiByUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("AccessExpiryDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("AccessToken")
                        .HasColumnType("text");

                    b.Property<int?>("ApiId")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("RefreshExpiryDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ApiId");

                    b.ToTable("ApiByUsers");
                });

            modelBuilder.Entity("Repres.Domain.Entities.ThirdParty.Oura.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<float>("average_met")
                        .HasColumnType("real");

                    b.Property<int?>("cal_active")
                        .HasColumnType("integer");

                    b.Property<int?>("cal_total")
                        .HasColumnType("integer");

                    b.Property<string>("class_5min")
                        .HasColumnType("text");

                    b.Property<int?>("daily_movement")
                        .HasColumnType("integer");

                    b.Property<DateTime>("day_end")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("day_start")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("exported_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("high")
                        .HasColumnType("integer");

                    b.Property<int?>("inactive")
                        .HasColumnType("integer");

                    b.Property<int?>("inactivity_alerts")
                        .HasColumnType("integer");

                    b.Property<int?>("low")
                        .HasColumnType("integer");

                    b.Property<int?>("medium")
                        .HasColumnType("integer");

                    b.Property<int?>("met_min_high")
                        .HasColumnType("integer");

                    b.Property<int?>("met_min_inactive")
                        .HasColumnType("integer");

                    b.Property<int?>("met_min_low")
                        .HasColumnType("integer");

                    b.Property<int?>("met_min_medium")
                        .HasColumnType("integer");

                    b.Property<int?>("met_min_medium_plus")
                        .HasColumnType("integer");

                    b.Property<int?>("non_wear")
                        .HasColumnType("integer");

                    b.Property<int?>("rest")
                        .HasColumnType("integer");

                    b.Property<int?>("rest_mode_state")
                        .HasColumnType("integer");

                    b.Property<int?>("score")
                        .HasColumnType("integer");

                    b.Property<int?>("score_meet_daily_targets")
                        .HasColumnType("integer");

                    b.Property<int?>("score_move_every_hour")
                        .HasColumnType("integer");

                    b.Property<int?>("score_recovery_time")
                        .HasColumnType("integer");

                    b.Property<int?>("score_stay_active")
                        .HasColumnType("integer");

                    b.Property<int?>("score_training_frequency")
                        .HasColumnType("integer");

                    b.Property<int?>("score_training_volume")
                        .HasColumnType("integer");

                    b.Property<int?>("steps")
                        .HasColumnType("integer");

                    b.Property<DateTime>("summary_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("timezone")
                        .HasColumnType("integer");

                    b.Property<string>("user_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ActivitySummary");
                });

            modelBuilder.Entity("Repres.Domain.Entities.ThirdParty.Oura.Readiness", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("exported_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("period_id")
                        .HasColumnType("integer");

                    b.Property<int?>("rest_mode_state")
                        .HasColumnType("integer");

                    b.Property<int?>("score")
                        .HasColumnType("integer");

                    b.Property<int?>("score_activity_balance")
                        .HasColumnType("integer");

                    b.Property<int?>("score_hrv_balance")
                        .HasColumnType("integer");

                    b.Property<int?>("score_previous_day")
                        .HasColumnType("integer");

                    b.Property<int?>("score_previous_night")
                        .HasColumnType("integer");

                    b.Property<int?>("score_recovery_index")
                        .HasColumnType("integer");

                    b.Property<int?>("score_resting_hr")
                        .HasColumnType("integer");

                    b.Property<int?>("score_sleep_balance")
                        .HasColumnType("integer");

                    b.Property<int?>("score_temperature")
                        .HasColumnType("integer");

                    b.Property<DateTime>("summary_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("user_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ReadinessSummary");
                });

            modelBuilder.Entity("Repres.Domain.Entities.ThirdParty.Oura.Sleep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("awake")
                        .HasColumnType("integer");

                    b.Property<DateTime>("bedtime_end")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("bedtime_start")
                        .HasColumnType("timestamp without time zone");

                    b.Property<float>("breath_average")
                        .HasColumnType("real");

                    b.Property<int?>("deep")
                        .HasColumnType("integer");

                    b.Property<int?>("duration")
                        .HasColumnType("integer");

                    b.Property<int?>("efficiency")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("exported_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<float>("hr_average")
                        .HasColumnType("real");

                    b.Property<float>("hr_lowest")
                        .HasColumnType("real");

                    b.Property<string>("hypnogram_5min")
                        .HasColumnType("text");

                    b.Property<int?>("is_longest")
                        .HasColumnType("integer");

                    b.Property<int?>("light")
                        .HasColumnType("integer");

                    b.Property<int?>("midpoint_time")
                        .HasColumnType("integer");

                    b.Property<int?>("onset_latency")
                        .HasColumnType("integer");

                    b.Property<int?>("period_id")
                        .HasColumnType("integer");

                    b.Property<int?>("rem")
                        .HasColumnType("integer");

                    b.Property<int?>("restless")
                        .HasColumnType("integer");

                    b.Property<int?>("rmssd")
                        .HasColumnType("integer");

                    b.Property<int?>("score")
                        .HasColumnType("integer");

                    b.Property<int?>("score_alignment")
                        .HasColumnType("integer");

                    b.Property<int?>("score_deep")
                        .HasColumnType("integer");

                    b.Property<int?>("score_disturbances")
                        .HasColumnType("integer");

                    b.Property<int?>("score_efficiency")
                        .HasColumnType("integer");

                    b.Property<int?>("score_latency")
                        .HasColumnType("integer");

                    b.Property<int?>("score_rem")
                        .HasColumnType("integer");

                    b.Property<int?>("score_total")
                        .HasColumnType("integer");

                    b.Property<DateTime>("summary_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<float>("temperature_delta")
                        .HasColumnType("real");

                    b.Property<int?>("timezone")
                        .HasColumnType("integer");

                    b.Property<int?>("total")
                        .HasColumnType("integer");

                    b.Property<string>("user_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SleepSummary");
                });

            modelBuilder.Entity("Repres.Infrastructure.Models.Audit.Audit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AffectedColumns")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("NewValues")
                        .HasColumnType("text");

                    b.Property<string>("OldValues")
                        .HasColumnType("text");

                    b.Property<string>("PrimaryKey")
                        .HasColumnType("text");

                    b.Property<string>("TableName")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AuditTrails");
                });

            modelBuilder.Entity("Repres.Infrastructure.Models.Identity.BlazorHeroRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Roles", "Identity");
                });

            modelBuilder.Entity("Repres.Infrastructure.Models.Identity.BlazorHeroRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Group")
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", "Identity");
                });

            modelBuilder.Entity("Repres.Infrastructure.Models.Identity.BlazorHeroUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("GmailAccount")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Language")
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("OuraSheetId")
                        .HasColumnType("text");

                    b.Property<string>("OuraSheetUrl")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("ProfilePictureDataUrl")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<string>("TimeZoneId")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Users", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Repres.Infrastructure.Models.Identity.BlazorHeroUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Repres.Infrastructure.Models.Identity.BlazorHeroUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Repres.Infrastructure.Models.Identity.BlazorHeroRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repres.Infrastructure.Models.Identity.BlazorHeroUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Repres.Infrastructure.Models.Identity.BlazorHeroUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Repres.Application.Models.Chat.ChatHistory<Repres.Infrastructure.Models.Identity.BlazorHeroUser>", b =>
                {
                    b.HasOne("Repres.Infrastructure.Models.Identity.BlazorHeroUser", "FromUser")
                        .WithMany("ChatHistoryFromUsers")
                        .HasForeignKey("FromUserId");

                    b.HasOne("Repres.Infrastructure.Models.Identity.BlazorHeroUser", "ToUser")
                        .WithMany("ChatHistoryToUsers")
                        .HasForeignKey("ToUserId");

                    b.Navigation("FromUser");

                    b.Navigation("ToUser");
                });

            modelBuilder.Entity("Repres.Domain.Entities.ExtendedAttributes.DocumentExtendedAttribute", b =>
                {
                    b.HasOne("Repres.Domain.Entities.Misc.Document", "Entity")
                        .WithMany("ExtendedAttributes")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("Repres.Domain.Entities.Misc.Document", b =>
                {
                    b.HasOne("Repres.Domain.Entities.Misc.DocumentType", "DocumentType")
                        .WithMany()
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentType");
                });

            modelBuilder.Entity("Repres.Domain.Entities.ThirdParty.ApiByUser", b =>
                {
                    b.HasOne("Repres.Domain.Entities.ThirdParty.Api", "Api")
                        .WithMany()
                        .HasForeignKey("ApiId");

                    b.Navigation("Api");
                });

            modelBuilder.Entity("Repres.Infrastructure.Models.Identity.BlazorHeroRoleClaim", b =>
                {
                    b.HasOne("Repres.Infrastructure.Models.Identity.BlazorHeroRole", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Repres.Domain.Entities.Misc.Document", b =>
                {
                    b.Navigation("ExtendedAttributes");
                });

            modelBuilder.Entity("Repres.Infrastructure.Models.Identity.BlazorHeroRole", b =>
                {
                    b.Navigation("RoleClaims");
                });

            modelBuilder.Entity("Repres.Infrastructure.Models.Identity.BlazorHeroUser", b =>
                {
                    b.Navigation("ChatHistoryFromUsers");

                    b.Navigation("ChatHistoryToUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
