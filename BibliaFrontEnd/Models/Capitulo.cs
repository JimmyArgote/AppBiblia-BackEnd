using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace BibliaFrontEnd.Models
{
    public class Capitulo
    {
        public int      Id          { get; set; }
        public int      Livro_id    { get; set; }
        public int      Versao_id   { get; set; }
        public string   Titulo      { get; set; }

        public Capitulo()
        { }

        public List<Capitulo> ListarCapitulos(int _livro_id, out string _erro)
        {
            _erro = null;

            string sql = string.Format("" +

                "SELECT [id], [livro_id], [versao_id], [titulo] " +

                "FROM [BIBLIA].[dbo].[capitulo] " +

                "WHERE [livro_id] = '{0}' " +

                "ORDER BY [id] ", _livro_id

            );

            Service servico = new Service("Biblia");

            servico.ExecutaComandoSqlNoBancoDeDados(sql, out object retorno);

            List<Capitulo> capitulos = new List<Capitulo>();

            foreach (DataRow linha in (retorno as DataTable).Rows)
            {
                capitulos.Add(new Capitulo()
                {
                    Id                  = Convert.ToInt32(linha["id"]),
                    Livro_id            = Convert.ToInt32(linha["livro_id"]),
                    Versao_id           = Convert.ToInt32(linha["versao_id"]),
                    Titulo              = Convert.ToString(linha["titulo"])
                });
            }

            _erro = retorno.ToString();

            Trace.WriteLine(_erro);

            return capitulos;
        }
    }
}