using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _7adarny.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addSubjectNameProperityinTeacherEntitiy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubjectName",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectName",
                table: "Teachers");
        }
    }
}
