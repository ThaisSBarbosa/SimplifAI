using SimplifAI.Utils;
using Neo4j.Driver;
using SimplifAI.TO;

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


        public static async Task<List<TermoJuridicoOriginalTO>> RetornaTermosDefinicoes()
        {
            var driver = RetornaDriver();
            var listaTermosDefinicoes = new List<TermoJuridicoOriginalTO>();

            try
            {
                using (var session = driver.AsyncSession())
                {
                    var result = await session.RunAsync("MATCH (t:TermoJuridicoOriginal) RETURN t.id AS Id, t.termo AS Termo, t.definicao AS Definicao");

                    await result.ForEachAsync(record =>
                    {
                        var id = record["Id"].As<int>();
                        var termo = record["Termo"].As<string>();
                        var definicao = record["Definicao"].As<string>();

                        listaTermosDefinicoes.Add(new TermoJuridicoOriginalTO
                        {
                            Id = id,
                            Termo = termo.ToLower(),
                            Definicao = definicao
                        });

                        //Console.WriteLine($"Id: {id}, Termo: {termo}, Definicao: {definicao}");
                    });
                }
            }
            catch
            {
                Console.WriteLine("Erro ao realizar consulta no Neo4j.");
            }
            finally
            {
                await driver.DisposeAsync();
            }

            return listaTermosDefinicoes;
        }
    }
}
