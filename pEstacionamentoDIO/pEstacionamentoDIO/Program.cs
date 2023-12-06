using pEstacionamentoDIO;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;

namespace pEstacionamentoDIO
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Vehicle vehicle = new Vehicle();
            vehicle.GetPrices();
            Menu(vehicle);

        }

        public static void Menu(Vehicle vehicle)
        {
            Console.Clear();
            int opc = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Digite o valor da sua opção: ");
                Console.WriteLine("1 => Cadastrar Veiculo");
                Console.WriteLine("2 => Remover Veiculo");
                Console.WriteLine("3 => Listar Veiculo");
                Console.WriteLine("4 => Encerrar");
                opc = int.Parse(Console.ReadLine());
                while(opc > 4 && opc < 0)
                {
                    Console.Write("Opção Invalida! Tente novamente: ");
                    opc = int.Parse(Console.ReadLine());
                }

                switch (opc)
                {
                    case 1: vehicle.AddVehicle();
                        break;
                    case 2: vehicle.RemoveVehicles();
                        break;
                    case 3: vehicle.ShowVehicles(); 
                        break;
                    default:
                        break;
                }


            }
            while (opc != 4);

            Console.WriteLine("Programa encerrado!");
            Console.ReadKey();
        }
    } 
}