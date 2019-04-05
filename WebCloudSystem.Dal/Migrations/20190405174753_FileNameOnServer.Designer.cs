﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebCloudSystem.Dal;

namespace WebCloudSystem.Dal.Migrations
{
    [DbContext(typeof(WebCloudSystemContext))]
    [Migration("20190405174753_FileNameOnServer")]
    partial class FileNameOnServer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebCloudSystem.Dal.Models.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Extension");

                    b.Property<string>("FileName");

                    b.Property<string>("FileNameOnServer");

                    b.Property<long>("FileSize");

                    b.Property<DateTime>("ModificationDate");

                    b.Property<int?>("userId");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("WebCloudSystem.Dal.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("ModificationDate");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebCloudSystem.Dal.Models.File", b =>
                {
                    b.HasOne("WebCloudSystem.Dal.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("userId");
                });
#pragma warning restore 612, 618
        }
    }
}
