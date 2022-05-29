﻿// <auto-generated />
using System;
using EHealth.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EHealth.DataAccess.Migrations
{
    [DbContext(typeof(EHealthDbContext))]
    partial class EHealthDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AppointmentTimeEntityDoctorEntity", b =>
                {
                    b.Property<int>("AvailableAppointmentTimeId")
                        .HasColumnType("int");

                    b.Property<int>("AvailableDoctorsId")
                        .HasColumnType("int");

                    b.HasKey("AvailableAppointmentTimeId", "AvailableDoctorsId");

                    b.HasIndex("AvailableDoctorsId");

                    b.ToTable("AppointmentTimeEntityDoctorEntity");
                });

            modelBuilder.Entity("DoctorEntityOccupationEntity", b =>
                {
                    b.Property<int>("DoctorsId")
                        .HasColumnType("int");

                    b.Property<int>("OccupationsId")
                        .HasColumnType("int");

                    b.HasKey("DoctorsId", "OccupationsId");

                    b.HasIndex("OccupationsId");

                    b.ToTable("DoctorEntityOccupationEntity");
                });

            modelBuilder.Entity("EHealth.Entity.AppointmentTimeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AvailableTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AppointmentTimes");
                });

            modelBuilder.Entity("EHealth.Entity.DoctorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("EHealth.Entity.HistoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AppointmentTimeEntityId")
                        .HasColumnType("int");

                    b.Property<string>("Diagnosis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("PatientFullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentTimeEntityId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("StatusId");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("EHealth.Entity.OccupationEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Occupations");
                });

            modelBuilder.Entity("EHealth.Entity.StatusEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("AppointmentTimeEntityDoctorEntity", b =>
                {
                    b.HasOne("EHealth.Entity.AppointmentTimeEntity", null)
                        .WithMany()
                        .HasForeignKey("AvailableAppointmentTimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EHealth.Entity.DoctorEntity", null)
                        .WithMany()
                        .HasForeignKey("AvailableDoctorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DoctorEntityOccupationEntity", b =>
                {
                    b.HasOne("EHealth.Entity.DoctorEntity", null)
                        .WithMany()
                        .HasForeignKey("DoctorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EHealth.Entity.OccupationEntity", null)
                        .WithMany()
                        .HasForeignKey("OccupationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EHealth.Entity.HistoryEntity", b =>
                {
                    b.HasOne("EHealth.Entity.AppointmentTimeEntity", "AppointmentDateTime")
                        .WithMany("ScheduledAppointsment")
                        .HasForeignKey("AppointmentTimeEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EHealth.Entity.DoctorEntity", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EHealth.Entity.StatusEntity", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppointmentDateTime");

                    b.Navigation("Doctor");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("EHealth.Entity.AppointmentTimeEntity", b =>
                {
                    b.Navigation("ScheduledAppointsment");
                });
#pragma warning restore 612, 618
        }
    }
}