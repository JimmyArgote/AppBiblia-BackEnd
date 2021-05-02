using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BibliaFrontEndCore.Models
{
    public class Versiculo
    {
        public int      Id           { get; set; }
        public int      Capitulo_id  { get; set; }
        public int      Livro_id     { get; set; }
        public int      Versao_id    { get; set; }
        public string   Texto        { get; set; }
        public string   Idioma       { get; set; }
        public int      Numero       { get; set; }
        public string   Formatado    { get; set; }
        public string   Limpo        { get; set; }
        public int      Qtd_aumento  { get; set; }

        public Versiculo()
        { }


        public List<Versiculo> ListarVersiculos(int _livro_id, int _capitulo_id, out string _erro)
        {
            _erro = null;

            string sql = string.Format("" +

                "SELECT [livro_id], [capitulo_id], [numero], [formatado] " +

                "FROM [BIBLIA].[dbo].[versiculo] " +

                "WHERE [livro_id] = '{0}' AND [capitulo_id] = '{1}' " +

                "ORDER BY [numero] ", _livro_id, _capitulo_id

            );

            Service servico = new Service("Biblia");

            servico.ExecutaComandoSqlNoBancoDeDados(sql, out object retorno);

            List<Versiculo> versiculos = new List<Versiculo>();

            foreach (DataRow linha in (retorno as DataTable).Rows)
            {
                versiculos.Add(new Versiculo()
                {
                    Livro_id            = Convert.ToInt32(linha["livro_id"]),
                    Capitulo_id         = Convert.ToInt32(linha["capitulo_id"]),
                    Numero              = Convert.ToInt32(linha["numero"]),
                    Formatado           = Convert.ToString(linha["formatado"])
                });
            }

            _erro = retorno.ToString();

            return versiculos;
        }
    }
}
