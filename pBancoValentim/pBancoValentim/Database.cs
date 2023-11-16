using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace pBancoValentim
{
    internal class Database
    {
        private static string Connection = "Data Source =VINICIUS\\SQLEXPRESS; Initial Catalog =Banco_valentim; User ID=vinic; Password=12345678;";

        public Database()
        {

        }
        public string ConnectionString()
        {
            return Connection; //retorna essa conexao pra onde for chamado
        }

    }
}
