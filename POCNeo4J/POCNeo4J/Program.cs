using Neo4j.Driver;

string uri = "neo4j://node-l3lpsgsqwdlj4.brazilsouth.cloudapp.azure.com:7687";
string user = "neo4j";
string password = "Projeto@ec10";

var driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));

await RealizarConsulta(driver);

await driver.DisposeAsync();

static async Task RealizarConsulta(IDriver driver)
{
    using (var session = driver.AsyncSession())
    {
        var result = session.RunAsync("MATCH (n) RETURN count(n)");

        var resultado = await result;
        await foreach (var record in resultado)
        {
            Console.WriteLine(record["count(n)"].As<int>());
        }
    }
}