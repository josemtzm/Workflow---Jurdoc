using Elsa.Persistence.EntityFrameworkCore.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jurdoc.Dashboard.DbContext
{
    public class SqliteContextFactory : IDesignTimeDbContextFactory<SqliteContext>
    {
        public SqliteContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqliteContext>();
            var migrationAssembly = typeof(SqliteContext).Assembly.FullName;
            var connectionString = Environment.GetEnvironmentVariable("EF_CONNECTIONSTRING") ?? @"Data Source=C:\Users\sygno.jmartinez\source\repos\Jurdoc\Jurdoc.Dashboard\data\elsa-dashboard.db;Cache=Shared";

            optionsBuilder.UseSqlite(
                connectionString,
                x => x.MigrationsAssembly(migrationAssembly)
            );

            return new SqliteContext(optionsBuilder.Options);
        }
    }
}
