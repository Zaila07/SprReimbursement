﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SprEmployeeReimbursement.DataAccess.SprDbContext;

#nullable disable

namespace SprEmployeeReimbursement.Migrations
{
    [DbContext(typeof(SprReimbursementDbContext))]
    [Migration("20230628133828_AddReimbursementFolderPathColumn")]
    partial class AddReimbursementFolderPathColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SprEmployeeReimbursement.DataAccess.Models.ReimbursementModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal?>("MonthlyTotal")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("ReimbursementFolderPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SprEmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("SprEmployeeId");

                    b.ToTable("ReimbursementModels");
                });

            modelBuilder.Entity("SprEmployeeReimbursement.DataAccess.Models.SprEmployee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("SprEmployees");
                });

            modelBuilder.Entity("SprEmployeeReimbursement.DataAccess.Models.ReimbursementModel", b =>
                {
                    b.HasOne("SprEmployeeReimbursement.DataAccess.Models.SprEmployee", null)
                        .WithMany("Reimbursements")
                        .HasForeignKey("SprEmployeeId");
                });

            modelBuilder.Entity("SprEmployeeReimbursement.DataAccess.Models.SprEmployee", b =>
                {
                    b.Navigation("Reimbursements");
                });
#pragma warning restore 612, 618
        }
    }
}
