using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnicalAssessmentTask2.Migrations
{
    /// <inheritdoc />
    public partial class AddSurveyResponseTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SurveyResponses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RespondentId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Tenure = table.Column<decimal>(type: "decimal(18,1)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Manager = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LeadershipLevel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WhyPride1 = table.Column<int>(type: "int", nullable: true),
                    WhyPride2 = table.Column<int>(type: "int", nullable: true),
                    WhySense1 = table.Column<int>(type: "int", nullable: true),
                    WhySense2 = table.Column<int>(type: "int", nullable: true),
                    WhySense3 = table.Column<int>(type: "int", nullable: true),
                    WhyMeaningfulness1 = table.Column<int>(type: "int", nullable: true),
                    WhyMeaningfulness2 = table.Column<int>(type: "int", nullable: true),
                    WhoFaithLeadership1 = table.Column<int>(type: "int", nullable: true),
                    WhoFaithLeadership2 = table.Column<int>(type: "int", nullable: true),
                    WhoFaithLeadership3 = table.Column<int>(type: "int", nullable: true),
                    WhoTrust1 = table.Column<int>(type: "int", nullable: true),
                    WhoTrust2 = table.Column<int>(type: "int", nullable: true),
                    WhoTrust3 = table.Column<int>(type: "int", nullable: true),
                    WhoRelationship1 = table.Column<int>(type: "int", nullable: true),
                    WhoRelationship2 = table.Column<int>(type: "int", nullable: true),
                    WhoRelationship3 = table.Column<int>(type: "int", nullable: true),
                    WhoRelationship4 = table.Column<int>(type: "int", nullable: true),
                    WhoRelationship5 = table.Column<int>(type: "int", nullable: true),
                    WhoRelationship6 = table.Column<int>(type: "int", nullable: true),
                    WhoRelationship7 = table.Column<int>(type: "int", nullable: true),
                    WhoSenseBelonging1 = table.Column<int>(type: "int", nullable: true),
                    HowSenseDirection1 = table.Column<int>(type: "int", nullable: true),
                    HowRoleClarity1 = table.Column<int>(type: "int", nullable: true),
                    HowLineSight1 = table.Column<int>(type: "int", nullable: true),
                    HowSystemTools1 = table.Column<int>(type: "int", nullable: true),
                    HowSystemTools2 = table.Column<int>(type: "int", nullable: true),
                    HowSystemTools3 = table.Column<int>(type: "int", nullable: true),
                    HowSystemTools4 = table.Column<int>(type: "int", nullable: true),
                    HowSenseProgress1 = table.Column<int>(type: "int", nullable: true),
                    WhatWorkCulture1 = table.Column<int>(type: "int", nullable: true),
                    WhatWorkCulture2 = table.Column<int>(type: "int", nullable: true),
                    WhatWorkCulture3 = table.Column<int>(type: "int", nullable: true),
                    WhatWorkCulture4 = table.Column<int>(type: "int", nullable: true),
                    WhatWorkCulture5 = table.Column<int>(type: "int", nullable: true),
                    WhatTalentDevelopment1 = table.Column<int>(type: "int", nullable: true),
                    WhatTalentDevelopment2 = table.Column<int>(type: "int", nullable: true),
                    WhatRecognition1 = table.Column<int>(type: "int", nullable: true),
                    WhatCompensationBenefits1 = table.Column<int>(type: "int", nullable: true),
                    WhatWorkLifeBalance1 = table.Column<int>(type: "int", nullable: true),
                    WhatWorkLifeBalance2 = table.Column<int>(type: "int", nullable: true),
                    WhatWorkLifeBalance3 = table.Column<int>(type: "int", nullable: true),
                    WhatPsychologicalSafety1 = table.Column<int>(type: "int", nullable: true),
                    WhatPsychologicalSafety2 = table.Column<int>(type: "int", nullable: true),
                    WhatPsychologicalSafety3 = table.Column<int>(type: "int", nullable: true),
                    WhatPsychologicalSafety4 = table.Column<int>(type: "int", nullable: true),
                    WhatPsychologicalSafety5 = table.Column<int>(type: "int", nullable: true),
                    WhatPsychologicalSafety6 = table.Column<int>(type: "int", nullable: true),
                    WhatPsychologicalSafety7 = table.Column<int>(type: "int", nullable: true),
                    OutcomeAdvocacy1 = table.Column<int>(type: "int", nullable: true),
                    OutcomeDiscretionaryEffort1 = table.Column<int>(type: "int", nullable: true),
                    OutcomeIntentToStay1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyResponses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TextAnalyses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyID = table.Column<int>(type: "int", nullable: false),
                    OpenEndedStart1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpenEndedContinue1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpenEndedStop1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpenEndedAnythingElse1 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextAnalyses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TextAnalyses_SurveyResponses_SurveyID",
                        column: x => x.SurveyID,
                        principalTable: "SurveyResponses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TextAnalyses_SurveyID",
                table: "TextAnalyses",
                column: "SurveyID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextAnalyses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SurveyResponses");
        }
    }
}
