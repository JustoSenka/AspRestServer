﻿// <auto-generated />
using System;
using LanguageLearnerData.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LanguageLearnerData.Migrations
{
    [DbContext(typeof(BookContext))]
    [Migration("20191105205322_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LanguageLearnerData.Objects.Book", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LanguageFromID");

                    b.Property<int>("LanguageToID");

                    b.HasKey("ID");

                    b.HasIndex("LanguageFromID");

                    b.HasIndex("LanguageToID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("LanguageLearnerData.Objects.Definition", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LanguageID");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("LanguageID");

                    b.ToTable("Definition");
                });

            modelBuilder.Entity("LanguageLearnerData.Objects.Language", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("LanguageLearnerData.Objects.Translation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BookID");

                    b.Property<int>("DefinitionID");

                    b.Property<int>("WordID");

                    b.HasKey("ID");

                    b.HasIndex("BookID");

                    b.HasIndex("DefinitionID");

                    b.HasIndex("WordID");

                    b.ToTable("Translation");
                });

            modelBuilder.Entity("LanguageLearnerData.Objects.Word", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LanguageID");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("LanguageID");

                    b.ToTable("Word");
                });

            modelBuilder.Entity("LanguageLearnerData.Objects.Book", b =>
                {
                    b.HasOne("LanguageLearnerData.Objects.Language", "LanguageFrom")
                        .WithMany()
                        .HasForeignKey("LanguageFromID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LanguageLearnerData.Objects.Language", "LanguageTo")
                        .WithMany()
                        .HasForeignKey("LanguageToID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LanguageLearnerData.Objects.Definition", b =>
                {
                    b.HasOne("LanguageLearnerData.Objects.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LanguageLearnerData.Objects.Translation", b =>
                {
                    b.HasOne("LanguageLearnerData.Objects.Book")
                        .WithMany("Words")
                        .HasForeignKey("BookID");

                    b.HasOne("LanguageLearnerData.Objects.Definition", "Definition")
                        .WithMany()
                        .HasForeignKey("DefinitionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LanguageLearnerData.Objects.Word", "Word")
                        .WithMany()
                        .HasForeignKey("WordID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LanguageLearnerData.Objects.Word", b =>
                {
                    b.HasOne("LanguageLearnerData.Objects.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
