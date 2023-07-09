using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GbsoDev.TechTest.Library.Dal.Migrations
{
    /// <inheritdoc />
    public partial class initianlmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "autores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 7, 9, 15, 53, 26, 312, DateTimeKind.Local).AddTicks(4094))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_autores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Editoriales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Sede = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editoriales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "libros",
                columns: table => new
                {
                    ISBN = table.Column<decimal>(type: "numeric(13,0)", nullable: false),
                    EditorialId = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Sinopsis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NPaginas = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_libros", x => x.ISBN);
                    table.ForeignKey(
                        name: "FK_libros_Editoriales_EditorialId",
                        column: x => x.EditorialId,
                        principalTable: "Editoriales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "autores_has_libros",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    autores_id = table.Column<int>(type: "int", nullable: false),
                    libros_ISBN = table.Column<decimal>(type: "numeric(13,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_autores_has_libros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_autores_has_libros_autores_autores_id",
                        column: x => x.autores_id,
                        principalTable: "autores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_autores_has_libros_libros_libros_ISBN",
                        column: x => x.libros_ISBN,
                        principalTable: "libros",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_autores_has_libros_autores_id_libros_ISBN",
                table: "autores_has_libros",
                columns: new[] { "autores_id", "libros_ISBN" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_autores_has_libros_libros_ISBN",
                table: "autores_has_libros",
                column: "libros_ISBN");

            migrationBuilder.CreateIndex(
                name: "IX_libros_EditorialId",
                table: "libros",
                column: "EditorialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "autores_has_libros");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "autores");

            migrationBuilder.DropTable(
                name: "libros");

            migrationBuilder.DropTable(
                name: "Editoriales");
        }
    }
}
