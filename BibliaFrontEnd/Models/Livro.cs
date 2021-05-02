using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace BibliaFrontEnd.Models
{
    public class Livro
    {
        public int      Id          { get; set; }
        public int      Ordem       { get; set; }
        public string   Nome        { get; set; }
        public string   Sigla       { get; set; }
        public string   Testamento  { get; set; }

        public Livro()
        { }

        public List<Livro> ListarLivros(out string _erro)
        {
            _erro = null;

            string sql = string.Format("" +

                "SELECT [id], [ordem], [nome], [sigla], [testamento] " +

                "FROM [BIBLIA].[dbo].[livro] " +

                "ORDER BY [testamento] ASC , [ordem] ASC "

            );

            Service servico = new Service("Biblia");

            servico.ExecutaComandoSqlNoBancoDeDados(sql, out object retorno);

            List<Livro> livros = new List<Livro>();

            foreach (DataRow linha in (retorno as DataTable).Rows)
            {
                livros.Add(new Livro()
                {
                    Id          = Convert.ToInt32(linha["id"]),
                    Ordem       = Convert.ToInt32(linha["ordem"]),
                    Nome        = Convert.ToString(linha["nome"]),
                    Sigla       = Convert.ToString(linha["sigla"]),
                    Testamento  = Convert.ToString(linha["testamento"])

                });
            }

            _erro = retorno.ToString();

            Trace.WriteLine(_erro);

            return livros;
        }
    }
}