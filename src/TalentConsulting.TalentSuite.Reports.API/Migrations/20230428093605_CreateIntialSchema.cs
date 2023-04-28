using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentConsulting.TalentSuite.Reports.API.Migrations
{
    public partial class CreateIntialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Detail = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ContactName = table.Column<string>(type: "text", nullable: false),
                    ContactEmail = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    NextRetryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "projectroles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Technical = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projectroles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ContactNumber = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Reference = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "usergroups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ReceivesReports = table.Column<bool>(type: "boolean", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usergroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userprojectroles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ProjectId = table.Column<string>(type: "text", nullable: false),
                    Recievesreports = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userprojectroles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "recipients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Notificationid = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recipients_notifications_Notificationid",
                        column: x => x.Notificationid,
                        principalTable: "notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clientprojects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ClientId = table.Column<string>(type: "text", nullable: false),
                    ProjectId = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientprojects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_clientprojects_cients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "cients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_clientprojects_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contacts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Firstname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    ReceivesReport = table.Column<bool>(type: "boolean", nullable: false),
                    ProjectId = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contacts_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sows",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    File = table.Column<byte[]>(type: "bytea", nullable: false),
                    IsChangeRequest = table.Column<bool>(type: "boolean", nullable: false),
                    SowStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SowEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProjectId = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sows_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Firstname = table.Column<string>(type: "text", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    UserGroupId = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_usergroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "usergroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PlannedTasks = table.Column<string>(type: "text", nullable: false),
                    CompletedTasks = table.Column<string>(type: "text", nullable: false),
                    Weeknumber = table.Column<int>(type: "integer", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProjectId = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reports_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reports_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "risks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ReportId = table.Column<string>(type: "text", nullable: false),
                    RiskDetails = table.Column<string>(type: "text", nullable: false),
                    RiskMitigation = table.Column<string>(type: "text", nullable: false),
                    RagStatus = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_risks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_risks_reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_clientprojects_ClientId",
                table: "clientprojects",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_clientprojects_ProjectId",
                table: "clientprojects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_contacts_ProjectId",
                table: "contacts",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_recipients_Notificationid",
                table: "recipients",
                column: "Notificationid");

            migrationBuilder.CreateIndex(
                name: "IX_reports_ProjectId",
                table: "reports",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_reports_UserId",
                table: "reports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_risks_ReportId",
                table: "risks",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_sows_ProjectId",
                table: "sows",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_users_UserGroupId",
                table: "users",
                column: "UserGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "clientprojects");

            migrationBuilder.DropTable(
                name: "contacts");

            migrationBuilder.DropTable(
                name: "projectroles");

            migrationBuilder.DropTable(
                name: "recipients");

            migrationBuilder.DropTable(
                name: "risks");

            migrationBuilder.DropTable(
                name: "sows");

            migrationBuilder.DropTable(
                name: "userprojectroles");

            migrationBuilder.DropTable(
                name: "cients");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "usergroups");
        }
    }
}
