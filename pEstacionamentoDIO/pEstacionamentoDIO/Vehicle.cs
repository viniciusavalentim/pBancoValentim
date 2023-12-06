using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pEstacionamentoDIO
{
    internal class Vehicle
    {
        public string Plate { get; set; }
        public double InitialPrice { get; set; }
        public double PricePerHour { get; set; }

        List<Vehicle> Plates = new List<Vehicle>();

        public Vehicle() { }

        public Vehicle(string plate)
        {
            Plate = plate;
        }

        public Vehicle(double initialPrice, double pricePerHour)
        {
            InitialPrice = initialPrice;
            PricePerHour = pricePerHour;
        }

        public void GetPrices()
        {

            Console.WriteLine("Seja bem vindo ao Estacionamento!");
            Console.Write("Digite o preço inicial: ");
            double initialPrice = double.Parse(Console.ReadLine());
            Console.Write("Digite o preço por hora: ");
            double pricePerHour = double.Parse(Console.ReadLine());

            InitialPrice = initialPrice;
            PricePerHour = pricePerHour;

        }

        public void AddVehicle()
        {
            Console.Write("Digita a placa do veículo para estacionar (Ex: ABC-1234): ");
            string plate = Console.ReadLine();

            while (!ValidadePlate(plate))
            {
                Console.Write("Placa Inválida! Por favor, digite no formato correto (*EXEMPLO*: ABC-1234): ");
                plate = Console.ReadLine();
            }

            Plates.Add(new Vehicle(plate));

            Console.WriteLine("Placa Adicionada com sucesso!");
            Console.WriteLine("Pressione [enter] para voltar a tela inicial");
            Console.ReadKey();

        }

        public void RemoveVehicles()
        {
            Console.Write("Digite a placa do veículo para remover: ");
            string plate = Console.ReadLine();
            while (!ValidadePlate(plate))
            {
                Console.Write("Placa Inválida! Por favor, digite no formato correto (*EXEMPLO*: ABC-1234): ");
                plate = Console.ReadLine();
            }
            
            Console.Write("Digite a quantidade de horas que o veículo permaneceu estacionado: ");
            int hour = int.Parse(Console.ReadLine());
            double total = 0;

            total = (hour * PricePerHour) + InitialPrice;


            foreach (Vehicle vehicle in Plates.ToList())
            {
                if (plate == vehicle.Plate)
                {
                    Plates.Remove(vehicle);
                    Console.WriteLine($"O veículo {vehicle.Plate} foi removido e o preço total foi de: ${total}");
                }
            }

            Console.WriteLine($"Pressione [enter] para sair {total}");
            Console.ReadKey();
        }

        public void ShowVehicles()
        {
            Console.WriteLine("Os veículos estacionados são: ");
            foreach (Vehicle obj in Plates)
            {
                Console.WriteLine(obj.Plate.ToUpper());
            }
            Console.WriteLine("Pressione [enter] para sair");
            Console.ReadKey();
        }

        public bool ValidadePlate(string plate)
        {
            string pattern = @"^[a-zA-Z]{3}-\d{4}$";
            return Regex.IsMatch(plate, pattern);
        }

    }
}
