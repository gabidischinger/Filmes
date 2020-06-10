﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Movies.Persistency;

namespace Movies.Persistency.Migrations
{
    [DbContext(typeof(MoviesDbContext))]
    [Migration("20200610223350_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5");

            modelBuilder.Entity("Movies.Domain.Movie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Genre")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Movies.Domain.Rating", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("MovieID")
                        .HasColumnType("INTEGER");

                    b.Property<float?>("MovieRating")
                        .HasColumnType("REAL");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("MovieID");

                    b.HasIndex("UserID");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Movies.Domain.Review", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("MovieID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("MovieID");

                    b.HasIndex("UserID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Movies.Domain.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ApiKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ApiSecret")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            ApiKey = "e2216fc2-a605-4994-a25e-982048f97969",
                            ApiSecret = "48279e3a-05e9-42d3-9eb1-211f2c968905",
                            Name = "Admin1",
                            Role = "admin"
                        },
                        new
                        {
                            ID = 2,
                            ApiKey = "ce300cae-bc10-40fb-988a-4b819abef9e2",
                            ApiSecret = "f731267b-c6de-4772-bfda-74ee4848dd8b",
                            Name = "Admin2",
                            Role = "admin"
                        },
                        new
                        {
                            ID = 3,
                            ApiKey = "62058c84-7f6a-4a07-b2c6-b1b0c2f1961a",
                            ApiSecret = "82eaa805-2060-40fe-a0e5-c606733383bc",
                            Name = "User1",
                            Role = "user"
                        },
                        new
                        {
                            ID = 4,
                            ApiKey = "5402738c-04d7-47cd-996d-97f6c392ad74",
                            ApiSecret = "c3ce5efc-34a9-47d8-9ef7-db721381d26f",
                            Name = "User2",
                            Role = "user"
                        });
                });

            modelBuilder.Entity("Movies.Domain.Movie", b =>
                {
                    b.HasOne("Movies.Domain.User", "User")
                        .WithMany("Movies")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Movies.Domain.Rating", b =>
                {
                    b.HasOne("Movies.Domain.Movie", "Movie")
                        .WithMany("Ratings")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Movies.Domain.User", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Movies.Domain.Review", b =>
                {
                    b.HasOne("Movies.Domain.Movie", "Movie")
                        .WithMany("Reviews")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Movies.Domain.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}