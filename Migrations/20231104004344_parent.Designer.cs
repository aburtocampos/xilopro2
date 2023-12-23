﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using xilopro2.Data;

#nullable disable

namespace xilopro2.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231104004344_parent")]
    partial class parent
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GroupsTorneo", b =>
                {
                    b.Property<int>("GroupsGroup_ID")
                        .HasColumnType("int");

                    b.Property<int>("TorneosTorneo_ID")
                        .HasColumnType("int");

                    b.HasKey("GroupsGroup_ID", "TorneosTorneo_ID");

                    b.HasIndex("TorneosTorneo_ID");

                    b.ToTable("GroupsTorneo");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Category", b =>
                {
                    b.Property<int>("Category_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Category_ID"));

                    b.Property<string>("Category_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Category_Status")
                        .HasColumnType("bit");

                    b.HasKey("Category_ID");

                    b.HasIndex("Category_Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.City", b =>
                {
                    b.Property<int>("City_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("City_ID"));

                    b.Property<string>("City_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("State_ID")
                        .HasColumnType("int");

                    b.HasKey("City_ID");

                    b.HasIndex("State_ID");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.CorrectionAction", b =>
                {
                    b.Property<int>("CorrectionAction_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CorrectionAction_ID"));

                    b.Property<string>("CorrectionAction_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("CorrectionAction_Status")
                        .HasColumnType("bit");

                    b.HasKey("CorrectionAction_ID");

                    b.ToTable("CorrectionActions");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Country", b =>
                {
                    b.Property<int>("Country_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Country_ID"));

                    b.Property<string>("Country_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Country_ID");

                    b.HasIndex("Country_Name")
                        .IsUnique();

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.GroupDetail", b =>
                {
                    b.Property<int>("GroupDetail_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupDetail_ID"));

                    b.Property<int>("GoalsAgainst")
                        .HasColumnType("int");

                    b.Property<int>("GoalsFor")
                        .HasColumnType("int");

                    b.Property<int>("GroupsGroup_ID")
                        .HasColumnType("int");

                    b.Property<int>("MatchesLost")
                        .HasColumnType("int");

                    b.Property<int>("MatchesPlayed")
                        .HasColumnType("int");

                    b.Property<int>("MatchesTied")
                        .HasColumnType("int");

                    b.Property<int>("MatchesWon")
                        .HasColumnType("int");

                    b.Property<int>("Team_ID")
                        .HasColumnType("int");

                    b.HasKey("GroupDetail_ID");

                    b.HasIndex("GroupsGroup_ID");

                    b.HasIndex("Team_ID");

                    b.ToTable("GroupDetails");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Groups", b =>
                {
                    b.Property<int>("Group_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Group_ID"));

                    b.Property<string>("Group_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Group_Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Group_ID");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Lineup", b =>
                {
                    b.Property<int>("Lineup_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Lineup_ID"));

                    b.Property<int>("EntraPor")
                        .HasColumnType("int");

                    b.Property<bool>("Lineup_IsTitular")
                        .HasColumnType("bit");

                    b.Property<int>("MinEntra")
                        .HasColumnType("int");

                    b.Property<int>("MinSale")
                        .HasColumnType("int");

                    b.Property<string>("Player_ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Salepor")
                        .HasColumnType("int");

                    b.HasKey("Lineup_ID");

                    b.HasIndex("Player_ID");

                    b.ToTable("Lineups");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Match", b =>
                {
                    b.Property<int>("Match_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Match_ID"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("GoalsLocal")
                        .HasColumnType("int");

                    b.Property<int>("GoalsVisitor")
                        .HasColumnType("int");

                    b.Property<int>("GroupsGroup_ID")
                        .HasColumnType("int");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("bit");

                    b.Property<string>("Player_ID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Team_ID")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Match_ID");

                    b.HasIndex("GroupsGroup_ID");

                    b.HasIndex("Player_ID");

                    b.HasIndex("Team_ID");

                    b.HasIndex("UserId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Parent", b =>
                {
                    b.Property<string>("Parent_ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("City_ID")
                        .HasColumnType("int");

                    b.Property<int?>("Country_ID")
                        .HasColumnType("int");

                    b.Property<string>("Parent_Cedula")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Parent_FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Parent_Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Parent_LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("State_ID")
                        .HasColumnType("int");

                    b.HasKey("Parent_ID");

                    b.HasIndex("City_ID");

                    b.HasIndex("Country_ID");

                    b.HasIndex("State_ID");

                    b.ToTable("Parents");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Player", b =>
                {
                    b.Property<string>("Player_ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Category_ID")
                        .HasColumnType("int");

                    b.Property<int?>("City_ID")
                        .HasColumnType("int");

                    b.Property<int?>("CorrectionAction_ID")
                        .HasColumnType("int");

                    b.Property<int?>("Country_ID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Player_Dorsal")
                        .HasColumnType("int");

                    b.Property<string>("Player_FNC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Player_FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Player_Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Player_LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Player_Status")
                        .HasColumnType("bit");

                    b.Property<int>("Position_ID")
                        .HasColumnType("int");

                    b.Property<int?>("State_ID")
                        .HasColumnType("int");

                    b.Property<int>("Team_ID")
                        .HasColumnType("int");

                    b.Property<string>("User_Cedula")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.HasKey("Player_ID");

                    b.HasIndex("Category_ID");

                    b.HasIndex("City_ID");

                    b.HasIndex("CorrectionAction_ID");

                    b.HasIndex("Country_ID");

                    b.HasIndex("Position_ID");

                    b.HasIndex("State_ID");

                    b.HasIndex("Team_ID");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Position", b =>
                {
                    b.Property<int>("Position_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Position_ID"));

                    b.Property<string>("Position_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Position_Status")
                        .HasColumnType("bit");

                    b.HasKey("Position_ID");

                    b.HasIndex("Position_Name")
                        .IsUnique();

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.State", b =>
                {
                    b.Property<int>("State_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("State_ID"));

                    b.Property<int>("Country_ID")
                        .HasColumnType("int");

                    b.Property<string>("State_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("State_ID");

                    b.HasIndex("Country_ID");

                    b.ToTable("States");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Team", b =>
                {
                    b.Property<int>("Team_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Team_ID"));

                    b.Property<string>("Team_Estadio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Team_Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Team_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Team_ID");

                    b.HasIndex("Team_Name")
                        .IsUnique();

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Torneo", b =>
                {
                    b.Property<int>("Torneo_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Torneo_ID"));

                    b.Property<DateTime>("Torneo_EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Torneo_Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Torneo_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Torneo_Season")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Torneo_StartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Torneo_Status")
                        .HasColumnType("bit");

                    b.HasKey("Torneo_ID");

                    b.ToTable("Torneos");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("Category_ID")
                        .HasColumnType("int");

                    b.Property<int>("Cityid")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Countryid")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stateid")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("UserTypeofRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User_Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("User_Cedula")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<DateTime?>("User_CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("User_FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("User_Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User_LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("User_Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Category_ID");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("GroupsTorneo", b =>
                {
                    b.HasOne("xilopro2.Data.Entities.Groups", null)
                        .WithMany()
                        .HasForeignKey("GroupsGroup_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("xilopro2.Data.Entities.Torneo", null)
                        .WithMany()
                        .HasForeignKey("TorneosTorneo_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("xilopro2.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("xilopro2.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("xilopro2.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("xilopro2.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("xilopro2.Data.Entities.City", b =>
                {
                    b.HasOne("xilopro2.Data.Entities.State", "State")
                        .WithMany("Cities")
                        .HasForeignKey("State_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.GroupDetail", b =>
                {
                    b.HasOne("xilopro2.Data.Entities.Groups", "Groups")
                        .WithMany("GroupDetails")
                        .HasForeignKey("GroupsGroup_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("xilopro2.Data.Entities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("Team_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Groups");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Lineup", b =>
                {
                    b.HasOne("xilopro2.Data.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("Player_ID");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Match", b =>
                {
                    b.HasOne("xilopro2.Data.Entities.Groups", "Groups")
                        .WithMany("Matches")
                        .HasForeignKey("GroupsGroup_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("xilopro2.Data.Entities.Player", "Player")
                        .WithMany("Matchs")
                        .HasForeignKey("Player_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("xilopro2.Data.Entities.Team", null)
                        .WithMany("Matches")
                        .HasForeignKey("Team_ID");

                    b.HasOne("xilopro2.Data.Entities.User", "User")
                        .WithMany("Matchs")
                        .HasForeignKey("UserId");

                    b.Navigation("Groups");

                    b.Navigation("Player");

                    b.Navigation("User");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Parent", b =>
                {
                    b.HasOne("xilopro2.Data.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("City_ID");

                    b.HasOne("xilopro2.Data.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("Country_ID");

                    b.HasOne("xilopro2.Data.Entities.State", "State")
                        .WithMany()
                        .HasForeignKey("State_ID");

                    b.Navigation("City");

                    b.Navigation("Country");

                    b.Navigation("State");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Player", b =>
                {
                    b.HasOne("xilopro2.Data.Entities.Category", "Category")
                        .WithMany("Player")
                        .HasForeignKey("Category_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("xilopro2.Data.Entities.City", "City")
                        .WithMany("Players")
                        .HasForeignKey("City_ID");

                    b.HasOne("xilopro2.Data.Entities.CorrectionAction", null)
                        .WithMany("Player")
                        .HasForeignKey("CorrectionAction_ID");

                    b.HasOne("xilopro2.Data.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("Country_ID");

                    b.HasOne("xilopro2.Data.Entities.Position", "Position")
                        .WithMany("Players")
                        .HasForeignKey("Position_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("xilopro2.Data.Entities.State", "State")
                        .WithMany("Players")
                        .HasForeignKey("State_ID");

                    b.HasOne("xilopro2.Data.Entities.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("Team_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("City");

                    b.Navigation("Country");

                    b.Navigation("Position");

                    b.Navigation("State");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.State", b =>
                {
                    b.HasOne("xilopro2.Data.Entities.Country", "Country")
                        .WithMany("States")
                        .HasForeignKey("Country_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.User", b =>
                {
                    b.HasOne("xilopro2.Data.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("Category_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Category", b =>
                {
                    b.Navigation("Player");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.City", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.CorrectionAction", b =>
                {
                    b.Navigation("Player");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Country", b =>
                {
                    b.Navigation("States");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Groups", b =>
                {
                    b.Navigation("GroupDetails");

                    b.Navigation("Matches");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Player", b =>
                {
                    b.Navigation("Matchs");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Position", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.State", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.Team", b =>
                {
                    b.Navigation("Matches");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("xilopro2.Data.Entities.User", b =>
                {
                    b.Navigation("Matchs");
                });
#pragma warning restore 612, 618
        }
    }
}
