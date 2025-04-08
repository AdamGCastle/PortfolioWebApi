﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PortfolioWebApi.Contexts;

#nullable disable

namespace PortfolioWebApi.Migrations
{
    [DbContext(typeof(AcPortFolioDbContext))]
    partial class AcPortFolioDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PortfolioWebApi.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("PortfolioWebApi.Models.MultipleChoiceOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<bool>("Removed")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("MultipleChoiceOptions");
                });

            modelBuilder.Entity("PortfolioWebApi.Models.MultipleChoiceOptionResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MultipleChoiceOptionId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionResponseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MultipleChoiceOptionId");

                    b.HasIndex("QuestionResponseId");

                    b.ToTable("MultipleChoiceOptionResponses");
                });

            modelBuilder.Entity("PortfolioWebApi.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsMultipleChoice")
                        .HasColumnType("bit");

                    b.Property<bool>("MultipleAnswersPermitted")
                        .HasColumnType("bit");

                    b.Property<bool>("Removed")
                        .HasColumnType("bit");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("PortfolioWebApi.Models.QuestionResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("MultipleChoiceOptionId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("SurveyResponseId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("SurveyResponseId");

                    b.ToTable("QuestionResponses");
                });

            modelBuilder.Entity("PortfolioWebApi.Models.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatedByAccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByAccountId");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("PortfolioWebApi.Models.SurveyResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<string>("SurveyTakerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyResponses");
                });

            modelBuilder.Entity("PortfolioWebApi.Models.MultipleChoiceOption", b =>
                {
                    b.HasOne("PortfolioWebApi.Models.Question", "Question")
                        .WithMany("MultipleChoiceOptions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("PortfolioWebApi.Models.MultipleChoiceOptionResponse", b =>
                {
                    b.HasOne("PortfolioWebApi.Models.MultipleChoiceOption", "MultipleChoiceOption")
                        .WithMany("MultipleChoiceOptionResponses")
                        .HasForeignKey("MultipleChoiceOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PortfolioWebApi.Models.QuestionResponse", "QuestionResponse")
                        .WithMany("MultipleChoiceOptionResponses")
                        .HasForeignKey("QuestionResponseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MultipleChoiceOption");

                    b.Navigation("QuestionResponse");
                });

            modelBuilder.Entity("PortfolioWebApi.Models.Question", b =>
                {
                    b.HasOne("PortfolioWebApi.Models.Survey", "Survey")
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("PortfolioWebApi.Models.QuestionResponse", b =>
                {
                    b.HasOne("PortfolioWebApi.Models.Question", "Question")
                        .WithMany("QuestionResponses")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PortfolioWebApi.Models.SurveyResponse", "SurveyResponse")
                        .WithMany("QuestionResponses")
                        .HasForeignKey("SurveyResponseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("SurveyResponse");
                });

            modelBuilder.Entity("PortfolioWebApi.Models.Survey", b =>
                {
                    b.HasOne("PortfolioWebApi.Models.Account", "CreatedByAccount")
                        .WithMany("SurveysCreated")
                        .HasForeignKey("CreatedByAccountId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CreatedByAccount");
                });

            modelBuilder.Entity("PortfolioWebApi.Models.SurveyResponse", b =>
                {
                    b.HasOne("PortfolioWebApi.Models.Survey", "Survey")
                        .WithMany("SurveyResponses")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("PortfolioWebApi.Models.Account", b =>
                {
                    b.Navigation("SurveysCreated");
                });

            modelBuilder.Entity("PortfolioWebApi.Models.MultipleChoiceOption", b =>
                {
                    b.Navigation("MultipleChoiceOptionResponses");
                });

            modelBuilder.Entity("PortfolioWebApi.Models.Question", b =>
                {
                    b.Navigation("MultipleChoiceOptions");

                    b.Navigation("QuestionResponses");
                });

            modelBuilder.Entity("PortfolioWebApi.Models.QuestionResponse", b =>
                {
                    b.Navigation("MultipleChoiceOptionResponses");
                });

            modelBuilder.Entity("PortfolioWebApi.Models.Survey", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("SurveyResponses");
                });

            modelBuilder.Entity("PortfolioWebApi.Models.SurveyResponse", b =>
                {
                    b.Navigation("QuestionResponses");
                });
#pragma warning restore 612, 618
        }
    }
}
