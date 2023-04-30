using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Xml;
using UniversityTimetable.Models;

namespace UniversityTimetable.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Timetable> Timetables { get; set; } = null!;

        public AppDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=UniversityTimetable;Username=postgres;Password=postgres");
        }
    }
}