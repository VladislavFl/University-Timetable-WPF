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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.UseSqlite("Data Source=../../../Database/UniversityTimetable.db");
#else
            optionsBuilder.UseSqlite("Data Source=Database/UniversityTimetable.db");
#endif
        }
    }
}