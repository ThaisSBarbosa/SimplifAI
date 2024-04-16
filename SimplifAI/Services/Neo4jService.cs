using SimplifAI.Utils;

using Neo4j.Driver;
using System;

namespace SimplifAI.Services
{
    public static class Neo4jService
    {
        private static IDriver RetornaDriver()
        {
            var configuracao = Helper.GetConfiguracoes();
            var endpoint = configuracao["NEO4J_ENDPOINT"];
            var user = configuracao["NEO4J_USER"];
            var password = configuracao["NEO4J_PASSWORD"];



            return GraphDatabase.Driver(endpoint, AuthTokens.Basic(user, password));
        }

      

        public static async Task RealizarConsulta()
        {
            var driver = RetornaDriver();
            using (var session = driver.AsyncSession())
            {
                var result = session.RunAsync("MATCH (n) RETURN count(n)");

                var resultado = await result;
                await foreach (var record in resultado)
                {
                    
                    Console.WriteLine("Saida Neo4j: "+record["count(n)"].As<int>());
                }
            }

            await driver.DisposeAsync();
        }


    }
}
