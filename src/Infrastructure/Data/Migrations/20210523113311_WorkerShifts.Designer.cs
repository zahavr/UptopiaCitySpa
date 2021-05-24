﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210523113311_WorkerShifts")]
    partial class WorkerShifts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Entities.Appartament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BuildingId")
                        .HasColumnType("int");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CountRooms")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int>("Floor")
                        .HasColumnType("int");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("TypeAppartament")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("Appartaments", "HousingSystem");

                    b.ToView("Appartaments", "HousingSystem");
                });

            modelBuilder.Entity("Core.Entities.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountAppartments")
                        .HasColumnType("int");

                    b.Property<int>("CountFloor")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Buildings", "HousingSystem");

                    b.ToView("Buildings", "HousingSystem");
                });

            modelBuilder.Entity("Core.Entities.Business", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BusinessStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateConfirmation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("MaxCountOfWorker")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Business", "Business");

                    b.ToView("Business", "Business");
                });

            modelBuilder.Entity("Core.Entities.BusinessWorker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("PositionAtWork")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartWork")
                        .HasColumnType("datetime2");

                    b.Property<string>("WorkerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("BusinessWorkers", "Business");

                    b.ToView("BusinessWorkers", "Business");
                });

            modelBuilder.Entity("Core.Entities.RejectedApplications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<DateTime>("ExpiredDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RejectedApplications", "Business");

                    b.ToView("RejectedApplications", "Business");
                });

            modelBuilder.Entity("Core.Entities.Shift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("EarnedMoney")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("EndShift")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartShift")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Shifts", "Work");

                    b.ToView("Shifts", "Work");
                });

            modelBuilder.Entity("Core.Entities.User.Friend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDateUser")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FriendBirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FriendEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FriendFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FriendId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FriendLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FriendStatus")
                        .HasColumnType("int");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", "Friends");

                    b.ToView("Users", "Friends");
                });

            modelBuilder.Entity("Core.Entities.UserAppartament", b =>
                {
                    b.Property<int>("AppartamentId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AppartamentId");

                    b.ToTable("UserAppartament", "HousingSystem");

                    b.ToView("UserAppartament", "HousingSystem");
                });

            modelBuilder.Entity("Core.Entities.Vacancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Vacancies", "Business");

                    b.ToView("Vacancies", "Business");
                });

            modelBuilder.Entity("Core.Entities.VacancyApplications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VacancyId")
                        .HasColumnType("int");

                    b.Property<int>("VacancyStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VacancyId");

                    b.ToTable("VacancyApplications", "Business");

                    b.ToView("VacancyApplications", "Business");
                });

            modelBuilder.Entity("Core.Entities.Violation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CitizenId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateExpired")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<decimal>("Penalty")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PolicemanId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SetDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TypeViolation")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Violations", "Police");

                    b.ToView("Violations", "Police");
                });

            modelBuilder.Entity("Core.Entities.Appartament", b =>
                {
                    b.HasOne("Core.Entities.Building", "Building")
                        .WithMany("Appartaments")
                        .HasForeignKey("BuildingId");

                    b.Navigation("Building");
                });

            modelBuilder.Entity("Core.Entities.BusinessWorker", b =>
                {
                    b.HasOne("Core.Entities.Business", "Business")
                        .WithMany("BusinessWorkers")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("Core.Entities.UserAppartament", b =>
                {
                    b.HasOne("Core.Entities.Appartament", "Appartament")
                        .WithMany("UserAppartaments")
                        .HasForeignKey("AppartamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appartament");
                });

            modelBuilder.Entity("Core.Entities.Vacancy", b =>
                {
                    b.HasOne("Core.Entities.Business", "Business")
                        .WithMany("Vacancies")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("Core.Entities.VacancyApplications", b =>
                {
                    b.HasOne("Core.Entities.Vacancy", "Vacancy")
                        .WithMany("VacancyApplications")
                        .HasForeignKey("VacancyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("Core.Entities.Appartament", b =>
                {
                    b.Navigation("UserAppartaments");
                });

            modelBuilder.Entity("Core.Entities.Building", b =>
                {
                    b.Navigation("Appartaments");
                });

            modelBuilder.Entity("Core.Entities.Business", b =>
                {
                    b.Navigation("BusinessWorkers");

                    b.Navigation("Vacancies");
                });

            modelBuilder.Entity("Core.Entities.Vacancy", b =>
                {
                    b.Navigation("VacancyApplications");
                });
#pragma warning restore 612, 618
        }
    }
}
