﻿// <auto-generated />
using System;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bouvet.Developer.Survey.Infrastructure.Migrations
{
    [DbContext(typeof(DeveloperSurveyContext))]
    partial class DeveloperSurveyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    b.Property<string>("DateExportTag")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

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

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.BlockElement", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.Question", b =>
                {
                    b.Navigation("Choices");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.Survey", b =>
                {
                    b.Navigation("SurveyBlocks");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Survey.SurveyBlock", b =>
                {
                    b.Navigation("BlockElements");
                });
#pragma warning restore 612, 618
        }
    }
}
