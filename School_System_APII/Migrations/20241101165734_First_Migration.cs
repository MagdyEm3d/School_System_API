using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_System_APII.Migrations
{
    /// <inheritdoc />
    public partial class First_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    InstractorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "instractors",
                columns: table => new
                {
                    InstractorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstractorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instractors", x => x.InstractorId);
                    table.ForeignKey(
                        name: "FK_instractors_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: true),
                    InstractorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId");
                    table.ForeignKey(
                        name: "FK_Students_instractors_InstractorId",
                        column: x => x.InstractorId,
                        principalTable: "instractors",
                        principalColumn: "InstractorId");
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "InstractorId", "Password", "SubjectName" },
                values: new object[] { 1, 1, "Arabic@123", "Arabic" });

            migrationBuilder.InsertData(
                table: "instractors",
                columns: new[] { "InstractorId", "Email", "InstractorName", "Password", "Salary", "SubjectId" },
                values: new object[] { 1, "Saber@gmail.com", "Saber", "Saber@123", 1000, 1 });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "Email", "FullName", "InstractorId", "Password", "SubjectId", "UserName" },
                values: new object[] { 1, "Magdy@gmail.com", "Magdy Emad", 1, "Magdy@123", 1, "Magdy7" });

            migrationBuilder.CreateIndex(
                name: "IX_instractors_SubjectId",
                table: "instractors",
                column: "SubjectId",
                unique: true,
                filter: "[SubjectId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Students_InstractorId",
                table: "Students",
                column: "InstractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SubjectId",
                table: "Students",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "instractors");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
