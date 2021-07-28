using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace MsSQL.Data
{
    public class UserData
    {
        private const string ConnectionString = @"Server=.\SQLExpress;Database=mydb;Trusted_Connection=True;MultipleActiveResultSets=true;User ID=root;pwd=root";

        public async Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters, CancellationToken cancellationToken = default)
        {
            using IDbConnection connection = new SqlConnection(ConnectionString);

            var rows = await connection.QueryAsync<T>(new CommandDefinition(sql, parameters, cancellationToken: cancellationToken));

            return rows;
        }

        public async Task<T> LoadSingle<T, U>(string sql, U parameters, CancellationToken cancellationToken = default)
        {
            using IDbConnection connection = new SqlConnection(ConnectionString);

            var row = await connection.QuerySingleAsync<T>(new CommandDefinition(sql, parameters, cancellationToken: cancellationToken));

            return row;
        }
    }
}
