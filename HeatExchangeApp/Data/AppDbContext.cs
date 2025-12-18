using HeatExchangeApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace HeatExchangeApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<SavedScenario> SavedScenarios => Set<SavedScenario>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SavedScenario>().HasIndex(s => s.CreatedAt);
    }
}