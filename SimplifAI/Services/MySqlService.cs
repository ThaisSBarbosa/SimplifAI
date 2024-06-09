using MySqlConnector;
using SimplifAI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifAI.Services
{
    public static class MySqlService
    {
        public static void Insere(string nome, string senha)
        {
            var query = "INSERT INTO usuarios (nome, senha) VALUES ('" + nome + "', '" + senha + "')";
            ExecutaComando(query);
        }

        public static bool ValidaUsuario(string nome, string senha)
        {
            var query = "SELECT COUNT(*) FROM usuarios WHERE nome = '" + nome + "' AND senha = '" + senha + "'";
            return ExecutaCountMaiorQue1(query);
        }

        public static void ExecutaComando(string query)
        {
            var configuracao = Helper.GetConfiguracoes();
            var connectionString = configuracao["MYSQL_STRCONN"];

            // Cria a conexão
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    // Abre a conexão
                    conn.Open();
                    Console.WriteLine("Conexão aberta com sucesso!");

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Executa a query
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine($"Query executada com sucesso! Linhas afetadas: {rowsAffected}");
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
                finally
                {
                    // Fecha a conexão
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                        Console.WriteLine("Conexão fechada.");
                    }
                }
            }
        }

        public static bool ExecutaCountMaiorQue1(string query)
        {
            var configuracao = Helper.GetConfiguracoes();
            var connectionString = configuracao["MYSQL_STRCONN"];
            bool userExists = false;

            // Cria a conexão
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    // Abre a conexão
                    conn.Open();
                    Console.WriteLine("Conexão aberta com sucesso!");

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Executa a query
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            int count = Convert.ToInt32(result);
                            userExists = count > 0;
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
                finally
                {
                    // Fecha a conexão
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                        Console.WriteLine("Conexão fechada.");
                    }
                }
            }

            return userExists;
        }



    }
}
