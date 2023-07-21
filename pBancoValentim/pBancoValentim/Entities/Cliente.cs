using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pBancoValentim.Entities
{
    internal class Cliente : Pessoa
    {
        public string Usuario { get; set; }
        public int Senha { get; set; }

        public Cliente()
        {

        }
        public Cliente(string nome, string cpf, DateTime data, string usuario, int senha)
            : base(nome, cpf, data)

        {
            Usuario = usuario;
            Senha = senha;
        }

     

        
    }
}
