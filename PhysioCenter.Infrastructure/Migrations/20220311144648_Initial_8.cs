using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhysioCenter.Infrastructure.Data.Migrations
{
    public partial class Initial_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TherapistClient_Clients_ClientId",
                table: "TherapistClient");

            migrationBuilder.DropForeignKey(
                name: "FK_TherapistClient_Therapists_TherapistId",
                table: "TherapistClient");

            migrationBuilder.DropForeignKey(
                name: "FK_TherapistServices_Services_ServiceId",
                table: "TherapistServices");

            migrationBuilder.DropForeignKey(
                name: "FK_TherapistServices_Therapists_TherapistId",
                table: "TherapistServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TherapistServices",
                table: "TherapistServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TherapistClient",
                table: "TherapistClient");

            migrationBuilder.RenameTable(
                name: "TherapistServices",
                newName: "TherapistsServices");

            migrationBuilder.RenameTable(
                name: "TherapistClient",
                newName: "TherapistsClients");

            migrationBuilder.RenameIndex(
                name: "IX_TherapistServices_TherapistId",
                table: "TherapistsServices",
                newName: "IX_TherapistsServices_TherapistId");

            migrationBuilder.RenameIndex(
                name: "IX_TherapistClient_ClientId",
                table: "TherapistsClients",
                newName: "IX_TherapistsClients_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TherapistsServices",
                table: "TherapistsServices",
                columns: new[] { "ServiceId", "TherapistId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TherapistsClients",
                table: "TherapistsClients",
                columns: new[] { "TherapistId", "ClientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TherapistsClients_Clients_ClientId",
                table: "TherapistsClients",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TherapistsClients_Therapists_TherapistId",
                table: "TherapistsClients",
                column: "TherapistId",
                principalTable: "Therapists",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TherapistsClients_Clients_ClientId",
                table: "TherapistsClients");

            migrationBuilder.DropForeignKey(
                name: "FK_TherapistsClients_Therapists_TherapistId",
                table: "TherapistsClients");

            migrationBuilder.DropForeignKey(
                name: "FK_TherapistsServices_Services_ServiceId",
                table: "TherapistsServices");

            migrationBuilder.DropForeignKey(
                name: "FK_TherapistsServices_Therapists_TherapistId",
                table: "TherapistsServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TherapistsServices",
                table: "TherapistsServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TherapistsClients",
                table: "TherapistsClients");

            migrationBuilder.RenameTable(
                name: "TherapistsServices",
                newName: "TherapistServices");

            migrationBuilder.RenameTable(
                name: "TherapistsClients",
                newName: "TherapistClient");

            migrationBuilder.RenameIndex(
                name: "IX_TherapistsServices_TherapistId",
                table: "TherapistServices",
                newName: "IX_TherapistServices_TherapistId");

            migrationBuilder.RenameIndex(
                name: "IX_TherapistsClients_ClientId",
                table: "TherapistClient",
                newName: "IX_TherapistClient_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TherapistServices",
                table: "TherapistServices",
                columns: new[] { "ServiceId", "TherapistId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TherapistClient",
                table: "TherapistClient",
                columns: new[] { "TherapistId", "ClientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TherapistClient_Clients_ClientId",
                table: "TherapistClient",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TherapistClient_Therapists_TherapistId",
                table: "TherapistClient",
                column: "TherapistId",
                principalTable: "Therapists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TherapistServices_Services_ServiceId",
                table: "TherapistServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TherapistServices_Therapists_TherapistId",
                table: "TherapistServices",
                column: "TherapistId",
                principalTable: "Therapists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
