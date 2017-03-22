using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ZenithWebsite.Models;

namespace ZenithWebsite.Migrations
{
    [DbContext(typeof(ZenithContext))]
    [Migration("20170321040146_8")]
    partial class _8
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ZenithWebsite.Models.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.HasKey("ActivityId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("ZenithWebsite.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActivityId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("EnteredBy");

                    b.Property<DateTime>("EventFrom");

                    b.Property<DateTime>("EventTo");

                    b.Property<bool>("IsActive");

                    b.HasKey("EventId");

                    b.HasIndex("ActivityId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("ZenithWebsite.Models.Event", b =>
                {
                    b.HasOne("ZenithWebsite.Models.Activity")
                        .WithMany("Events")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
