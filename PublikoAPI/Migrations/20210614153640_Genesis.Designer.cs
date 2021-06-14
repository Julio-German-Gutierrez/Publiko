﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PublikoAPI.Data;

namespace PublikoAPI.Migrations
{
    [DbContext(typeof(PublikoPagesDBContext))]
    [Migration("20210614153640_Genesis")]
    partial class Genesis
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PublikoAPI.Models.Page", b =>
                {
                    b.Property<string>("PageID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PageContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PageID");

                    b.ToTable("Pages");
                });
#pragma warning restore 612, 618
        }
    }
}