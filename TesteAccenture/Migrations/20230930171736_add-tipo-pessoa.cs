using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteAccenture.Migrations
{
    /// <inheritdoc />
    public partial class addtipopessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Fornecedores",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RG",
                table: "Fornecedores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoPessoa",
                table: "Fornecedores",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "RG",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "TipoPessoa",
                table: "Fornecedores");
        }
    }
}
