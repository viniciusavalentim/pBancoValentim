using pBancoValentim.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace pBancoValentim.Entities
{
    internal class Conta : Cliente
    {
        public double SaldoDaConta { get; set; }
        public int Id { get; set; }

        public List<Conta> Contas = new List<Conta>();

        public Conta()
        {

        }
        public Conta(string usuario, int senha)
        {
            Usuario = usuario;
            Senha = senha;
        }
        public Conta(string nome, string cpf, DateTime data, string usuario, int senha, double saldoDaConta, int id)
            : base(nome, cpf, data, usuario, senha)
        {
            SaldoDaConta = saldoDaConta;
            Id = id;
        }

        //public void CadastrarConta()
        //{
        //    Nome = "Vinicius";
        //    CPF = "44210163813";
        //    Data = DateTime.Now;
        //    Usuario = "vini";
        //    Senha = 1234;
        //    SaldoDaConta = 0.0;

        //    Contas.Add(new Conta(Nome, CPF, Data, Usuario, Senha, SaldoDaConta));

        //}

        public void CadastrarConta()
        {
            Conta conta = new Conta();
            Console.Clear();
            Cabecalho();
            Console.WriteLine("\nBem vindo a ciação de conta no banco viniboy, vamos começar: \n");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("|       Dados Pessoais      |");
            Console.WriteLine("-----------------------------");
            Console.Write("Digite seu nome: ");
            Nome = Console.ReadLine().ToLower();
            Console.Write("Digite seu CPF: ");
            CPF = Console.ReadLine();

            while (!ValidateCPF(CPF))
            {
                Console.WriteLine("CPF INVÁLIDO!\nInsira novamente um CPF Válido: ");
                CPF = Console.ReadLine();
            }

            Console.Write("Digite sua data de Aniversário (dd/mm/aaaa): ");
            Data = DateTime.Parse(Console.ReadLine());

            while (!ValidateAge(Data))
            {
                Console.WriteLine("CADASTRO INVALIDO! USUARIO MENOR DE IDADE. TECLE [alguma tecla] PARA SAIR");
                Console.ReadKey();
                CadastrarConta();
            }


            Console.WriteLine("-----------------------------");
            Console.WriteLine("|      Dados da conta:      |");
            Console.WriteLine("-----------------------------");
            Console.Write("Crie um Usuário: ");
            Usuario = Console.ReadLine();


            Console.Write("Crie uma senha (A senha deve conter 4 números): ");
            Senha = int.Parse(Console.ReadLine());

            Random random = new Random();
            Id = random.Next(1, 1000);

            Contas.Add(new Conta(Nome, CPF, Data, Usuario, Senha, SaldoDaConta = 0, Id));

            string usuario = Usuario;
            int senha = Senha;
            int id = Id;

            ContaCriadaComSucesso(usuario, senha, id);
            Console.ReadKey();

        }

        public void Logar()
        {

            Conta conta = new Conta();
            Console.Clear();
            Cabecalho();
            Console.WriteLine("-----------------------------");
            Console.WriteLine("|      Dados De Login:      |");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("\nDigite seu usuário: ");
            string login = Console.ReadLine().ToLower();
            Console.WriteLine("Digite sua senha: ");
            int senha = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite seu id: ");
            int id = int.Parse(Console.ReadLine());
            if (ValidarConta(login, senha, id))
            {
                Console.Write("Tentando logar");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.WriteLine(".");
                Console.WriteLine("* Você Logou *\nAperte a tecla [Enter] para continuar!");
                Console.ReadKey();
                OpcoesBancariasDoCliente(login, senha, id);

            }
            else
            {
                Console.Write("Tentando logar");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.WriteLine(".");
                Console.WriteLine("Login Inválido!\nTente novamente ou saia:");
                Console.WriteLine("[1] - Tentar Novamente");
                Console.WriteLine("[0] - Sair");
                int opcaoTentarNovamente = int.Parse(Console.ReadLine());
                if (opcaoTentarNovamente == 1)
                {
                    Logar();
                }
                else if (opcaoTentarNovamente == 0)
                {
                    Sair();
                }
            }

        }

        public void OpcoesBancariasDoCliente(string usuario, int senha, int id)
        {
            foreach (Conta obj in Contas)
            {
                if (usuario == obj.Usuario && senha == obj.Senha && id == Id)
                {
                    Console.Clear();
                    Cabecalho();
                    Console.WriteLine($"Fico feliz em estar aqui novamente Sr(a) {obj.Nome}!\n");
                    Console.WriteLine($"Dados do usuário: \n| Nome: {obj.Nome}\n| Aniversário: {obj.Data}\n| CPF: {obj.CPF}\n| Saldo: ${obj.SaldoDaConta}\n| ID: #{obj.Id}\n\n");

                    Console.WriteLine(" [ Operações bancarias ] ");
                    Console.WriteLine("[1] - Sacar");
                    Console.WriteLine("[2] - Depositar");
                    Console.WriteLine("[3] - Tranferências");
                    Console.WriteLine("[4] - Voltar ao Menu Incial");
                    int opcaoDoCliente = int.Parse(Console.ReadLine());

                    switch (opcaoDoCliente)
                    {
                        case 1:
                            Saque(usuario, senha, id);
                            break;
                        case 2:
                            Deposito(usuario, senha, id);
                            break;
                        case 3:
                            Transferencia(usuario, senha, id);
                            break;
                        case 4:
                            Sair();
                            break;
                        default:
                            break;
                    }

                    Console.ReadKey();
                }
            }
        }

        public bool ValidarConta(string usuario, int senha, int id)
        {
            foreach (Conta Obj in Contas)
            {
                if (Obj.Usuario == usuario && Obj.Senha == senha && id == Obj.Id)
                {
                    return true;
                }
            }
            return false;

        }

        public void Deposito(string usuario, int senha, int id)
        {
            Console.Clear();
            Cabecalho();
            Console.WriteLine("Digite quanto quer depositar: ");
            Console.Write("$"); double deposito = double.Parse(Console.ReadLine());
            foreach (Conta obj in Contas)
            {
                if (usuario == obj.Usuario && senha == obj.Senha)
                {
                    obj.SaldoDaConta += deposito;
                    Console.Write("Depositando, aguarde");
                    Thread.Sleep(1000);
                    Console.Write(".");
                    Thread.Sleep(1000);
                    Console.Write(".");
                    Thread.Sleep(1000);
                    Console.WriteLine(".");
                    Console.WriteLine($"| Você Depositou ${deposito} reais\n");
                    Console.WriteLine("Deposito finalizado, Saldo atual: $" + obj.SaldoDaConta);
                    Console.WriteLine("Aperte a tecla [Enter] para continuar!");
                }
            }


            Console.ReadKey();
            OpcoesBancariasDoCliente(usuario, senha, id);
        }

        public void Saque(string usuario, int senha, int id)
        {
            Console.Clear();
            Cabecalho();
            Console.WriteLine("Digite quanto quer Sacar: ");
            foreach (Conta obj in Contas)
            {
                if (usuario == obj.Usuario && senha == obj.Senha && id == Id)
                {
                    Console.WriteLine($"Seu Limite de saque é de: ${obj.SaldoDaConta} reais");
                    Console.Write("$"); double saque = double.Parse(Console.ReadLine());

                    while (saque > obj.SaldoDaConta)
                    {
                        Console.WriteLine($"O seu saldo é de ${obj.SaldoDaConta} reais\nPortanto não é possivel sacar ${saque} reais!\nTente novamente: ");
                        Console.WriteLine("Ou aperte a tecla [0] para voltar");
                        saque = int.Parse(Console.ReadLine());
                        if (saque == 0)
                        {
                            OpcoesBancariasDoCliente(usuario, senha, id);
                        }
                    }

                    obj.SaldoDaConta -= saque;
                    Console.Write("Realizando o saque, aguarde");
                    Thread.Sleep(1000);
                    Console.Write(".");
                    Thread.Sleep(1000);
                    Console.Write(".");
                    Thread.Sleep(1000);
                    Console.WriteLine(".");
                    Console.WriteLine($"| Você Sacou ${saque} reais\n");
                    Console.WriteLine("Saque finalizado, Saldo atual: $" + obj.SaldoDaConta);
                    Console.WriteLine("Aperte a tecla [Enter] para continuar!");

                    Console.ReadKey();
                    OpcoesBancariasDoCliente(usuario, senha, id);
                }
            }


            Console.ReadKey();
            OpcoesBancariasDoCliente(usuario, senha, id);
        }

        public void Transferencia(string usuario, int senha, int id)
        {
            Console.Clear();
            Cabecalho();
            double transferencia = 0;
            string usuario2 = null;
            int id2 = 0;
            foreach (Conta obj in Contas)
            {
                if (usuario == obj.Usuario && senha == obj.Senha && id == Id)
                {
                    Console.WriteLine($"Digite quanto quer Transferir [Saldo atual: ${obj.SaldoDaConta}]: ");
                    Console.Write("$"); transferencia = double.Parse(Console.ReadLine());
                    while (transferencia > obj.SaldoDaConta)
                    {
                        Console.WriteLine($"O seu saldo é de ${obj.SaldoDaConta} reais\nPortanto não é possivel transferir ${transferencia} reais!\nTente novamente: ");
                        Console.WriteLine("Ou aperte a tecla [0] para voltar");
                        transferencia = int.Parse(Console.ReadLine());
                        if (transferencia == 0)
                        {
                            OpcoesBancariasDoCliente(usuario, senha, id);
                        }
                    }
                    Console.WriteLine("\nPara quem você quer transferir? ");
                    Console.Write("Digite o Usuário da pessoa que deseja enviar o dinheiro: ");
                    usuario2 = Console.ReadLine();
                    Console.WriteLine("Digite o Id da conta de quem vai receber: ");
                    id2 = int.Parse(Console.ReadLine());
                    obj.SaldoDaConta -= transferencia;
                    Console.Write("Realizando a Transferência, aguarde");
                    Thread.Sleep(1000);
                    Console.Write(".");
                    Thread.Sleep(1000);
                    Console.Write(".");
                    Thread.Sleep(1000);
                    Console.WriteLine(".");
                    Console.WriteLine("Transferencia finalizada, Saldo atual: $" + obj.SaldoDaConta);
                    Console.WriteLine("Aperte a tecla [Enter] para continuar!");
                }
            }
            foreach (Conta obj in Contas)
            {
                if (usuario2 == obj.Usuario && id2 == obj.Id)
                {
                    obj.SaldoDaConta += transferencia;
                }
            }
            Console.ReadKey();
            OpcoesBancariasDoCliente(usuario, senha, id);
        }

        public bool ValidateCPF(string CPF)
        {
            string valor = CPF.Replace(".", "");
            valor = valor.Replace("-", "");
            if (valor.Length != 11)
                return false;
            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;
            if (igual || valor == "12345678909")
                return false;
            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(
                  valor[i].ToString());
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];
            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];
            resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;
            return true;
        }

        public bool ValidateAge(DateTime Aniversario)
        {

            Conta conta = new Conta();
            int age = (DateTime.Now.Year - conta.Data.Year);
            if (age < 18)
            {
                Console.WriteLine("Menor de idade!!!");
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ContaCriadaComSucesso(string usuario, int senha, int id)
        {
            Console.Clear();
            Cabecalho();
            foreach(Conta obj in Contas)
            {
                if (usuario == obj.Usuario && senha == obj.Senha && id == obj.Id)
                {
                    Console.WriteLine($"Parabéns Sr(a) {obj.Nome}\nSua conta foi criada com sucesso!\n");
                    Console.WriteLine($"Geramos um ID da sua conta: #{obj.Id}\nNão perca.");
                    Console.WriteLine("Aproveite nosso banco e as novidades do BancoValentim");
                    Console.WriteLine("Aperte a tecla [ENTER] para sair");
                    Sair();

                }
            }
        }

        public static void Cabecalho()
        {
            Console.WriteLine("════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine("                                                  Banco Valentim                                                        ");
            Console.WriteLine("════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
        }

        public void Sair()
        {

        }
    }
}
