﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web.Infrustructure;

#nullable disable

namespace Web.Migrations
{
    [DbContext(typeof(WeatherDbContext))]
    partial class WeatherDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Web.Services.Models.WeatherData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("HumidityValue")
                        .HasColumnType("REAL");

                    b.Property<double>("TemperatureValue")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("TimeEvent")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("WeatherDatas");
                });
#pragma warning restore 612, 618
        }
    }
}
