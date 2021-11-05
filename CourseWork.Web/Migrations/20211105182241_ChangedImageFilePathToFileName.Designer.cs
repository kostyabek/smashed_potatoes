﻿// <auto-generated />
using System;
using CourseWork.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CourseWork.Web.Migrations
{
    [DbContext(typeof(BaseDbContext))]
    [Migration("20211105182241_ChangedImageFilePathToFileName")]
    partial class ChangedImageFilePathToFileName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Identity.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("role_name_index");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "2e6e4c4b-43d0-4a0d-9a9c-320c24260476",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            ConcurrencyStamp = "41b26433-bda8-4fd6-954e-e6b947810df2",
                            Name = "Moderator",
                            NormalizedName = "MODERATOR"
                        },
                        new
                        {
                            Id = 3,
                            ConcurrencyStamp = "07c3c173-38c0-444a-a5a6-511507b5e590",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = 4,
                            ConcurrencyStamp = "ab109c0f-d32f-4615-9ae2-b928afdc8088",
                            Name = "New user",
                            NormalizedName = "NEW USER"
                        });
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Identity.AppRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_roles_claims");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_roles_claims_role_id");

                    b.ToTable("roles_claims");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Identity.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("access_failed_count");

                    b.Property<int?>("AvatarId")
                        .HasColumnType("integer")
                        .HasColumnName("avatar_id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("AvatarId")
                        .HasDatabaseName("ix_users_avatar_id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("email_index");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("user_name_index");

                    b.ToTable("users");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Identity.AppUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_users_claims");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_users_claims_user_id");

                    b.ToTable("users_claims");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Identity.AppUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("provider_key");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("provider_display_name");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_users_logins");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_users_logins_user_id");

                    b.ToTable("users_logins");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Identity.AppUserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_users_roles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_users_roles_role_id");

                    b.ToTable("users_roles");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Identity.AppUserToken", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_users_tokens");

                    b.ToTable("users_tokens");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.ImageModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FileName")
                        .HasColumnType("text")
                        .HasColumnName("file_name");

                    b.HasKey("Id")
                        .HasName("pk_images");

                    b.ToTable("images");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Identity.AppRoleClaim", b =>
                {
                    b.HasOne("CourseWork.Core.Database.Entities.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_roles_claims_roles_role_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Identity.AppUser", b =>
                {
                    b.HasOne("CourseWork.Core.Database.Entities.ImageModel", "Avatar")
                        .WithMany()
                        .HasForeignKey("AvatarId")
                        .HasConstraintName("fk_users_images_avatar_id")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Avatar");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Identity.AppUserClaim", b =>
                {
                    b.HasOne("CourseWork.Core.Database.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_users_claims_users_user_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Identity.AppUserLogin", b =>
                {
                    b.HasOne("CourseWork.Core.Database.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_users_logins_users_user_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Identity.AppUserRole", b =>
                {
                    b.HasOne("CourseWork.Core.Database.Entities.Identity.AppRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_users_roles_roles_role_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CourseWork.Core.Database.Entities.Identity.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_users_roles_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Identity.AppUserToken", b =>
                {
                    b.HasOne("CourseWork.Core.Database.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_users_tokens_users_user_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Identity.AppRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Identity.AppUser", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
