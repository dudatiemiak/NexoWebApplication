using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexoWebApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NomeDaNovaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TN_CLIENTE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TN_CLIENTE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TN_DESCRICAO_CLIENTE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ClienteId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AreaEstudo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    OcupacaoAtual = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    AnosExperiencia = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Satisfacao = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AdocaoTecnologia = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    InteresseMudar = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TN_DESCRICAO_CLIENTE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TN_DESCRICAO_CLIENTE_TN_CLIENTE_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "TN_CLIENTE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TN_DESCRICAO_CLIENTE_ClienteId",
                table: "TN_DESCRICAO_CLIENTE",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TN_DESCRICAO_CLIENTE");

            migrationBuilder.DropTable(
                name: "TN_CLIENTE");
        }
    }
}
