using Aircon.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Test
{
    public class DataConfigurationTestBase
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly AirconDbContext AirconDbContext;

        protected DataConfigurationTestBase()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<AirconDbContext>()
                   .UseSqlite(_connection)
                   .Options;
            AirconDbContext = new AirconDbContext(options);
            AirconDbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
