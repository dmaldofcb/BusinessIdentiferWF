﻿// <auto-generated />
using InclusionDiversityIdentifier.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace InclusionDiversityIdentifier.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("InclusionDiversityIdentifier.Models.Business", b =>
                {
                    b.Property<int>("dunsNumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("city")
                        .HasColumnType("text");

                    b.Property<string>("county")
                        .HasColumnType("text");

                    b.Property<string>("dunsName")
                        .HasColumnType("text");

                    b.Property<string>("executiveContact")
                        .HasColumnType("text");

                    b.Property<string>("executiveContact2")
                        .HasColumnType("text");

                    b.Property<bool>("isWomanOwned")
                        .HasColumnType("boolean");

                    b.Property<string>("minorityOwnedDesc")
                        .HasColumnType("text");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("state")
                        .HasColumnType("text");

                    b.Property<string>("streetAddress")
                        .HasColumnType("text");

                    b.Property<string>("zipCode")
                        .HasColumnType("text");

                    b.HasKey("dunsNumId");

                    b.ToTable("Businesses");
                });
#pragma warning restore 612, 618
        }
    }
}
