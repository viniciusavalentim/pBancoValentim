using pBancoValentim.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace pBancoValentim.Entities
{
    internal class Conta : Cliente
    {
        public float SaldoDaConta { get; set; }
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
        public Conta(string nome, string cpf, DateTime data, string usuario, int senha, float saldoDaConta, int id)
            : base(nome, cpf, data, usuario, senha)
        {
            SaldoDaConta = saldoDaConta;
            Id = id;
        }

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
            SaldoDaConta = 5000;


            ContaCriadaComSucesso(usuario, senha, id);
            CreatePerson(Nome, CPF, usuario, senha, id, Data, SaldoDaConta);
            Console.ReadKey();



        }
        public static void CreatePerson(string name, string cpf, string login, int passwordUser, int id, DateTime age, float accountPerson)
        {
            Database dataBase = new Database();

            SqlConnection connectionSql = new SqlConnection(dataBase.ConnectionString());    // parametro necessario para SqlConnection é uma string
            SqlCommand commandSql = new SqlCommand();                                              //criando uma variavel para fazer comandos do sql                                                                                          // que identifica o caminho de conexão com o banco
            connectionSql.Open(); //Abrindo conexão sql

            string commandInsert = $"INSERT INTO Person (Name, Age, CPF, Login, PasswordUser, Id, AccountBalance) VALUES ('{name}','{age}','{cpf}','{login}','{passwordUser}','{id}','{accountPerson}')";

            commandSql = new SqlCommand(commandInsert, connectionSql);
            commandSql.ExecuteNonQuery();
            connectionSql.Close(); //fecha conexão sql

        }

        public static void AddAccountBalance(int id, double balance)
        {
            Database dataBase = new Database();
            SqlConnection connectionSql = new SqlConnection(dataBase.ConnectionString());
            try
            {
                connectionSql.Open();

                // Use parâmetros para evitar injeção de SQL
                string cmdSelect = $"SELECT * FROM Person WHERE Id = {id}";

                using (SqlCommand commandSql = new SqlCommand(cmdSelect, connectionSql))
                {
                    // Execute a consulta
                    using (SqlDataReader reader = commandSql.ExecuteReader())
                    {
                        if (reader.Read()) //enquanto leitor for verdadeiro
                        {
                            balance += reader.GetDouble(6);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lide com exceções, registre ou notifique conforme necessário
                Console.WriteLine($"Erro ao fazer depósito conta: {ex.Message}");
            }
            finally
            {
                // Certifique-se de fechar a conexão, independentemente do resultado
                connectionSql.Close();
            }

            // Se não encontrou a conta no banco de dados, retorne falso

            UpdateAccountBalance(id, balance);
        }
        public static void RemoveAccountBalance(int id, double balance, double aux)
        {
            Database dataBase = new Database();
            SqlConnection connectionSql = new SqlConnection(dataBase.ConnectionString());
            try
            {
                connectionSql.Open();

                // Use parâmetros para evitar injeção de SQL
                string cmdSelect = $"SELECT * FROM Person WHERE Id = {id}";

                using (SqlCommand commandSql = new SqlCommand(cmdSelect, connectionSql))
                {
                    // Execute a consulta
                    using (SqlDataReader reader = commandSql.ExecuteReader())
                    {
                        if (reader.Read()) //enquanto leitor for verdadeiro
                        {
                            aux = reader.GetDouble(6);
                            aux -= balance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lide com exceções, registre ou notifique conforme necessário
                Console.WriteLine($"Erro ao fazer saque conta: {ex.Message}");
            }
            finally
            {
                // Certifique-se de fechar a conexão, independentemente do resultado
                connectionSql.Close();
            }

            // Se não encontrou a conta no banco de dados, retorne falso


            UpdateAccountBalance(id, aux);
        }
        public static void UpdateAccountBalance(int id, double balance)
        {
            Database dataBase = new Database();

            SqlConnection connectionSql = new SqlConnection(dataBase.ConnectionString());    // parametro necessario para SqlConnection é uma string
            SqlCommand commandSql = new SqlCommand();                                              //criando uma variavel para fazer comandos do sql                                                                                          // que identifica o caminho de conexão com o banco
            connectionSql.Open(); //Abrindo conexão sql

            string commandInsert = $"SELECT * FROM Person Update person set AccountBalance = {balance} where id = {id}";

            commandSql = new SqlCommand(commandInsert, connectionSql);
            commandSql.ExecuteNonQuery();
            connectionSql.Close(); //fecha conexão sql
        }
        public static void GetPerson()
        {
            Database dataBase = new Database();

            SqlConnection connectionSql = new SqlConnection(dataBase.ConnectionString());    // parametro necessario para SqlConnection é uma string
            SqlCommand commandSql = new SqlCommand();                                              //criando uma variavel para fazer comandos do sql                                                                                          // que identifica o caminho de conexão com o banco
            connectionSql.Open(); //Abrindo conexão sql

            string cmdInsert = $"SELECT * FROM Person WHERE idade=19";

            commandSql = new SqlCommand(cmdInsert, connectionSql);
            commandSql.ExecuteNonQuery();

            using (SqlDataReader read = commandSql.ExecuteReader())
            {

                while (read.Read()) //enquanto leitor for verdadeiro
                {
                    Console.WriteLine("\nName: {0}", read.GetString(0)); //{0} posição da variavel q vai informar, parametro 0, cada linha começa no zero
                    Console.WriteLine("Idade: {0}", read.GetInt32(1));
                }
            }

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
                Console.ReadKey();

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
            Console.Clear();
            Cabecalho();
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

        public bool ValidarConta(string login, int passwordUser, int id)
        {

            Database dataBase = new Database();

            SqlConnection connectionSql = new SqlConnection(dataBase.ConnectionString());    
            try
            {
                connectionSql.Open();

                // Use parâmetros para evitar injeção de SQL
                string cmdSelect = $"SELECT * FROM Person WHERE Login = '{login}' AND PasswordUser = {passwordUser} AND Id = {id}";

                using (SqlCommand commandSql = new SqlCommand(cmdSelect, connectionSql))
                {
                    // Execute a consulta
                    using (SqlDataReader reader = commandSql.ExecuteReader())
                    {
                        // Se houver uma linha no resultado, a conta é válida
                        if (reader.HasRows)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lide com exceções, registre ou notifique conforme necessário
                Console.WriteLine($"Erro ao fazer depósito conta: {ex.Message}");
            }
            finally
            {
                // Certifique-se de fechar a conexão, independentemente do resultado
                connectionSql.Close();
            }

            // Se não encontrou a conta no banco de dados, retorne falso
            return false;
        }

        public void Deposito(string usuario, int senha, int id)
        {
            Console.Clear();
            Cabecalho();
            Console.WriteLine("Digite quanto quer depositar: ");
            Console.Write("$"); double balance = double.Parse(Console.ReadLine());

            AddAccountBalance(id, balance);

            Console.WriteLine("Sua operação foi realizada com sucesso ");
            Console.WriteLine("Precione [enter] para sair =))");
            Console.ReadKey();
            OpcoesBancariasDoCliente(usuario, senha, id);
        }

        public void Saque(string usuario, int senha, int id)
        {
            Console.Clear();
            Cabecalho();
            Console.WriteLine("Digite quanto quer Sacar: ");
            Console.Write("$"); double balance = double.Parse(Console.ReadLine());
            double aux = 0;

            RemoveAccountBalance(id, balance,aux);

            Console.WriteLine("Sua operação foi realizada com sucesso ");
            Console.WriteLine("Precione [enter] para sair =))");
            Console.ReadKey();
            OpcoesBancariasDoCliente(usuario, senha, id);
        }

        public void Transferencia(string login, int senha, int id)
        {
            Console.Clear();
            Cabecalho();
            double aux = 0;
            double transfer = 0;
            int id2 = 0;
            Console.WriteLine($"Digite quanto quer Transferir ");
            Console.Write("$"); transfer = double.Parse(Console.ReadLine());
            Console.WriteLine("\n\nPara quem você deseja transferir? ");
            Console.WriteLine("Digite o Id da conta de quem vai receber: ");
            id2 = int.Parse(Console.ReadLine());

            RemoveAccountBalance(id, transfer, aux);
            AddAccountBalance(id2, transfer);

            Console.Write("Realizando a Transferência, aguarde");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.WriteLine(".");
            Console.WriteLine("Transferencia finalizada");
            Console.WriteLine("Aperte a tecla [Enter] para continuar!");

            //while (transferencia > obj.SaldoDaConta)
            //{
            //    Console.WriteLine($"O seu saldo é de ${obj.SaldoDaConta} reais\nPortanto não é possivel transferir ${transferencia} reais!\nTente novamente: ");
            //    Console.WriteLine("Ou aperte a tecla [0] para voltar");
            //    transferencia = int.Parse(Console.ReadLine());
            //    if (transferencia == 0)
            //    {
            //        OpcoesBancariasDoCliente(usuario, senha, id);
            //    }
            //}
            Console.ReadKey();
            OpcoesBancariasDoCliente(login, senha, id);
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
