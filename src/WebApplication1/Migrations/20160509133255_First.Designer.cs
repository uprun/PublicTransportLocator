using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using WebApplication1.Models;

namespace WebApplication1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160509133255_First")]
    partial class First
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("WebApplication1.Models.RoutePoint", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<int?>("NextRoutePointID");

                    b.Property<int>("TransportRouteID");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("WebApplication1.Models.TransportLocation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Latitude");

                    b.Property<DateTime>("LocationRecordedTime");

                    b.Property<double>("Longitude");

                    b.Property<int>("TransportRouteID");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("WebApplication1.Models.TransportRoute", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RouteName");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("WebApplication1.Models.RoutePoint", b =>
                {
                    b.HasOne("WebApplication1.Models.RoutePoint")
                        .WithMany()
                        .HasForeignKey("NextRoutePointID");

                    b.HasOne("WebApplication1.Models.TransportRoute")
                        .WithMany()
                        .HasForeignKey("TransportRouteID");
                });

            modelBuilder.Entity("WebApplication1.Models.TransportLocation", b =>
                {
                    b.HasOne("WebApplication1.Models.TransportRoute")
                        .WithMany()
                        .HasForeignKey("TransportRouteID");
                });
        }
    }
}
