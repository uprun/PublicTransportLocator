using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using PublicTransportLocatorWebApp.Models;

namespace PublicTransportLocatorWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("PublicTransportLocatorWebApp.Models.RoutePoint", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<int?>("NextRoutePointID");

                    b.Property<int>("TransportRouteID");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("PublicTransportLocatorWebApp.Models.TransportLocation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Latitude");

                    b.Property<DateTime>("LocationRecordedTime");

                    b.Property<double>("Longitude");

                    b.Property<int>("TransportRouteID");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("PublicTransportLocatorWebApp.Models.TransportRoute", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RouteName");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("PublicTransportLocatorWebApp.Models.RoutePoint", b =>
                {
                    b.HasOne("PublicTransportLocatorWebApp.Models.RoutePoint")
                        .WithMany()
                        .HasForeignKey("NextRoutePointID");

                    b.HasOne("PublicTransportLocatorWebApp.Models.TransportRoute")
                        .WithMany()
                        .HasForeignKey("TransportRouteID");
                });

            modelBuilder.Entity("PublicTransportLocatorWebApp.Models.TransportLocation", b =>
                {
                    b.HasOne("PublicTransportLocatorWebApp.Models.TransportRoute")
                        .WithMany()
                        .HasForeignKey("TransportRouteID");
                });
        }
    }
}
