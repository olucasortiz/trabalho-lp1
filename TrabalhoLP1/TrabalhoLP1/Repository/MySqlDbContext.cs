using MySql.Data.MySqlClient;
using System.Data;
namespace TrabalhoLP1.Repository
{
    public class MySqlDbContext : IDisposable
    {
        private readonly MySqlConnection _connection;

        public MySqlDbContext()
        {
            if (Environment.GetEnvironmentVariable("STRING_CONEXAO") == null)
                throw new Exception("Variável de ambiente STRING_CONEXAO não encontrada");

            string stringConexao = Environment.GetEnvironmentVariable("STRING_CONEXAO");
            _connection = new MySqlConnection(stringConexao);
        }

        public MySql.Data.MySqlClient.MySqlConnection GetConexao()
        {

            if (_connection.State == ConnectionState.Closed)
                _connection.Open();

            return _connection;

        }
        public void Dispose()
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();

            _connection.Dispose();
        }
    }
}
