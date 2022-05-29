using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EHealth.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvailableTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentTimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Occupations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occupations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentTimeEntityDoctorEntity",
                columns: table => new
                {
                    AvailableAppointmentTimeId = table.Column<int>(type: "int", nullable: false),
                    AvailableDoctorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentTimeEntityDoctorEntity", x => new { x.AvailableAppointmentTimeId, x.AvailableDoctorsId });
                    table.ForeignKey(
                        name: "FK_AppointmentTimeEntityDoctorEntity_AppointmentTimes_AvailableAppointmentTimeId",
                        column: x => x.AvailableAppointmentTimeId,
                        principalTable: "AppointmentTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentTimeEntityDoctorEntity_Doctors_AvailableDoctorsId",
                        column: x => x.AvailableDoctorsId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorEntityOccupationEntity",
                columns: table => new
                {
                    DoctorsId = table.Column<int>(type: "int", nullable: false),
                    OccupationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorEntityOccupationEntity", x => new { x.DoctorsId, x.OccupationsId });
                    table.ForeignKey(
                        name: "FK_DoctorEntityOccupationEntity_Doctors_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorEntityOccupationEntity_Occupations_OccupationsId",
                        column: x => x.OccupationsId,
                        principalTable: "Occupations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    AppointmentTimeEntityId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Histories_AppointmentTimes_AppointmentTimeEntityId",
                        column: x => x.AppointmentTimeEntityId,
                        principalTable: "AppointmentTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Histories_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Histories_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentTimeEntityDoctorEntity_AvailableDoctorsId",
                table: "AppointmentTimeEntityDoctorEntity",
                column: "AvailableDoctorsId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorEntityOccupationEntity_OccupationsId",
                table: "DoctorEntityOccupationEntity",
                column: "OccupationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_AppointmentTimeEntityId",
                table: "Histories",
                column: "AppointmentTimeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_DoctorId",
                table: "Histories",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_StatusId",
                table: "Histories",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentTimeEntityDoctorEntity");

            migrationBuilder.DropTable(
                name: "DoctorEntityOccupationEntity");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "Occupations");

            migrationBuilder.DropTable(
                name: "AppointmentTimes");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Statuses");
        }
    }
}
