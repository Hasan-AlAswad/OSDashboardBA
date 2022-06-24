﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OSDashboardBA.DB;

#nullable disable

namespace OSDashboardBA.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DashboardLayer", b =>
                {
                    b.Property<int>("DashbordsId")
                        .HasColumnType("int");

                    b.Property<int>("LayersId")
                        .HasColumnType("int");

                    b.HasKey("DashbordsId", "LayersId");

                    b.HasIndex("LayersId");

                    b.ToTable("DashboardLayer");
                });

            modelBuilder.Entity("OSDashboardBA.Models.Dashboard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Dashboards");
                });

            modelBuilder.Entity("OSDashboardBA.Models.Layer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LayerDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LayerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsersId");

                    b.ToTable("Layers");
                });

            modelBuilder.Entity("OSDashboardBA.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DashboardLayer", b =>
                {
                    b.HasOne("OSDashboardBA.Models.Dashboard", null)
                        .WithMany()
                        .HasForeignKey("DashbordsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OSDashboardBA.Models.Layer", null)
                        .WithMany()
                        .HasForeignKey("LayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OSDashboardBA.Models.Dashboard", b =>
                {
                    b.HasOne("OSDashboardBA.Models.User", "Users")
                        .WithMany("Dashboards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("OSDashboardBA.Models.Layer", b =>
                {
                    b.HasOne("OSDashboardBA.Models.User", "Users")
                        .WithMany("Layers")
                        .HasForeignKey("UsersId");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("OSDashboardBA.Models.User", b =>
                {
                    b.Navigation("Dashboards");

                    b.Navigation("Layers");
                });
#pragma warning restore 612, 618
        }
    }
}
