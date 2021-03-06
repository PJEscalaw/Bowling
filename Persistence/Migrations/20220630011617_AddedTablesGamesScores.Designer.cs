// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Contexts;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(BowlingDbContext))]
    [Migration("20220630011617_AddedTablesGamesScores")]
    partial class AddedTablesGamesScores
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("Domain.Entities.Games", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Domain.Entities.Scores", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Frame")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("GameId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsSpare")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsStrike")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PinsKnockedDown")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Scores");
                });
#pragma warning restore 612, 618
        }
    }
}
