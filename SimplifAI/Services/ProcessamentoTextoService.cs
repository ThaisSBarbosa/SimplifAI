using SimplifAI.TO;
namespace SimplifAI.Services
{
    public static class ProcessamentoTextoService
    {
        public static string AdicionaDefinicoesNeo4j(string textoOriginal, List<TermoJuridicoOriginalTO> listaTermosDefinicoes)
        {
            string retorno = textoOriginal;

            foreach (var termoDefinicao in listaTermosDefinicoes)
            {
                int posicaoTermo = textoOriginal.IndexOf(" " + termoDefinicao.Termo + " ");

                if (posicaoTermo > -1)
                {
                    string primeiraParte = retorno.Substring(0, posicaoTermo + termoDefinicao.Termo.Length + 1);
                    string segundaParte = retorno.Substring(posicaoTermo + termoDefinicao.Termo.Length + 1);

                    retorno = primeiraParte + " (" + termoDefinicao.Definicao + ")" + segundaParte;
                }
            }
            return retorno;
        }
    }
}
