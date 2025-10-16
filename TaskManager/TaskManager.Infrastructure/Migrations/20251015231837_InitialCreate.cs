using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "DataConclusao", "DataCriacao", "Descricao", "Status", "Titulo" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 10, 15, 22, 25, 31, 0, DateTimeKind.Utc), "Descrição detalhada da tarefa pendente.", 0, "Teste 01 - Tarefa Pendente" },
                    { 2, new DateTime(2025, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 15, 15, 21, 31, 0, DateTimeKind.Utc), "Descrição detalhada da tarefa concluida.", 2, "Teste 02 - Tarefa Concluída" },
                    { 3, null, new DateTime(2025, 10, 15, 18, 36, 31, 0, DateTimeKind.Utc), "Descrição detalhada da tarefa em andamento.", 1, "Teste 03 - Em Andamento" },
                    { 4, null, new DateTime(2025, 10, 15, 12, 25, 31, 0, DateTimeKind.Utc), "Descrição detalhada da tarefa pendente.", 0, "Teste 04 - Prioridade Alta" },
                    { 5, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 15, 9, 25, 31, 0, DateTimeKind.Utc), "Descrição detalhada da tarefa concluida.", 2, "Teste 05 - Concluído Hoje" },
                    { 6, null, new DateTime(2025, 10, 15, 10, 25, 31, 0, DateTimeKind.Utc), "Descrição detalhada da tarefa em andamento.", 1, "Teste 06 - Desenvolvimento" },
                    { 7, null, new DateTime(2025, 10, 15, 11, 25, 31, 0, DateTimeKind.Utc), "Descrição detalhada da tarefa pendente.", 0, "Teste 07 - Revisão de Código" },
                    { 8, null, new DateTime(2025, 10, 15, 11, 34, 31, 0, DateTimeKind.Utc), "Descrição detalhada da tarefa pendente.", 0, "Teste 08 - Tarefa Urgente" },
                    { 9, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 15, 22, 11, 31, 0, DateTimeKind.Utc), "Descrição detalhada da tarefa concluida.", 2, "Teste 09 - Antiga Concluída" },
                    { 10, null, new DateTime(2025, 10, 15, 15, 1, 31, 0, DateTimeKind.Utc), "Descrição detalhada da tarefa em andamento.", 1, "Teste 10 - Documentação" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
