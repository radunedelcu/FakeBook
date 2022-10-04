﻿// <auto-generated />
using System;
using FakeBook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FakeBook.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221003113300_NullableImage")]
    partial class NullableImage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FakeBook.Domain.Entities.FriendEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("deleted");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_date");

                    b.Property<int?>("User1Id")
                        .HasColumnType("int");

                    b.Property<int?>("User2Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("User1Id");

                    b.HasIndex("User2Id");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("FakeBook.Domain.Entities.ImageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("DataFile")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MessageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("FakeBook.Domain.Entities.MessageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("deleted");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)")
                        .HasColumnName("message");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_date");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userId");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("FakeBook.Domain.Entities.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("FakeBook.Domain.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("username");

                    b.Property<byte[]>("HashPassword")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("hash_password");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("deleted");

                    b.Property<byte[]>("KeyPassword")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("key_password");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("name");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("roleId");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_date");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("FakeBook.Domain.Entities.FriendEntity", b =>
                {
                    b.HasOne("FakeBook.Domain.Entities.UserEntity", "User1")
                        .WithMany()
                        .HasForeignKey("User1Id");

                    b.HasOne("FakeBook.Domain.Entities.UserEntity", "User2")
                        .WithMany()
                        .HasForeignKey("User2Id");

                    b.Navigation("User1");

                    b.Navigation("User2");
                });

            modelBuilder.Entity("FakeBook.Domain.Entities.MessageEntity", b =>
                {
                    b.HasOne("FakeBook.Domain.Entities.ImageEntity", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.HasOne("FakeBook.Domain.Entities.UserEntity", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FakeBook.Domain.Entities.UserEntity", b =>
                {
                    b.HasOne("FakeBook.Domain.Entities.RoleEntity", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("FakeBook.Domain.Entities.UserEntity", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
