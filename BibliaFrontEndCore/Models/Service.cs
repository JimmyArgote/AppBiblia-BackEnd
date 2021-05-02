using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace BibliaFrontEndCore.Models
{
    public class Service
    {
        #region Atributos e Propriedades
        public string connectionString;

        #endregion

        #region Construtores

        /// <summary>
        /// Para criar um objeto Service é obrigatório informar o nome da ConnectionString informada no App.Config
        /// </summary>
        /// <param name="_connStringName">Nome/Apelido da ConnectionString</param>
        public Service(string _connStringName)
        {
            this.connectionString = ConfigurationManager.ConnectionStrings[_connStringName].ConnectionString;
        }

        #endregion

        #region Métodos
        /// <summary>
        /// Método Genérico para execução de comandos em SQL no Sql Server
        /// </summary>
        /// <param name="_comandoSql">Comando SQL a ser executado</param>
        /// <param name="_retorno">
        /// Lista de objetos a ser retornado ao método solicitante
        /// Insert -> Retorna Id do novo registro
        /// Updade/Delete -> Retorna a quantidade de registros afetados no banco de dados
        /// Select -> Retorna um DataTable
        /// </param>
        public void ExecutaComandoSqlNoBancoDeDados(string _comandoSql, out object _retorno)
        {
            _retorno = new List<object>();
            try
            {
                SqlConnection conn      = new SqlConnection();
                conn.ConnectionString   = connectionString;
                SqlCommand command      = new SqlCommand();
                command.Connection      = conn;
                command.CommandType     = CommandType.Text;
                command.CommandText     = _comandoSql;
                if (_comandoSql.StartsWith("SELECT"))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet);

                    _retorno = dataSet.Tables[0];

                }
                else if (_comandoSql.StartsWith("INSERT"))
                {
                    conn.Open();
                    int id = (int)command.ExecuteScalar();
                    conn.Close();
                    _retorno = id;
                }
                else
                {
                    conn.Open();
                    int linhasAfetadas = command.ExecuteNonQuery();
                    conn.Close();
                    _retorno = linhasAfetadas;
                }
            }
            catch (Exception e)
            {
                _retorno = e.Message;
            }
        }

        #endregion
    }
}
