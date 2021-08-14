﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PublikoAPI.Data;

namespace PublikoAPI.Migrations
{
    [DbContext(typeof(PublikoPagesDBContext))]
    [Migration("20210812083224_AddedPostRequired")]
    partial class AddedPostRequired
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PublikoSharedLibrary.Models.WebPage", b =>
                {
                    b.Property<string>("PageID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PageBody")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PageDateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PageDateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("PageOrder")
                        .HasColumnType("int");

                    b.Property<string>("PageTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PageID");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("PublikoSharedLibrary.Models.WebPost", b =>
                {
                    b.Property<string>("PostID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PostContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PostDateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PostDateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostID");

                    b.ToTable("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
