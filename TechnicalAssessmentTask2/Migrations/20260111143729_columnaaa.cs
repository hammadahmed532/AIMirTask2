using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnicalAssessmentTask2.Migrations
{
    /// <inheritdoc />
    public partial class columnaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SupportingInfo",
                table: "TextAnalyses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupportingInfo",
                table: "TextAnalyses");
        }
    }
}
