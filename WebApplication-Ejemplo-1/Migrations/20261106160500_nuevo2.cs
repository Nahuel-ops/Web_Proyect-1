using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication_Ejemplo_1.Migrations
{
    /// <inheritdoc />
    public partial class nuevo2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Canchas",
                table: "Canchas");

            migrationBuilder.RenameTable(
                name: "Canchas",
                newName: "canchas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_canchas",
                table: "canchas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Telefono = table.Column<int>(type: "int", maxLength: 99999999, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "resevas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCancha = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resevas", x => x.id);
                    table.ForeignKey(
                        name: "FK_resevas_clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_resevas_IdCliente",
                table: "resevas",
                column: "IdCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "resevas");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_canchas",
                table: "canchas");

            migrationBuilder.RenameTable(
                name: "canchas",
                newName: "Canchas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Canchas",
                table: "Canchas",
                column: "Id");
        }
    }
}
