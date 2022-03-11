using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhysioCenter.Infrastructure.Data.Migrations
{
    public partial class Initial_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TherapistService_Services_ServiceId",
                table: "TherapistService");

            migrationBuilder.DropForeignKey(
                name: "FK_TherapistService_Therapists_TherapistId",
                table: "TherapistService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TherapistService",
                table: "TherapistService");

            migrationBuilder.RenameTable(
                name: "TherapistService",
                newName: "TherapistServices");

            migrationBuilder.RenameIndex(
                name: "IX_TherapistService_TherapistId",
                table: "TherapistServices",
                newName: "IX_TherapistServices_TherapistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TherapistServices",
                table: "TherapistServices",
                columns: new[] { "ServiceId", "TherapistId" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TherapistServices_Services_ServiceId",
                table: "TherapistServices");

            migrationBuilder.DropForeignKey(
                name: "FK_TherapistServices_Therapists_TherapistId",
                table: "TherapistServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TherapistServices",
                table: "TherapistServices");

            migrationBuilder.RenameTable(
                name: "TherapistServices",
                newName: "TherapistService");

            migrationBuilder.RenameIndex(
                name: "IX_TherapistServices_TherapistId",
                table: "TherapistService",
                newName: "IX_TherapistService_TherapistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TherapistService",
                table: "TherapistService",
                columns: new[] { "ServiceId", "TherapistId" });

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
    }
}
