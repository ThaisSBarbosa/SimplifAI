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


        public static async Task<List<TermoJuridicoTO>> RetornaTermosDefinicoes()
        {
            var driver = RetornaDriver();
            var listaTermosDefinicoes = new List<TermoJuridicoTO>();

            try
            {
                using (var session = driver.AsyncSession())
                {
                    string query = "MATCH (SimplificacaoTermo: SimplificacaoTermo) - [:PERTENCE_A]->(TermoJuridicoOriginal: TermoJuridicoOriginal) \r\nMATCH (TermoJuridicoOriginal) - [:CATEGORIZADO_POR]->(Categoria: Categoria) \r\nRETURN TermoJuridicoOriginal.idTermoJuridico AS Id, TermoJuridicoOriginal.termo AS Termo, TermoJuridicoOriginal.definicao AS Definicao, SimplificacaoTermo.idSimplificacao AS IdSimplificacao, SimplificacaoTermo.simplificacao AS Simplificacao, \r\nCategoria.id AS IdCategoria, Categoria.categoria AS Categoria, Categoria.descricao AS Descricao";

                    var result = await session.RunAsync(query);

                    while (await result.FetchAsync())
                    {
                        try
                        {
                            var record = result.Current;

                            var idTermoJuridico = record["Id"].As<int?>();
                            var termo = record["Termo"] == null ? string.Empty : record["Termo"].As<string>();
                            var definicao = record["Definicao"] == null ? string.Empty : record["Definicao"].As<string>();

                            var idSimplificacao = record["IdSimplificacao"].As<int?>();
                            var simplificacao = record["Simplificacao"] == null ? string.Empty : record["Simplificacao"].As<string>();

                            var id = record["IdCategoria"].As<int?>();
                            var categoria = record["Categoria"] == null ? string.Empty : record["Categoria"].As<string>();
                            var descricao = record["Descricao"] == null ? string.Empty : record["Descricao"].As<string>();

                            listaTermosDefinicoes.Add(new TermoJuridicoTO
                            {
                                IdTermoJuridico = idTermoJuridico,
                                Termo = termo.ToLower(),
                                Definicao = definicao,
                                IdSimplificacao = idSimplificacao,
                                Simplificacao = simplificacao.ToLower(),
                                Id = id,
                                Categoria = categoria,
                                Descricao = descricao.ToLower()
                            });
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Erro ao montar um objeto com um termo do Neo4j. " + e.Message);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao realizar consulta no Neo4j. " + e.Message);
            }
            finally
            {
                await driver.DisposeAsync();
            }

            return listaTermosDefinicoes;
        }
    }
}
