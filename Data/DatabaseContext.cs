using Microsoft.EntityFrameworkCore;
using Statistic.Entities;

namespace Statistic.Data
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        DbSet<StatisticEntry> Statistics { get; set; }
    }
}