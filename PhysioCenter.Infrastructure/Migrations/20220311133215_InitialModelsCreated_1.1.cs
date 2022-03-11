using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhysioCenter.Infrastructure.Data.Migrations
{
    public partial class InitialModelsCreated_11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TherapistId",
                table: "Clients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_TherapistId",
                table: "Notes",
                column: "TherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_TherapistId",
                table: "Clients",
                column: "TherapistId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Therapists_TherapistId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Therapists_TherapistId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_TherapistId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Clients_TherapistId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "TherapistId",
                table: "Clients");
        }
    }
}
