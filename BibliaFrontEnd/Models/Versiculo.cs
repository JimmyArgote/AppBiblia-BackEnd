using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace BibliaFrontEnd.Models
{
    public class Versiculo
    {
        public int      Id           { get; set; }
        public int      Capitulo_id  { get; set; }
        public int      Livro_id     { get; set; }
        public int      Versao_id    { get; set; }
      //public string   Texto        { get; set; }
      //public string   Idioma       { get; set; }
        public int      Numero       { get; set; }
        public string   Formatado    { get; set; }
      //public string   Limpo        { get; set; }
      //public int      Qtd_aumento  { get; set; }
        public string   Livro_nome   { get; set; }
        public string   Livro_sigla  { get; set; }
        public int      Capitulo     { get; set; }
        public string Livro_testamento { get; set; }
        public int      Qtd_vers       { get; set; }
        public int      Qtd_caps       { get; set; }


        public Versiculo()
        { }

        /*
        public List<Versiculo> ListarVersiculos(int _livro_id, int _capitulo_id, out string _erro)
        {
            _erro = null;

            dynamic busca = new dynamic[2];
            busca[0] = _livro_id;
            busca[1] = _capitulo_id;

            string sql = string.Format("" +

                "SELECT [id], [livro_id], [capitulo_id], [versao_id], [numero], [formatado] " +

                "FROM [BIBLIA].[dbo].[versiculo] " +

                "WHERE [livro_id] = '{0}' AND [capitulo_id] = '{1}' " +

                "ORDER BY [numero] ", busca

            );

            Service servico = new Service("Biblia");

            servico.ExecutaComandoSqlNoBancoDeDados(sql, out object retorno);

            List<Versiculo> versiculos = new List<Versiculo>();

            foreach (DataRow linha in (retorno as DataTable).Rows)
            {
                versiculos.Add(new Versiculo()
                {
                    Id                  = Convert.ToInt32(linha["id"]),
                    Livro_id            = Convert.ToInt32(linha["livro_id"]),
                    Capitulo_id         = Convert.ToInt32(linha["capitulo_id"]),
                    Versao_id           = Convert.ToInt32(linha["versao_id"]),
                    Numero              = Convert.ToInt32(linha["numero"]),
                    Formatado           = Convert.ToString(linha["formatado"])
                });
            }

            _erro = retorno.ToString();

            Trace.WriteLine(_erro);

            return versiculos;
        }
        */


        public LivroCapVers ListarVersiculos(int _livro_id, int _capitulo_id, out string _erro)
        {
            _erro = null;

            dynamic busca = new dynamic[2];
            busca[0] = _livro_id;
            busca[1] = _capitulo_id;

            string sql = string.Format("" +

            "SELECT vers.[id], vers.[livro_id], vers.[capitulo_id], vers.[versao_id], vers.[numero], vers.[formatado], " +

            "(SELECT COUNT(1) FROM [BIBLIA].[dbo].[versiculo] versiculo WHERE versiculo.livro_id = '{0}' AND versiculo.capitulo_id = '{1}') AS qtd_vers, " +

            "(SELECT COUNT(1) FROM [BIBLIA].[dbo].[capitulo] capitulos WHERE capitulos.livro_id = '{0}') AS qtd_caps, " +

            "livro.nome AS livro_nome, livro.sigla AS livro_sigla, livro.testamento AS livro_testamento " +

            "FROM [BIBLIA].[dbo].[versiculo] vers " +

            "INNER JOIN [BIBLIA].[dbo].[livro] livro ON vers.livro_id = livro.id " +

            "WHERE vers.[livro_id] = '{0}' AND vers.[capitulo_id] = '{1}' " +

            "ORDER BY vers.numero ASC", busca

            );

            Service servico = new Service("Biblia");

            servico.ExecutaComandoSqlNoBancoDeDados(sql, out object retorno);

            List<Versiculo> versiculos      = new List<Versiculo>();

            LivroCapVers LivroCapVers       = new LivroCapVers();

            List<VersiculoSlim> VersSlim = new List<VersiculoSlim>();

            foreach (DataRow linha in (retorno as DataTable).Rows)
            {
                versiculos.Add(new Versiculo()
                {
                    Id                      = Convert.ToInt32(linha["id"]),
                    Livro_id                = Convert.ToInt32(linha["livro_id"]),
                    Capitulo_id             = Convert.ToInt32(linha["capitulo_id"]),
                    Versao_id               = Convert.ToInt32(linha["versao_id"]),
                    Numero                  = Convert.ToInt32(linha["numero"]),
                    Formatado               = Convert.ToString(linha["formatado"]),
                    Livro_nome              = Convert.ToString(linha["livro_nome"]),
                    Livro_sigla             = Convert.ToString(linha["livro_sigla"]),
                    Livro_testamento        = Convert.ToString(linha["livro_testamento"]),
                    Qtd_vers                = Convert.ToInt32(linha["qtd_vers"]),
                    Qtd_caps                = Convert.ToInt32(linha["qtd_caps"])
                });

                VersSlim.Add(new VersiculoSlim()
                {
                    numero                  = Convert.ToInt32(linha["numero"]),
                    formatado               = Convert.ToString(linha["formatado"])
                });

            }

            //tratar excessão de retorno vazio ou diferente de array (versiculos[?])...

            //trazer na query a quantidade de capitulos do livro

            LivroCapVers.Error = false;

            try
            {
                if (versiculos != null || versiculos.Count > 0)
                {
                    LivroCapVers.Livro_id       = versiculos[0].Livro_id;
                    LivroCapVers.Capitulo_id    = versiculos[0].Capitulo_id;
                    LivroCapVers.Vers_total     = versiculos[0].Qtd_vers;
                    LivroCapVers.Caps_total     = versiculos[0].Qtd_caps;
                    LivroCapVers.Livro_nome     = versiculos[0].Livro_nome;
                    LivroCapVers.Livro_sigla    = versiculos[0].Livro_sigla;
                    LivroCapVers.Testamento     = versiculos[0].Livro_testamento;
                }
            }
            catch (ArgumentOutOfRangeException outOfRange)
            {
                var dict = new Dictionary<string, dynamic>
                {
                    ["state"] = true,
                    ["message"] = outOfRange.Message
                };

                LivroCapVers.Error = dict;
                Trace.WriteLine("Error: {0}", outOfRange.Message);
            }

            LivroCapVers.VersiculosList     = VersSlim;

            _erro = retorno.ToString();

            Trace.WriteLine(_erro);

            return LivroCapVers;
        }

        public List<Versiculo> ListarVersiculo(int _livro_id, int _capitulo_id, int _versiculo_id, out string _erro)
        {
            _erro = null;

            dynamic busca = new dynamic[3];
            busca[0] = _livro_id;
            busca[1] = _capitulo_id;
            busca[2] = _versiculo_id;


            string sql = string.Format("" +

                "SELECT [id], [livro_id], [capitulo_id], [versao_id], [numero], [formatado] " +

                "FROM [BIBLIA].[dbo].[versiculo] " +

                "WHERE [livro_id] = '{0}' AND [capitulo_id] = '{1}' AND [id] = '{2}' " +

                "ORDER BY [numero] ", busca

            );

            Service servico = new Service("Biblia");

            servico.ExecutaComandoSqlNoBancoDeDados(sql, out object retorno);

            List<Versiculo> versiculo = new List<Versiculo>();

            foreach (DataRow linha in (retorno as DataTable).Rows)
            {
                versiculo.Add(new Versiculo()
                {
                    Id                  = Convert.ToInt32(linha["id"]),
                    Livro_id            = Convert.ToInt32(linha["livro_id"]),
                    Capitulo_id         = Convert.ToInt32(linha["capitulo_id"]),
                    Versao_id           = Convert.ToInt32(linha["versao_id"]),
                    Numero              = Convert.ToInt32(linha["numero"]),
                    Formatado           = Convert.ToString(linha["formatado"])
                });
            }

            _erro = retorno.ToString();

            Trace.WriteLine(_erro);

            return versiculo;
        }

        public List<Versiculo> Pesquisar(string _palavra, out string _erro)
        {
            _erro = null;

            string sql = string.Format("" +

                "SELECT DISTINCT vers.id, vers.capitulo_id, vers.livro_id, vers.numero, vers.formatado, " +

                "livro.nome AS livro_nome, livro.sigla AS livro_sigla, cap.id AS capitulo " +

                "FROM [BIBLIA].[dbo].[versiculo] vers " +

                "INNER JOIN [BIBLIA].[dbo].[livro] livro ON vers.livro_id = livro.id " +

                "INNER JOIN [BIBLIA].[dbo].[capitulo] cap ON vers.capitulo_id = cap.id " +

                "WHERE vers.Formatado LIKE '%{0}%' " +

                "ORDER BY vers.livro_id ASC, vers.capitulo_id ASC, vers.numero ASC", _palavra

            );

            Trace.WriteLine(sql);

            Service servico = new Service("Biblia");

            servico.ExecutaComandoSqlNoBancoDeDados(sql, out object retorno);

            List<Versiculo> versiculo = new List<Versiculo>();

            foreach (DataRow linha in (retorno as DataTable).Rows)
            {
                versiculo.Add(new Versiculo()
                {
                    Id                  = Convert.ToInt32(linha["id"]),
                    Capitulo_id         = Convert.ToInt32(linha["capitulo_id"]),
                    Livro_id            = Convert.ToInt32(linha["livro_id"]),
                    Numero              = Convert.ToInt32(linha["numero"]),
                    Formatado           = Convert.ToString(linha["formatado"]),
                    Livro_nome          = Convert.ToString(linha["livro_nome"]),
                    Livro_sigla         = Convert.ToString(linha["livro_sigla"]),
                    Capitulo            = Convert.ToInt32(linha["capitulo"])
                });
            }

            _erro = retorno.ToString();

            Trace.WriteLine(_palavra);
            if (_erro !=  null)
            {
                Trace.WriteLine(_erro);
            }


            return versiculo;
        }

        public LivroCapVers ListarVers(int _livro_id, int _capitulo_id, out string _erro)
        {
            _erro = null;

            dynamic busca = new dynamic[2];
            busca[0] = _livro_id;
            busca[1] = _capitulo_id;

            string sql = string.Format("" +

            "SELECT vers.[id], vers.[livro_id], vers.[capitulo_id], vers.[versao_id], vers.[numero], vers.[formatado], " +

            "(SELECT COUNT(1) FROM [BIBLIA].[dbo].[versiculo] versiculo WHERE versiculo.livro_id = '{0}' AND versiculo.capitulo_id = '{1}') AS qtd_vers, " +

            "(SELECT COUNT(1) FROM [BIBLIA].[dbo].[capitulo] capitulos WHERE capitulos.livro_id = '{0}') AS qtd_caps, " +

            "livro.nome AS livro_nome, livro.sigla AS livro_sigla, livro.testamento AS livro_testamento " +

            "FROM [BIBLIA].[dbo].[versiculo] vers " +

            "INNER JOIN [BIBLIA].[dbo].[livro] livro ON vers.livro_id = livro.id " +

            "WHERE vers.[livro_id] = '{0}' AND vers.[capitulo_id] = '{1}' " +

            "ORDER BY vers.numero ASC", busca

            );

            Service servico = new Service("Biblia");

            servico.ExecutaComandoSqlNoBancoDeDados(sql, out object retorno);

            List<Versiculo> versiculos      = new List<Versiculo>();

            LivroCapVers LivroCapVers       = new LivroCapVers();

            List<VersiculoSlim> VersSlim = new List<VersiculoSlim>();

            foreach (DataRow linha in (retorno as DataTable).Rows)
            {
                versiculos.Add(new Versiculo()
                {
                    Id                      = Convert.ToInt32(linha["id"]),
                    Livro_id                = Convert.ToInt32(linha["livro_id"]),
                    Capitulo_id             = Convert.ToInt32(linha["capitulo_id"]),
                    Versao_id               = Convert.ToInt32(linha["versao_id"]),
                    Numero                  = Convert.ToInt32(linha["numero"]),
                    Formatado               = Convert.ToString(linha["formatado"]),
                    Livro_nome              = Convert.ToString(linha["livro_nome"]),
                    Livro_sigla             = Convert.ToString(linha["livro_sigla"]),
                    Livro_testamento        = Convert.ToString(linha["livro_testamento"]),
                    Qtd_vers                = Convert.ToInt32(linha["qtd_vers"]),
                    Qtd_caps                = Convert.ToInt32(linha["qtd_caps"])
                });

                VersSlim.Add(new VersiculoSlim()
                {
                    numero                  = Convert.ToInt32(linha["numero"]),
                    formatado               = Convert.ToString(linha["formatado"])
                });

            }

            //tratar excessão de retorno vazio ou diferente de array (versiculos[?])...

            //trazer na query a quantidade de capitulos do livro

            LivroCapVers.Error = false;

            try
            {
                if (versiculos != null || versiculos.Count > 0)
                {
                    LivroCapVers.Livro_id       = versiculos[0].Livro_id;
                    LivroCapVers.Capitulo_id    = versiculos[0].Capitulo_id;
                    LivroCapVers.Vers_total     = versiculos[0].Qtd_vers;
                    LivroCapVers.Caps_total     = versiculos[0].Qtd_caps;
                    LivroCapVers.Livro_nome     = versiculos[0].Livro_nome;
                    LivroCapVers.Livro_sigla    = versiculos[0].Livro_sigla;
                    LivroCapVers.Testamento     = versiculos[0].Livro_testamento;
                }
            }
            catch (ArgumentOutOfRangeException outOfRange)
            {
                var dict = new Dictionary<string, dynamic>
                {
                    ["state"] = true,
                    ["message"] = outOfRange.Message
                };

                LivroCapVers.Error = dict;
                Trace.WriteLine("Error: {0}", outOfRange.Message);
            }

            LivroCapVers.VersiculosList     = VersSlim;

            _erro = retorno.ToString();

            Trace.WriteLine(_erro);

            return LivroCapVers;
        }

    }
}