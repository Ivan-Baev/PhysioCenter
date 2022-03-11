using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhysioCenter.Infrastructure.Data.Migrations
{
    public partial class Initial_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Therapists_TherapistId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_TherapistId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "TherapistId",
                table: "Clients");

            migrationBuilder.CreateTable(
                name: "ClientTherapist",
                columns: table => new
                {
                    ClientsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TherapistsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTherapist", x => new { x.ClientsId, x.TherapistsId });
                    table.ForeignKey(
                        name: "FK_ClientTherapist_Clients_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientTherapist_Therapists_TherapistsId",
                        column: x => x.TherapistsId,
                        principalTable: "Therapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ClientId",
                table: "Reviews",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientTherapist_TherapistsId",
                table: "ClientTherapist",
                column: "TherapistsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Clients_ClientId",
                table: "Reviews",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Clients_ClientId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "ClientTherapist");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ClientId",
                table: "Reviews");

            migrationBuilder.AddColumn<Guid>(
                name: "TherapistId",
                table: "Clients",
                type: "uniqueidentifier",
                nullable: true);

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
        }
    }
}
