﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Repository;

#nullable disable

namespace TalentConsulting.TalentSuite.Reports.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.Audit", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Detail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.Client", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("cients");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.ClientProject", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ProjectId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ProjectId");

                    b.ToTable("clientprojects");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.Contact", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProjectId")
                        .HasColumnType("text");

                    b.Property<bool>("ReceivesReport")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("contacts");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.Notification", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("NextRetryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("notifications");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.Project", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("Created")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("projects");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.ProjectRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Technical")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("projectroles");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.Recipient", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Notificationid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Notificationid");

                    b.ToTable("recipients");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.Report", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CompletedTasks")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PlannedTasks")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProjectId")
                        .HasColumnType("text");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Weeknumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("reports");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.Risk", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RagStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ReportId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RiskDetails")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RiskMitigation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("risks");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.Sow", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<byte[]>("File")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<bool>("IsChangeRequest")
                        .HasColumnType("boolean");

                    b.Property<string>("ProjectId")
                        .HasColumnType("text");

                    b.Property<DateTime>("SowEndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("SowStartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("sows");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserGroupId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserGroupId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.UserGroup", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool?>("ReceivesReports")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("usergroups");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.UserProjectRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Recievesreports")
                        .HasColumnType("boolean");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("userprojectroles");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.ClientProject", b =>
                {
                    b.HasOne("TalentConsulting.TalentSuite.Reports.Core.Entities.Client", null)
                        .WithMany("ClientProjects")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TalentConsulting.TalentSuite.Reports.Core.Entities.Project", null)
                        .WithMany("ClientProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.Contact", b =>
                {
                    b.HasOne("TalentConsulting.TalentSuite.Reports.Core.Entities.Project", null)
                        .WithMany("Contacts")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.Recipient", b =>
                {
                    b.HasOne("TalentConsulting.TalentSuite.Reports.Core.Entities.Notification", null)
                        .WithMany("Recipients")
                        .HasForeignKey("Notificationid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.Report", b =>
                {
                    b.HasOne("TalentConsulting.TalentSuite.Reports.Core.Entities.Project", null)
                        .WithMany("Reports")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TalentConsulting.TalentSuite.Reports.Core.Entities.User", null)
                        .WithMany("Reports")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.Risk", b =>
                {
                    b.HasOne("TalentConsulting.TalentSuite.Reports.Core.Entities.Report", null)
                        .WithMany("Risks")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.Sow", b =>
                {
                    b.HasOne("TalentConsulting.TalentSuite.Reports.Core.Entities.Project", null)
                        .WithMany("Sows")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.User", b =>
                {
                    b.HasOne("TalentConsulting.TalentSuite.Reports.Core.Entities.UserGroup", null)
                        .WithMany("Users")
                        .HasForeignKey("UserGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.Client", b =>
                {
                    b.Navigation("ClientProjects");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.Notification", b =>
                {
                    b.Navigation("Recipients");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.Project", b =>
                {
                    b.Navigation("ClientProjects");

                    b.Navigation("Contacts");

                    b.Navigation("Reports");

                    b.Navigation("Sows");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.Report", b =>
                {
                    b.Navigation("Risks");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.User", b =>
                {
                    b.Navigation("Reports");
                });

            modelBuilder.Entity("TalentConsulting.TalentSuite.Reports.Core.Entities.UserGroup", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
