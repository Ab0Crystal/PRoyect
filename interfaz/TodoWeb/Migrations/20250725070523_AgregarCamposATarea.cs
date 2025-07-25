using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoWeb.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCamposATarea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "Completada",
                table: "Tareas",
                newName: "Completado");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "Tareas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Tareas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Tareas");

            migrationBuilder.RenameColumn(
                name: "Completado",
                table: "Tareas",
                newName: "Completada");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
