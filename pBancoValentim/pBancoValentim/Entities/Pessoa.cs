using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace pBancoValentim.Entities
{
    internal class Pessoa
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime Data { get; set; }

        public Pessoa()
        {

        }
        public Pessoa(string nome, string cpf, DateTime data)
        {
            Nome = nome;
            CPF = cpf;
            Data = data;
        }


    }
}
