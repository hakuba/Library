﻿// <auto-generated />
using Library.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Library.Migrations
{
    [DbContext(typeof(DBIndex))]
    [Migration("20231030123838_Add stock into library and book relation")]
    partial class Addstockintolibraryandbookrelation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Library.Infrastructure.Models.Author", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Library.Infrastructure.Models.Book", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Library.Infrastructure.Models.BookLibrary", b =>
                {
                    b.Property<string>("BookId")
                        .HasColumnType("text");

                    b.Property<string>("LibraryId")
                        .HasColumnType("text");

                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("Stock")
                        .HasColumnType("integer");

                    b.HasKey("BookId", "LibraryId");

                    b.HasIndex("LibraryId");

                    b.ToTable("BookLibraries");
                });

            modelBuilder.Entity("Library.Infrastructure.Models.LibraryDBO", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("LibraryInfoId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Libraries");
                });

            modelBuilder.Entity("Library.Infrastructure.Models.LibraryInfo", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HoursOfOperation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LibraryId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LibraryId")
                        .IsUnique();

                    b.ToTable("LibraryInfos");
                });

            modelBuilder.Entity("Library.Infrastructure.Models.Book", b =>
                {
                    b.HasOne("Library.Infrastructure.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Library.Infrastructure.Models.BookLibrary", b =>
                {
                    b.HasOne("Library.Infrastructure.Models.Book", "Book")
                        .WithMany("BookLibrary")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Infrastructure.Models.LibraryDBO", "Library")
                        .WithMany("BookLibrary")
                        .HasForeignKey("LibraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Library");
                });

            modelBuilder.Entity("Library.Infrastructure.Models.LibraryInfo", b =>
                {
                    b.HasOne("Library.Infrastructure.Models.LibraryDBO", "Library")
                        .WithOne("LibraryInfo")
                        .HasForeignKey("Library.Infrastructure.Models.LibraryInfo", "LibraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Library");
                });

            modelBuilder.Entity("Library.Infrastructure.Models.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Library.Infrastructure.Models.Book", b =>
                {
                    b.Navigation("BookLibrary");
                });

            modelBuilder.Entity("Library.Infrastructure.Models.LibraryDBO", b =>
                {
                    b.Navigation("BookLibrary");

                    b.Navigation("LibraryInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
