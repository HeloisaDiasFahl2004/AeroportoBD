using System;
using System.Collections.Generic;
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

    }
}
