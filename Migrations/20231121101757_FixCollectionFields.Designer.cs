﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using finalTaskItra.Data;

#nullable disable

namespace finalTaskItra.Migrations
{
    [DbContext(typeof(EFCoreContext))]
    [Migration("20231121101757_FixCollectionFields")]
    partial class FixCollectionFields
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("finalTaskItra.Models.CollectionFields", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("MyCollectionid")
                        .HasColumnType("int");

                    b.Property<string>("fieldName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fieldType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("MyCollectionid");

                    b.ToTable("collectionFields");
                });

            modelBuilder.Entity("finalTaskItra.Models.Comment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("Itemid")
                        .HasColumnType("int");

                    b.Property<string>("comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("creationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Itemid");

                    b.ToTable("comments");
                });

            modelBuilder.Entity("finalTaskItra.Models.Item", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("MyCollectionid")
                        .HasColumnType("int");

                    b.Property<DateTime>("creationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("MyCollectionid");

                    b.ToTable("items");
                });

            modelBuilder.Entity("finalTaskItra.Models.ItemFields", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("Itemid")
                        .HasColumnType("int");

                    b.Property<string>("boolFieldName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("boolFieldValue")
                        .HasColumnType("bit");

                    b.Property<string>("dateFieldName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("dateFieldValue")
                        .HasColumnType("datetime2");

                    b.Property<string>("doubleFieldName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("doubleFieldValue")
                        .HasColumnType("float");

                    b.Property<string>("stringFieldName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("stringFieldValue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Itemid");

                    b.ToTable("itemFields");
                });

            modelBuilder.Entity("finalTaskItra.Models.MyCollection", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("Userid")
                        .HasColumnType("int");

                    b.Property<DateTime>("creationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("photoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("theme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Userid");

                    b.ToTable("collections");
                });

            modelBuilder.Entity("finalTaskItra.Models.Reaction", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("Itemid")
                        .HasColumnType("int");

                    b.Property<DateTime>("creationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isLike")
                        .HasColumnType("bit");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Itemid");

                    b.ToTable("likes");
                });

            modelBuilder.Entity("finalTaskItra.Models.Tag", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("Itemid")
                        .HasColumnType("int");

                    b.Property<string>("tag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Itemid");

                    b.ToTable("tags");
                });

            modelBuilder.Entity("finalTaskItra.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<bool>("access")
                        .HasColumnType("bit");

                    b.Property<string>("accessToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isOnline")
                        .HasColumnType("bit");

                    b.Property<DateTime>("joinDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("loginDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("role")
                        .HasColumnType("int");

                    b.Property<string>("saltedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("finalTaskItra.Models.CollectionFields", b =>
                {
                    b.HasOne("finalTaskItra.Models.MyCollection", null)
                        .WithMany("collectionFields")
                        .HasForeignKey("MyCollectionid");
                });

            modelBuilder.Entity("finalTaskItra.Models.Comment", b =>
                {
                    b.HasOne("finalTaskItra.Models.Item", null)
                        .WithMany("comments")
                        .HasForeignKey("Itemid");
                });

            modelBuilder.Entity("finalTaskItra.Models.Item", b =>
                {
                    b.HasOne("finalTaskItra.Models.MyCollection", null)
                        .WithMany("items")
                        .HasForeignKey("MyCollectionid");
                });

            modelBuilder.Entity("finalTaskItra.Models.ItemFields", b =>
                {
                    b.HasOne("finalTaskItra.Models.Item", null)
                        .WithMany("fields")
                        .HasForeignKey("Itemid");
                });

            modelBuilder.Entity("finalTaskItra.Models.MyCollection", b =>
                {
                    b.HasOne("finalTaskItra.Models.User", null)
                        .WithMany("collections")
                        .HasForeignKey("Userid");
                });

            modelBuilder.Entity("finalTaskItra.Models.Reaction", b =>
                {
                    b.HasOne("finalTaskItra.Models.Item", null)
                        .WithMany("likes")
                        .HasForeignKey("Itemid");
                });

            modelBuilder.Entity("finalTaskItra.Models.Tag", b =>
                {
                    b.HasOne("finalTaskItra.Models.Item", null)
                        .WithMany("tags")
                        .HasForeignKey("Itemid");
                });

            modelBuilder.Entity("finalTaskItra.Models.Item", b =>
                {
                    b.Navigation("comments");

                    b.Navigation("fields");

                    b.Navigation("likes");

                    b.Navigation("tags");
                });

            modelBuilder.Entity("finalTaskItra.Models.MyCollection", b =>
                {
                    b.Navigation("collectionFields");

                    b.Navigation("items");
                });

            modelBuilder.Entity("finalTaskItra.Models.User", b =>
                {
                    b.Navigation("collections");
                });
#pragma warning restore 612, 618
        }
    }
}
