using SimplifAI.TO;
namespace SimplifAI.Services
{
    public static class ProcessamentoTextoService
    {
        public static string AdicionaDefinicoesNeo4j(string textoOriginal, List<TermoJuridicoTO> listaTermosDefinicoes)
        {
            string retorno = textoOriginal;

            foreach (var termoDefinicao in listaTermosDefinicoes)
            {
                int posicaoTermo = retorno.IndexOf(" " + termoDefinicao.Termo + " ");

                if (posicaoTermo > -1)
                {
                    string primeiraParte = retorno.Substring(0, posicaoTermo + termoDefinicao.Termo.Length + 1);
                    string segundaParte = retorno.Substring(posicaoTermo + termoDefinicao.Termo.Length + 1);

                    string textoParaAcrescentar = " (" + termoDefinicao.Simplificacao + ", mais especificamente, " + termoDefinicao.Definicao + ")";

                    if (retorno.IndexOf(textoParaAcrescentar) == -1)  //previne definições duplicadas se houver nós duplicados
                        retorno = primeiraParte + textoParaAcrescentar + segundaParte;
                }
            }
            return retorno;
        }
    }
}
