using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ContosoUniversity.Migrations
{
    public partial class Inheritance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Student_StudentID",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_StudentID",
                table: "Enrollment");

            migrationBuilder.RenameTable(
                name: "Instructor", newName: "Person");

            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollmentDate",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Person",
                nullable: false,
                maxLength: 128,
                defaultValue: "Instructor");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HireDate",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OldId", table: "Person", nullable: true);

            //Copy existing Student data into new Person table.
            migrationBuilder.Sql("INSERT INTO dbo.Person (LastName, FirstName, HireDate, EnrollmentDate, Discriminator, OLDId) SELECT LastName, FirstName, null AS HireDate, EnrollmentDate, 'Student' AS Discriminator, ID as OldId FROM dbo.Student");

            //Fix up existing relationships to match new Primary Keys.
            migrationBuilder.Sql("UPDATE dbo.Enrollment SET StudentID = (SELECT ID FROM dbo.Person WHERE OldId = Enrollment.StudentID AND Discriminator = 'Student')");

            //Remove temporary key
            migrationBuilder.DropColumn(
                name: "OldId",
                table: "Person");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_StudentID",
                table: "Enrollment",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Person_StudentID",
                table: "Enrollment",
                column: "StudentID",
                principalTable: "Person",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseAssignment_Person_InstructorID",
                table: "CourseAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_Person_InstructorID",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Person_StudentID",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficeAssignment_Person_InstructorID",
                table: "OfficeAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "HireDate",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Person");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Student");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EnrollmentDate",
                table: "Student",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    HireDate = table.Column<DateTime>(nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAssignment_Instructor_InstructorID",
                table: "CourseAssignment",
                column: "InstructorID",
                principalTable: "Instructor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Instructor_InstructorID",
                table: "Department",
                column: "InstructorID",
                principalTable: "Instructor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Student_StudentID",
                table: "Enrollment",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficeAssignment_Instructor_InstructorID",
                table: "OfficeAssignment",
                column: "InstructorID",
                principalTable: "Instructor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
