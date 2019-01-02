﻿// <auto-generated />
using System;
using GoGo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GoGo.Data.Migrations
{
    [DbContext(typeof(GoDbContext))]
    partial class GoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GoGo.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("GoGo.Models.Comment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ComentatorId");

                    b.Property<string>("Content");

                    b.Property<string>("DestinationId");

                    b.HasKey("Id");

                    b.HasIndex("ComentatorId");

                    b.HasIndex("DestinationId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("GoGo.Models.Course", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Category");

                    b.Property<int>("CountOfHours");

                    b.Property<string>("CreatorId");

                    b.Property<string>("Description");

                    b.Property<int>("DurationOfDays");

                    b.Property<byte[]>("Image");

                    b.Property<int>("MaxCountParticipants");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("GoGo.Models.CoursesUsers", b =>
                {
                    b.Property<string>("CourseId");

                    b.Property<string>("ParticipantId");

                    b.Property<int>("StatusUser");

                    b.HasKey("CourseId", "ParticipantId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("CoursesUsers");
                });

            modelBuilder.Entity("GoGo.Models.Destination", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatorId");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("EndDateToJoin");

                    b.Property<byte[]>("Image");

                    b.Property<int>("Level");

                    b.Property<string>("Naame");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Destinations");
                });

            modelBuilder.Entity("GoGo.Models.DestinationPhoto", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DestinationId");

                    b.Property<byte[]>("Image");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("DestinationId");

                    b.HasIndex("UserId");

                    b.ToTable("DestinationPhoto");
                });

            modelBuilder.Entity("GoGo.Models.DestinationsUsers", b =>
                {
                    b.Property<string>("DestinationId");

                    b.Property<string>("ParticipantId");

                    b.Property<int>("Socialization");

                    b.HasKey("DestinationId", "ParticipantId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("DestinationsUsers");
                });

            modelBuilder.Entity("GoGo.Models.Game", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatorId");

                    b.Property<string>("Description");

                    b.Property<byte[]>("Image");

                    b.Property<int>("MaxPoints");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("GoGo.Models.GameLevelParticipant", b =>
                {
                    b.Property<string>("ParticipantId");

                    b.Property<string>("GameId");

                    b.Property<string>("LevelId");

                    b.Property<byte[]>("CorrespondingImage");

                    b.Property<int>("StatusLevel");

                    b.HasKey("ParticipantId", "GameId", "LevelId");

                    b.HasIndex("GameId");

                    b.HasIndex("LevelId");

                    b.ToTable("LevelsParticipants");
                });

            modelBuilder.Entity("GoGo.Models.GoUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Country");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<byte[]>("Image");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int>("Points");

                    b.Property<string>("PostalCode");

                    b.Property<string>("Province");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Street");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("GoGo.Models.Level", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("GameId");

                    b.Property<byte[]>("Image");

                    b.Property<int>("NumberInGame");

                    b.Property<int>("Points");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("GoGo.Models.PeopleStories", b =>
                {
                    b.Property<string>("StoryId");

                    b.Property<string>("UserId");

                    b.HasKey("StoryId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("PeopleStories");
                });

            modelBuilder.Entity("GoGo.Models.Story", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId");

                    b.Property<string>("Content");

                    b.Property<string>("DestinationId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("DestinationId");

                    b.ToTable("Stories");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("GoGo.Models.Comment", b =>
                {
                    b.HasOne("GoGo.Models.GoUser", "Comentator")
                        .WithMany("Comments")
                        .HasForeignKey("ComentatorId");

                    b.HasOne("GoGo.Models.Destination", "Destination")
                        .WithMany("Comments")
                        .HasForeignKey("DestinationId");
                });

            modelBuilder.Entity("GoGo.Models.Course", b =>
                {
                    b.HasOne("GoGo.Models.GoUser", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");
                });

            modelBuilder.Entity("GoGo.Models.CoursesUsers", b =>
                {
                    b.HasOne("GoGo.Models.Course", "Course")
                        .WithMany("Participants")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GoGo.Models.GoUser", "Participant")
                        .WithMany("Courses")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GoGo.Models.Destination", b =>
                {
                    b.HasOne("GoGo.Models.GoUser", "Creator")
                        .WithMany("CreatedDestinations")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GoGo.Models.DestinationPhoto", b =>
                {
                    b.HasOne("GoGo.Models.Destination", "Destination")
                        .WithMany("Photos")
                        .HasForeignKey("DestinationId");

                    b.HasOne("GoGo.Models.GoUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GoGo.Models.DestinationsUsers", b =>
                {
                    b.HasOne("GoGo.Models.Destination", "Destination")
                        .WithMany("Participants")
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GoGo.Models.GoUser", "Participant")
                        .WithMany("Destinations")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GoGo.Models.Game", b =>
                {
                    b.HasOne("GoGo.Models.GoUser", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");
                });

            modelBuilder.Entity("GoGo.Models.GameLevelParticipant", b =>
                {
                    b.HasOne("GoGo.Models.Game", "Game")
                        .WithMany("LevelsParticipants")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GoGo.Models.Level", "Level")
                        .WithMany("Participants")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GoGo.Models.GoUser", "Participant")
                        .WithMany("Levels")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GoGo.Models.Level", b =>
                {
                    b.HasOne("GoGo.Models.Game", "Game")
                        .WithMany("Levels")
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("GoGo.Models.PeopleStories", b =>
                {
                    b.HasOne("GoGo.Models.Story", "Story")
                        .WithMany("PeopleWhosLikeThis")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GoGo.Models.GoUser", "User")
                        .WithMany("Stories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GoGo.Models.Story", b =>
                {
                    b.HasOne("GoGo.Models.GoUser", "Author")
                        .WithMany("CreatedStories")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GoGo.Models.Destination", "Destination")
                        .WithMany("Stories")
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("GoGo.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GoGo.Models.GoUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GoGo.Models.GoUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("GoGo.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GoGo.Models.GoUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("GoGo.Models.GoUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
