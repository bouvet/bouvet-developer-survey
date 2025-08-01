﻿// <auto-generated />
using System;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bouvet.Developer.Survey.Infrastructure.Migrations.Bouvet
{
    [DbContext(typeof(BouvetSurveyContext))]
    partial class BouvetSurveyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SectionId")
                        .HasColumnType("int");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.HasIndex("SurveyId");

                    b.ToTable("Question", "bouvet");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetResponse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FreeTextAnswer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("HasWorkedWith")
                        .HasColumnType("bit");

                    b.Property<int?>("OptionId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("WantsToWorkWith")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("OptionId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("Response", "bouvet");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetSurvey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExternalId")
                        .IsUnique();

                    b.ToTable("Survey", "bouvet");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetSurveyStructure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("StructureJson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BouvetSurveyStructures");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("RespondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("User", "bouvet");
                });

            modelBuilder.Entity("BouvetOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Option", "bouvet");
                });

            modelBuilder.Entity("BouvetSection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId", "ExternalId")
                        .IsUnique();

                    b.ToTable("Section", "bouvet");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetQuestion", b =>
                {
                    b.HasOne("BouvetSection", "Section")
                        .WithMany()
                        .HasForeignKey("SectionId");

                    b.HasOne("Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetSurvey", "Survey")
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetResponse", b =>
                {
                    b.HasOne("BouvetOption", "Option")
                        .WithMany()
                        .HasForeignKey("OptionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetQuestion", "Question")
                        .WithMany("Responses")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Option");

                    b.Navigation("Question");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetUser", b =>
                {
                    b.HasOne("Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetSurvey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("BouvetOption", b =>
                {
                    b.HasOne("Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetQuestion", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("BouvetSection", b =>
                {
                    b.HasOne("Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetSurvey", "Survey")
                        .WithMany("Sections")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetQuestion", b =>
                {
                    b.Navigation("Options");

                    b.Navigation("Responses");
                });

            modelBuilder.Entity("Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetSurvey", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("Sections");
                });
#pragma warning restore 612, 618
        }
    }
}
