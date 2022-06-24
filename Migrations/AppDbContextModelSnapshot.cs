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

            modelBuilder.Entity("OSDashboardBA.Models.Dashboard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserDId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserDId");

                    b.ToTable("Dashboards");
                });

            modelBuilder.Entity("OSDashboardBA.Models.Layer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DashboardId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LayerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserDId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DashboardId");

                    b.HasIndex("UserDId");

                    b.ToTable("Layers");
                });

            modelBuilder.Entity("OSDashboardBA.Models.TextString", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("DashboardId")
                        .HasColumnType("int");

                    b.Property<int?>("LayerId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DashboardId");

                    b.HasIndex("LayerId");

                    b.ToTable("TextString");
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

            modelBuilder.Entity("OSDashboardBA.Models.Dashboard", b =>
                {
                    b.HasOne("OSDashboardBA.Models.User", "UserD")
                        .WithMany("Dashboards")
                        .HasForeignKey("UserDId");

                    b.Navigation("UserD");
                });

            modelBuilder.Entity("OSDashboardBA.Models.Layer", b =>
                {
                    b.HasOne("OSDashboardBA.Models.Dashboard", null)
                        .WithMany("Layers")
                        .HasForeignKey("DashboardId");

                    b.HasOne("OSDashboardBA.Models.User", "UserD")
                        .WithMany("Layers")
                        .HasForeignKey("UserDId");

                    b.Navigation("UserD");
                });

            modelBuilder.Entity("OSDashboardBA.Models.TextString", b =>
                {
                    b.HasOne("OSDashboardBA.Models.Dashboard", null)
                        .WithMany("Widgets")
                        .HasForeignKey("DashboardId");

                    b.HasOne("OSDashboardBA.Models.Layer", null)
                        .WithMany("GeoJson")
                        .HasForeignKey("LayerId");
                });

            modelBuilder.Entity("OSDashboardBA.Models.Dashboard", b =>
                {
                    b.Navigation("Layers");

                    b.Navigation("Widgets");
                });

            modelBuilder.Entity("OSDashboardBA.Models.Layer", b =>
                {
                    b.Navigation("GeoJson");
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
