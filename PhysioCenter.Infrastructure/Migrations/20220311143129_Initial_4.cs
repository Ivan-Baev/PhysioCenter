using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhysioCenter.Infrastructure.Data.Migrations
{
    public partial class Initial_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Therapists_Categories_CategoryId",
                table: "Therapists");

            migrationBuilder.DropIndex(
                name: "IX_Therapists_CategoryId",
                table: "Therapists");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Therapists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Therapists",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Therapists_CategoryId",
                table: "Therapists",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Therapists_Categories_CategoryId",
                table: "Therapists",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
