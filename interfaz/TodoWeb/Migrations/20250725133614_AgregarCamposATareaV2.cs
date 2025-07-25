using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoWeb.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCamposATareaV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Tareas",
                newName: "FechaVencimiento");

            migrationBuilder.AlterColumn<string>(
                name: "NombreUsuario",
                table: "Usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Tareas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Prioridad",
                table: "Tareas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "Prioridad",
                table: "Tareas");

            migrationBuilder.RenameColumn(
                name: "FechaVencimiento",
                table: "Tareas",
                newName: "Fecha");

            migrationBuilder.AlterColumn<string>(
                name: "NombreUsuario",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }
    }
}
