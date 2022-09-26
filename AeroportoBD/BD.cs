using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportoBD
{
    internal class BD
    {
        string Conexao = "Data Source = 'localhost\\SQLSERVER'; Initial Catalog = OnTheFly;User Id = sa; Password= sqlservermeu;";

        public BD()
        {

        }
        public string Caminho()
        {
            return Conexao;
        }
        public void InsertCompanhiaAerea(CompanhiaAerea novaComp)
        {
            try
            {
                BD bd = new BD();
                SqlConnection conexaosql = new SqlConnection(bd.Caminho());
                conexaosql.Open();
                string insertComp = $"INSERT INTO CompanhiaAerea(CNPJ,RazaoSocial,SituacaO,DataAbertura,DataCadastro,DataUltimoVoo FROM CompanhiaAerea"
            }
    }
}
