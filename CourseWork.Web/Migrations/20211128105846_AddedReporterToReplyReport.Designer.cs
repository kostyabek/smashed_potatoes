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
    [Migration("20211128105846_AddedReporterToReplyReport")]
    partial class AddedReporterToReplyReport
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Admin.Ban", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsPermanent")
                        .HasColumnType("boolean")
                        .HasColumnName("is_permanent");

                    b.Property<string>("Reason")
                        .HasColumnType("text")
                        .HasColumnName("reason");

                    b.Property<DateTime?>("Until")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("until");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_bans");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_bans_user_id");

                    b.ToTable("bans");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Boards.PotatoBoard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text")
                        .HasColumnName("display_name");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_updated");

                    b.Property<string>("Name")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_boards");

                    b.HasIndex("DisplayName")
                        .IsUnique()
                        .HasDatabaseName("ix_boards_display_name");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_boards_name");

                    b.ToTable("boards");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Files.ImageModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created");

                    b.Property<string>("FileName")
                        .HasColumnType("text")
                        .HasColumnName("file_name");

                    b.HasKey("Id")
                        .HasName("pk_images");

                    b.ToTable("images");
                });

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

                    b.Property<string>("DisplayName")
                        .HasColumnType("text")
                        .HasColumnName("display_name");

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

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Replies.PotatoReply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created");

                    b.Property<bool>("IsThreadStarter")
                        .HasColumnType("boolean")
                        .HasColumnName("is_thread_starter");

                    b.Property<int?>("PicRelatedId")
                        .HasColumnType("integer")
                        .HasColumnName("pic_related_id");

                    b.Property<int>("ThreadId")
                        .HasColumnType("integer")
                        .HasColumnName("thread_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_replies");

                    b.HasIndex("PicRelatedId")
                        .HasDatabaseName("ix_replies_pic_related_id");

                    b.HasIndex("ThreadId")
                        .HasDatabaseName("ix_replies_thread_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_replies_user_id");

                    b.ToTable("replies");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Replies.ReplyReply", b =>
                {
                    b.Property<int>("PointingReplyId")
                        .HasColumnType("integer")
                        .HasColumnName("pointing_reply_id");

                    b.Property<int>("PointedReplyId")
                        .HasColumnType("integer")
                        .HasColumnName("pointed_reply_id");

                    b.HasKey("PointingReplyId", "PointedReplyId")
                        .HasName("pk_reply_replies");

                    b.HasIndex("PointedReplyId")
                        .HasDatabaseName("ix_reply_replies_pointed_reply_id");

                    b.ToTable("reply_replies");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Replies.ReplyReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Explanation")
                        .HasColumnType("text")
                        .HasColumnName("explanation");

                    b.Property<int>("ReplyId")
                        .HasColumnType("integer")
                        .HasColumnName("reply_id");

                    b.Property<int>("ReportReasonId")
                        .HasColumnType("integer")
                        .HasColumnName("report_reason_id");

                    b.Property<int>("ReporterId")
                        .HasColumnType("integer")
                        .HasColumnName("reporter_id");

                    b.HasKey("Id")
                        .HasName("pk_reply_reports");

                    b.HasIndex("ReplyId")
                        .HasDatabaseName("ix_reply_reports_reply_id");

                    b.HasIndex("ReportReasonId")
                        .HasDatabaseName("ix_reply_reports_report_reason_id");

                    b.HasIndex("ReporterId")
                        .HasDatabaseName("ix_reply_reports_reporter_id");

                    b.ToTable("reply_reports");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Reports.ReportReason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_report_reasons");

                    b.ToTable("report_reasons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Inappropriate"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Bullying"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Threads.PotatoThread", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BoardId")
                        .HasColumnType("integer")
                        .HasColumnName("board_id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("MainPictureId")
                        .HasColumnType("integer")
                        .HasColumnName("main_picture_id");

                    b.Property<string>("Name")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("name");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_threads");

                    b.HasIndex("BoardId")
                        .HasDatabaseName("ix_threads_board_id");

                    b.HasIndex("MainPictureId")
                        .HasDatabaseName("ix_threads_main_picture_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_threads_user_id");

                    b.ToTable("threads");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Admin.Ban", b =>
                {
                    b.HasOne("CourseWork.Core.Database.Entities.Identity.AppUser", "User")
                        .WithMany("Bans")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_bans_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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
                    b.HasOne("CourseWork.Core.Database.Entities.Files.ImageModel", "Avatar")
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

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Replies.PotatoReply", b =>
                {
                    b.HasOne("CourseWork.Core.Database.Entities.Files.ImageModel", "PicRelated")
                        .WithMany()
                        .HasForeignKey("PicRelatedId")
                        .HasConstraintName("fk_replies_images_pic_related_id")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("CourseWork.Core.Database.Entities.Threads.PotatoThread", "Thread")
                        .WithMany("Replies")
                        .HasForeignKey("ThreadId")
                        .HasConstraintName("fk_replies_threads_thread_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CourseWork.Core.Database.Entities.Identity.AppUser", "User")
                        .WithMany("Replies")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_replies_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PicRelated");

                    b.Navigation("Thread");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Replies.ReplyReply", b =>
                {
                    b.HasOne("CourseWork.Core.Database.Entities.Replies.PotatoReply", "PointedReply")
                        .WithMany()
                        .HasForeignKey("PointedReplyId")
                        .HasConstraintName("fk_reply_replies_replies_pointed_reply_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CourseWork.Core.Database.Entities.Replies.PotatoReply", "PointingReply")
                        .WithMany("ReplyReplies")
                        .HasForeignKey("PointingReplyId")
                        .HasConstraintName("fk_reply_replies_replies_pointing_reply_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PointedReply");

                    b.Navigation("PointingReply");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Replies.ReplyReport", b =>
                {
                    b.HasOne("CourseWork.Core.Database.Entities.Replies.PotatoReply", "Reply")
                        .WithMany()
                        .HasForeignKey("ReplyId")
                        .HasConstraintName("fk_reply_reports_replies_reply_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CourseWork.Core.Database.Entities.Reports.ReportReason", "ReportReason")
                        .WithMany()
                        .HasForeignKey("ReportReasonId")
                        .HasConstraintName("fk_reply_reports_report_reasons_report_reason_id")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("CourseWork.Core.Database.Entities.Identity.AppUser", "Reporter")
                        .WithMany()
                        .HasForeignKey("ReporterId")
                        .HasConstraintName("fk_reply_reports_users_reporter_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reply");

                    b.Navigation("Reporter");

                    b.Navigation("ReportReason");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Threads.PotatoThread", b =>
                {
                    b.HasOne("CourseWork.Core.Database.Entities.Boards.PotatoBoard", "Board")
                        .WithMany("Threads")
                        .HasForeignKey("BoardId")
                        .HasConstraintName("fk_threads_boards_board_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CourseWork.Core.Database.Entities.Files.ImageModel", "MainPicture")
                        .WithMany()
                        .HasForeignKey("MainPictureId")
                        .HasConstraintName("fk_threads_images_main_picture_id")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("CourseWork.Core.Database.Entities.Identity.AppUser", "User")
                        .WithMany("Threads")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_threads_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");

                    b.Navigation("MainPicture");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Boards.PotatoBoard", b =>
                {
                    b.Navigation("Threads");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Identity.AppRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Identity.AppUser", b =>
                {
                    b.Navigation("Bans");

                    b.Navigation("Replies");

                    b.Navigation("Threads");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Replies.PotatoReply", b =>
                {
                    b.Navigation("ReplyReplies");
                });

            modelBuilder.Entity("CourseWork.Core.Database.Entities.Threads.PotatoThread", b =>
                {
                    b.Navigation("Replies");
                });
#pragma warning restore 612, 618
        }
    }
}
