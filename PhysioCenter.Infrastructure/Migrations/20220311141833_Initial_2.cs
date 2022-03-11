using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhysioCenter.Infrastructure.Data.Migrations
{
    public partial class Initial_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Services_ServiceId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_TherapistsServices_TherapistServiceServiceId_TherapistServiceTherapistId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Therapists_Categories_CategoryId",
                table: "Therapists");

            migrationBuilder.DropForeignKey(
                name: "FK_TherapistsServices_Services_ServiceId",
                table: "TherapistsServices");

            migrationBuilder.DropForeignKey(
                name: "FK_TherapistsServices_Therapists_TherapistId",
                table: "TherapistsServices");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_TherapistServiceServiceId_TherapistServiceTherapistId",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TherapistsServices",
                table: "TherapistsServices");

            migrationBuilder.DropColumn(
                name: "TherapistServiceServiceId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TherapistServiceTherapistId",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "TherapistsServices",
                newName: "TherapistService");

            migrationBuilder.RenameIndex(
                name: "IX_TherapistsServices_TherapistId",
                table: "TherapistService",
                newName: "IX_TherapistService_TherapistId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Therapists",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "TherapistId",
                table: "Clients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TherapistService",
                table: "TherapistService",
                columns: new[] { "ServiceId", "TherapistId" });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_TherapistId",
                table: "Notes",
                column: "TherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_TherapistId",
                table: "Clients",
                column: "TherapistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Services_ServiceId",
                table: "Appointments",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Therapists_TherapistId",
                table: "Clients",
                column: "TherapistId",
                principalTable: "Therapists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Therapists_TherapistId",
                table: "Notes",
                column: "TherapistId",
                principalTable: "Therapists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Therapists_Categories_CategoryId",
                table: "Therapists",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TherapistService_Services_ServiceId",
                table: "TherapistService",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TherapistService_Therapists_TherapistId",
                table: "TherapistService",
                column: "TherapistId",
                principalTable: "Therapists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Services_ServiceId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Therapists_TherapistId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Therapists_TherapistId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Therapists_Categories_CategoryId",
                table: "Therapists");

            migrationBuilder.DropForeignKey(
                name: "FK_TherapistService_Services_ServiceId",
                table: "TherapistService");

            migrationBuilder.DropForeignKey(
                name: "FK_TherapistService_Therapists_TherapistId",
                table: "TherapistService");

            migrationBuilder.DropIndex(
                name: "IX_Notes_TherapistId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Clients_TherapistId",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TherapistService",
                table: "TherapistService");

            migrationBuilder.DropColumn(
                name: "TherapistId",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "TherapistService",
                newName: "TherapistsServices");

            migrationBuilder.RenameIndex(
                name: "IX_TherapistService_TherapistId",
                table: "TherapistsServices",
                newName: "IX_TherapistsServices_TherapistId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Therapists",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "TherapistServiceServiceId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TherapistServiceTherapistId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TherapistsServices",
                table: "TherapistsServices",
                columns: new[] { "ServiceId", "TherapistId" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TherapistServiceServiceId_TherapistServiceTherapistId",
                table: "Appointments",
                columns: new[] { "TherapistServiceServiceId", "TherapistServiceTherapistId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Services_ServiceId",
                table: "Appointments",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_TherapistsServices_TherapistServiceServiceId_TherapistServiceTherapistId",
                table: "Appointments",
                columns: new[] { "TherapistServiceServiceId", "TherapistServiceTherapistId" },
                principalTable: "TherapistsServices",
                principalColumns: new[] { "ServiceId", "TherapistId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Therapists_Categories_CategoryId",
                table: "Therapists",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TherapistsServices_Services_ServiceId",
                table: "TherapistsServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TherapistsServices_Therapists_TherapistId",
                table: "TherapistsServices",
                column: "TherapistId",
                principalTable: "Therapists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
