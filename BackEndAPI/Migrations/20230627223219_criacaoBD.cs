using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndAPI.Migrations
{
    /// <inheritdoc />
    public partial class criacaoBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClasseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClasseId);
                });

            migrationBuilder.CreateTable(
                name: "ContraIndicadoTags",
                columns: table => new
                {
                    ContraIndicadoTagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContraIndicadoTags", x => x.ContraIndicadoTagId);
                });

            migrationBuilder.CreateTable(
                name: "IndicadoTags",
                columns: table => new
                {
                    IndicadoTagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicadoTags", x => x.IndicadoTagId);
                });

            migrationBuilder.CreateTable(
                name: "Tipos",
                columns: table => new
                {
                    TipoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos", x => x.TipoId);
                });

            migrationBuilder.CreateTable(
                name: "Medicamentos",
                columns: table => new
                {
                    MedicamentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Posologia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClasseId = table.Column<int>(type: "int", nullable: false),
                    TipoId = table.Column<int>(type: "int", nullable: false),
                    Bula = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamentos", x => x.MedicamentoId);
                    table.ForeignKey(
                        name: "FK_Medicamentos_Classes_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classes",
                        principalColumn: "ClasseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medicamentos_Tipos_TipoId",
                        column: x => x.TipoId,
                        principalTable: "Tipos",
                        principalColumn: "TipoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicamentoContraIndicadoTag",
                columns: table => new
                {
                    ContraIndicadoTagsContraIndicadoTagId = table.Column<int>(type: "int", nullable: false),
                    MedicamentosMedicamentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicamentoContraIndicadoTag", x => new { x.ContraIndicadoTagsContraIndicadoTagId, x.MedicamentosMedicamentoId });
                    table.ForeignKey(
                        name: "FK_MedicamentoContraIndicadoTag_ContraIndicadoTags_ContraIndicadoTagsContraIndicadoTagId",
                        column: x => x.ContraIndicadoTagsContraIndicadoTagId,
                        principalTable: "ContraIndicadoTags",
                        principalColumn: "ContraIndicadoTagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicamentoContraIndicadoTag_Medicamentos_MedicamentosMedicamentoId",
                        column: x => x.MedicamentosMedicamentoId,
                        principalTable: "Medicamentos",
                        principalColumn: "MedicamentoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicamentoIndicadoTag",
                columns: table => new
                {
                    IndicadoTagsIndicadoTagId = table.Column<int>(type: "int", nullable: false),
                    MedicamentosMedicamentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicamentoIndicadoTag", x => new { x.IndicadoTagsIndicadoTagId, x.MedicamentosMedicamentoId });
                    table.ForeignKey(
                        name: "FK_MedicamentoIndicadoTag_IndicadoTags_IndicadoTagsIndicadoTagId",
                        column: x => x.IndicadoTagsIndicadoTagId,
                        principalTable: "IndicadoTags",
                        principalColumn: "IndicadoTagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicamentoIndicadoTag_Medicamentos_MedicamentosMedicamentoId",
                        column: x => x.MedicamentosMedicamentoId,
                        principalTable: "Medicamentos",
                        principalColumn: "MedicamentoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicamentoContraIndicadoTag_MedicamentosMedicamentoId",
                table: "MedicamentoContraIndicadoTag",
                column: "MedicamentosMedicamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicamentoIndicadoTag_MedicamentosMedicamentoId",
                table: "MedicamentoIndicadoTag",
                column: "MedicamentosMedicamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicamentos_ClasseId",
                table: "Medicamentos",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicamentos_TipoId",
                table: "Medicamentos",
                column: "TipoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicamentoContraIndicadoTag");

            migrationBuilder.DropTable(
                name: "MedicamentoIndicadoTag");

            migrationBuilder.DropTable(
                name: "ContraIndicadoTags");

            migrationBuilder.DropTable(
                name: "IndicadoTags");

            migrationBuilder.DropTable(
                name: "Medicamentos");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Tipos");
        }
    }
}
