using Npgsql;

namespace CourseWork.Core.Helpers.DatabaseConnectionHelper
{
    /// <summary>
    /// Interface for database connection helper.
    /// </summary>
    public interface IDatabaseConnectionHelper
    {
        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns></returns>
        NpgsqlConnection CreateConnection();
    }
}
