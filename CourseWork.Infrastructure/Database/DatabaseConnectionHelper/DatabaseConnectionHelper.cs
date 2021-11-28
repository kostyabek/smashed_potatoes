namespace CourseWork.Core.Database.DatabaseConnectionHelper
{
    using Microsoft.Extensions.Configuration;
    using Npgsql;

    /// <summary>
    /// Database connection helper.
    /// </summary>
    /// <seealso cref="IDatabaseConnectionHelper" />
    public class DatabaseConnectionHelper : IDatabaseConnectionHelper
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnectionHelper"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public DatabaseConnectionHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgreSql");
        }

        /// <inheritdoc/>
        public NpgsqlConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
