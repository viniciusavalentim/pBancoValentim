using pBancoValentim.Entities;
using pBancoValentim.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Numerics;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace pBancoValentim
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int OpcaoUsuario = 0;
            Conta conta = new Conta();
           
            do
            {
                OpcaoUsuario = Menu();

                switch (OpcaoUsuario)
                {
                    case 1:
                        conta.CadastrarConta();
                        break;

                    case 2:
                        conta.Logar();
                        break;
             
                    default:
                        Console.WriteLine("Número Inválido!\nTente Novamente [Enter]");
                        Console.ReadKey();
                        break;
                }
            }
            while (OpcaoUsuario != 0);


        }

        public static int Menu()
        { 
            Console.Clear();
            Cabecalho();
            Console.WriteLine("\nSeja Bem Vindo ao Banco Do Valentim!\nPara iniarmos, digite como deseja entrar em nosso banco, \nse for novo por aqui faça um cadastro!\n");
            int valorMenu;

            Console.WriteLine("[1] - Create Account");
            Console.WriteLine("[2] - Login Account");

            return valorMenu = int.Parse(Console.ReadLine()); ;
        }

        public static void Cabecalho()
        {
            Console.WriteLine("════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine("                                                  Banco Valentim                                                        ");
            Console.WriteLine("════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
        }

    }
}
