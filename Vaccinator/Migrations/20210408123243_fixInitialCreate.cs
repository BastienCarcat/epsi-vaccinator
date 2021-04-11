using Microsoft.EntityFrameworkCore.Migrations;

namespace Vaccinator.Migrations
{
    public partial class fixInitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Injections_Personne_PersonneId",
                table: "Injections");

            migrationBuilder.DropForeignKey(
                name: "FK_Injections_Vaccin_VaccinId",
                table: "Injections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vaccin",
                table: "Vaccin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Personne",
                table: "Personne");

            migrationBuilder.RenameTable(
                name: "Vaccin",
                newName: "Vaccins");

            migrationBuilder.RenameTable(
                name: "Personne",
                newName: "Personnes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vaccins",
                table: "Vaccins",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personnes",
                table: "Personnes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Injections_Personnes_PersonneId",
                table: "Injections",
                column: "PersonneId",
                principalTable: "Personnes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Injections_Vaccins_VaccinId",
                table: "Injections",
                column: "VaccinId",
                principalTable: "Vaccins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Injections_Personnes_PersonneId",
                table: "Injections");

            migrationBuilder.DropForeignKey(
                name: "FK_Injections_Vaccins_VaccinId",
                table: "Injections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vaccins",
                table: "Vaccins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Personnes",
                table: "Personnes");

            migrationBuilder.RenameTable(
                name: "Vaccins",
                newName: "Vaccin");

            migrationBuilder.RenameTable(
                name: "Personnes",
                newName: "Personne");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vaccin",
                table: "Vaccin",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personne",
                table: "Personne",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Injections_Personne_PersonneId",
                table: "Injections",
                column: "PersonneId",
                principalTable: "Personne",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Injections_Vaccin_VaccinId",
                table: "Injections",
                column: "VaccinId",
                principalTable: "Vaccin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
