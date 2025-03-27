﻿// <auto-generated />
using System;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bouvet.Developer.Survey.Infrastructure.Migrations
{
    [DbContext(typeof(DeveloperSurveyContext))]
    [Migration("20241025121714_UsersAndLinkToResponses")]
    partial class UsersAndLinkToResponses
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Results.Response", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AnswerOptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChoiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnswerOptionId");

                    b.HasIndex("ChoiceId");

                    b.ToTable("Responses", (string)null);
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Results.ResponseUser", b =>
                {
                    b.Property<Guid>("ResponseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("ResponseId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ResponseUser", (string)null);
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Results.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("RespondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SurveyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.AnswerOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("IndexId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SurveyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("AnswerOptions", (string)null);
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.BlockElement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("QuestionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SurveyElementGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("SurveyElementGuid");

                    b.ToTable("BlockElements", (string)null);
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.Choice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("IndexId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Choices", (string)null);
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BlockElementId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("DataExportTag")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsMultipleChoice")
                        .HasColumnType("bit");

                    b.Property<string>("QuestionDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SurveyId")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("BlockElementId");

                    b.ToTable("Questions", (string)null);
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.Survey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("LastSyncedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("SurveyId")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("SurveyLanguage")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Surveys", (string)null);
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.SurveyBlock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SurveyBlockId")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<Guid>("SurveyGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("SurveyGuid");

                    b.ToTable("SurveyBlocks", (string)null);
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Results.Response", b =>
                {
                    b.HasOne("Bouvet.Developer.Survey.Domain.Entities.Survey.AnswerOption", "AnswerOption")
                        .WithMany("Responses")
                        .HasForeignKey("AnswerOptionId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Bouvet.Developer.Survey.Domain.Entities.Survey.Choice", "Choice")
                        .WithMany("Responses")
                        .HasForeignKey("ChoiceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AnswerOption");

                    b.Navigation("Choice");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Results.ResponseUser", b =>
                {
                    b.HasOne("Bouvet.Developer.Survey.Domain.Entities.Results.Response", "Response")
                        .WithMany("ResponseUsers")
                        .HasForeignKey("ResponseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Bouvet.Developer.Survey.Domain.Entities.Results.User", "User")
                        .WithMany("ResponseUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Response");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Results.User", b =>
                {
                    b.HasOne("Bouvet.Developer.Survey.Domain.Entities.Survey.Survey", "Survey")
                        .WithMany("Users")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.AnswerOption", b =>
                {
                    b.HasOne("Bouvet.Developer.Survey.Domain.Entities.Survey.Survey", "Survey")
                        .WithMany("AnswerOptions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.BlockElement", b =>
                {
                    b.HasOne("Bouvet.Developer.Survey.Domain.Entities.Survey.SurveyBlock", "SurveyBlock")
                        .WithMany("BlockElements")
                        .HasForeignKey("SurveyElementGuid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("SurveyBlock");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.Choice", b =>
                {
                    b.HasOne("Bouvet.Developer.Survey.Domain.Entities.Survey.Question", "Question")
                        .WithMany("Choices")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.Question", b =>
                {
                    b.HasOne("Bouvet.Developer.Survey.Domain.Entities.Survey.BlockElement", "BlockElement")
                        .WithMany("Questions")
                        .HasForeignKey("BlockElementId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BlockElement");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.SurveyBlock", b =>
                {
                    b.HasOne("Bouvet.Developer.Survey.Domain.Entities.Survey.Survey", "Survey")
                        .WithMany("SurveyBlocks")
                        .HasForeignKey("SurveyGuid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Results.Response", b =>
                {
                    b.Navigation("ResponseUsers");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Results.User", b =>
                {
                    b.Navigation("ResponseUsers");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.AnswerOption", b =>
                {
                    b.Navigation("Responses");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.BlockElement", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.Choice", b =>
                {
                    b.Navigation("Responses");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.Question", b =>
                {
                    b.Navigation("Choices");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.Survey", b =>
                {
                    b.Navigation("AnswerOptions");

                    b.Navigation("SurveyBlocks");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.SurveyBlock", b =>
                {
                    b.Navigation("BlockElements");
                });
#pragma warning restore 612, 618
        }
    }
}
