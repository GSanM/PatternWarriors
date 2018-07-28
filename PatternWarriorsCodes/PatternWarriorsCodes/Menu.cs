using Library;
using HeroSpace;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Xml;
using System.Threading;

static class Constants
{
	public const int TEXT_SPEED1 = 20;
	public const int TEXT_SPEED2 = 50;
	public const int TEXT_SPEED3 = 100;
}

namespace Menu
{
	public class Conta
	{
		public string Username { get; set; }
		public string Senha { get; set; }

        //Cria Arquivo XML com header
        private void CriaXml()
		{
			XmlDocument doc = new XmlDocument();

			XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
			doc.InsertBefore(xmlDeclaration, root);

			XmlElement elementUsers = doc.CreateElement(string.Empty, "Users", string.Empty);
            doc.AppendChild(elementUsers);

			doc.Save("database.xml");
            
		}

        //Insere conta no arquivo XML
		private void InsereContaXml(string user, string pass)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load("database.xml");

			XmlNode NodeUsers = doc.SelectSingleNode("Users");

            XmlElement elementUser = doc.CreateElement(string.Empty, "User", string.Empty);
            NodeUsers.AppendChild(elementUser);

            XmlElement elementUsername = doc.CreateElement(string.Empty, "username", string.Empty);
            XmlText username = doc.CreateTextNode(user);
            elementUsername.AppendChild(username);
            elementUser.AppendChild(elementUsername);

            XmlElement elementPass = doc.CreateElement(string.Empty, "password", string.Empty);
            XmlText passwd = doc.CreateTextNode(pass);
            elementPass.AppendChild(passwd);
            elementUser.AppendChild(elementPass);

			XmlElement elementChars = doc.CreateElement(string.Empty, "Chars", string.Empty);
			elementUser.AppendChild(elementChars);

            XmlElement elementSave = doc.CreateElement(string.Empty, "save", string.Empty);
            elementUser.AppendChild(elementSave);

			doc.Save("database.xml");
		}

        //Salva no arquivo XML o progresso do usuario
        public void Salvar(string username, string fase)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load("database.xml");
            
			XmlNodeList aNodes = doc.SelectNodes("/Users/User");
			foreach(XmlNode node in aNodes)
			{
				XmlNode NodeUsername = node.SelectSingleNode("username");
				if (NodeUsername.InnerText == username)
				{
					XmlNode NodeSave = node.SelectSingleNode("save");
                    NodeSave.InnerText = fase;
				}
			}
                       
			doc.Save("database.xml");
		}

		public void SalvaChar(string user, string pass, string charname, string charclass)
		{
			XmlDocument doc = new XmlDocument();
            doc.Load("database.xml");

			XmlNodeList aNodes = doc.SelectNodes("/Users/User");
			foreach (XmlNode node in aNodes)
			{
				XmlNode NodeUsername = node.SelectSingleNode("username");
				if (NodeUsername.InnerText == user)
				{
					XmlNode NodePassw = node.SelectSingleNode("password");
					if (NodePassw.InnerText == pass)
					{
						XmlNode NodeChars = node.SelectSingleNode("Chars");
						XmlElement elementNewChar = doc.CreateElement(string.Empty, "char", string.Empty);
						XmlAttribute attCharClass = doc.CreateAttribute("class");
						attCharClass.Value = charclass;
						elementNewChar.Attributes.SetNamedItem(attCharClass);
						XmlText charNameText = doc.CreateTextNode(charname);
						elementNewChar.AppendChild(charNameText);
						NodeChars.AppendChild(elementNewChar);                        
						break;
					}
				}
			}

            doc.Save("database.xml");
		}

        //Procura o save do usuario no arquivo XML
		public string Load(string username, string passw)
		{
			bool found = false;
			string answer = "noSave";
			XmlDocument doc = new XmlDocument();
            doc.Load("database.xml");

            XmlNodeList aNodes = doc.SelectNodes("/Users/User");
            foreach (XmlNode node in aNodes)
            {
                XmlNode NodeUsername = node.SelectSingleNode("username");
                if (NodeUsername.InnerText == username)
                {
					XmlNode NodePassw = node.SelectSingleNode("password");
					if (NodePassw.InnerText == passw)
					{
						XmlNode NodeFase = node.SelectSingleNode("save");
						answer = NodeFase.InnerText;
						found = true;
						break;
					}
                }
            }
			Console.WriteLine("");
			if (found == true)
			{
				library.slowWrite("Loading...", Constants.TEXT_SPEED3, true);
			}
			else if(found == false)
			{
				library.slowWrite("Incorrect username or password!", Constants.TEXT_SPEED1, true);
			}
			Thread.Sleep(2000);
			return answer;
		}

        //Cria uma nova conta
        public int CriaConta()
		{
			library.slowWrite("Welcome to Pattern Warriors! Let's create an account for you.", Constants.TEXT_SPEED2, true);
			library.slowWrite("Username: ", Constants.TEXT_SPEED2, false);
			Username = Console.ReadLine();
			library.slowWrite("Password: ", Constants.TEXT_SPEED2, false);
			while (true)
            {
                var key = System.Console.ReadKey(true);
				Console.Write("*");
                if (key.Key == ConsoleKey.Enter)
                    break;
				if (key.Key == ConsoleKey.Backspace)
					Senha.Remove(Senha.Length - 1);
				else
                    Senha += key.KeyChar;
            }

			//Salvar no arquivo
			if (!File.Exists("database.xml"))
			    this.CriaXml();
			this.InsereContaXml(Username, Senha);
           
			return 1;
		}

        //Faz login em uma conta ja existente
        public int Login()
		{
			string save = "noSave";

            while (save == "noSave")
			{
				Console.Clear();
				Senha = "";
				library.slowWrite("Username: ", Constants.TEXT_SPEED2, false);
                Username = Console.ReadLine();
                library.slowWrite("Password: ", Constants.TEXT_SPEED2, false);
                while (true)
                {
                    var key = System.Console.ReadKey(true);
					Console.Write("*");
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    if (key.Key == ConsoleKey.Backspace)
                        Senha.Remove(Senha.Length - 1);
                    else
                        Senha += key.KeyChar;
                }
                
                save = this.Load(Username, Senha);
			}
            
			return 1;
		}

	}

	public class Save //Memento utilizado para salvar estados
	{
		private string State;

		public class Memento
		{
			private string state;
			public string State
			{
				get
				{
					return state;
				}
			}

			public Memento(string s)
			{
				state = s;
			}
		}

		public void SetState(string s)
		{
			State = s;
		}

        public string GetState()
		{
			return State;
		}

		public Memento SaveState()
		{
			return new Memento(State);
		}

		public void RestoreState(Memento memento)
		{
			State = memento.State;
		}
	}

    public class Caretaker
	{
		private Stack<Save.Memento> saves = new Stack<Save.Memento>();
		private Save save;

		public Caretaker(Save s)
		{
			save = s;
		}

		public void SaveState()
        {
			Save.Memento memento;
			memento = save.SaveState();
            saves.Push(memento);
        }

        public void RestoreState()
        {
			save.RestoreState(saves.Pop());
        }

        public int Empty()
        {
			if (saves.Count == 0)
                return 1;
            else
                return 0;
        }
	}

    //Singleton no Menu Principal para existir apenas uma instância de menu.
	sealed class MenuPrincipal
	{
		private static MenuPrincipal instance = null;
		private static readonly object padlock = new object();
		private int chosed;

		public static MenuPrincipal Instance
		{
			get 
			{
				lock(padlock)
				{
					if(instance == null)
					{
						instance = new MenuPrincipal();
					}
					return instance;
				}
			}
		}

        public void ImprimeMenu()
		{
			library.slowWrite( "||||||||||||||||||||||||||", Constants.TEXT_SPEED1, true );
			library.slowWrite( "|||| Pattern Warriors ||||", Constants.TEXT_SPEED2, true );
			library.slowWrite( "||||||||||||||||||||||||||", Constants.TEXT_SPEED1, true );
			library.slowWrite( "|||||||| 1 Start  ||||||||", Constants.TEXT_SPEED2, true );
			library.slowWrite( "|||||||| 2 Load   ||||||||", Constants.TEXT_SPEED2, true );
			library.slowWrite( "|||||||| 3 Quit   ||||||||", Constants.TEXT_SPEED2, true );
			library.slowWrite( "||||||||||||||||||||||||||", Constants.TEXT_SPEED1, true );
		}

        public void ImprimeAtkDef()
		{
			library.slowWrite("1 Attack", Constants.TEXT_SPEED1, true);
			library.slowWrite("2 Deffend", Constants.TEXT_SPEED1, true);
			Console.Write("> ");
			Console.WriteLine("CHAMAR AQUI UMA FUNC DE OP DE ATK OU DEF");
			//Convert.ToInt32(Console.ReadLine());
		}

		public void ImprimeFala(int fala, string username)
        {
            switch (fala)
            {
                case 1:
                    library.slowWrite(username += " was searching for knowledge, then he decided to leave in his journey to find the Pattern Scrolls. Each scroll hold the secret of a certain pattern. To reach them, it is necessary to defeat the monsters that keep them.", Constants.TEXT_SPEED2, true);
                    break;
            }
        }

        public int NewChar(Conta conta)
		{
			string charname;
			string charclass;

			library.slowWrite("Let's create a new character!", Constants.TEXT_SPEED2, true);
			library.slowWrite("What will be his/her name? ", Constants.TEXT_SPEED2, false);
			charname = Console.ReadLine();

			//Colocar tabelhinha maneira aqui
            Console.WriteLine("");
			library.slowWrite("Choose a class:", Constants.TEXT_SPEED2, true);

            Hero Warrior = new Warrior("");
            Hero Mage = new Mage("");
            Hero Assassin = new Assassin("");

            Warrior.showPresentation();
            Mage.showPresentation();
            Assassin.showPresentation();

            while(true)
			{
				charclass = Console.ReadLine();
                if (charclass == "Assassin" || charclass == "assassin")
                {
                    library.slowWrite("Creation succeed! Welcome to Assassins' World.", Constants.TEXT_SPEED2, true);
					break;
                }
                else if (charclass == "Mage" || charclass == "mage")
                {
                    library.slowWrite("Creation succeed! Welcome to Mages' World.", Constants.TEXT_SPEED2, true);
					break;
                }
                else if (charclass == "Warrior" || charclass == "warrior")
                {
                    library.slowWrite("Creation succeed! Welcome to Warriors' World.", Constants.TEXT_SPEED2, true);
					break;
                }
				else
				{
					Console.WriteLine("Incorret name! Try again");
				}
			}
            conta.SalvaChar(conta.Username, conta.Senha, charname, charclass);

			return 1;
		}

        public void SetChosed()
		{

			library.slowWrite("\n> Select your option: ", Constants.TEXT_SPEED2, false);
			chosed = Convert.ToInt32(Console.ReadLine());

			switch(chosed)
			{
				case 1:
					Conta nova = new Conta();
					MenuPrincipal menuPrincipal = Instance;
					nova.CriaConta();
					Console.Clear();
					menuPrincipal.NewChar(nova);
					menuPrincipal.ImprimeFala(1, nova.Username);
                    //Colocar opcao de escolher char
					break;
				case 2:
					Conta nova2 = new Conta();
					nova2.Login();
					break;
				case 3:
					Console.Clear();
					library.slowWrite("Thank you for playing! Goodbye.", Constants.TEXT_SPEED3, true);
					Environment.Exit(1);
					break;
			}
            
		}



        public static int Main()
		{
			Menu.MenuPrincipal menuPrincipal = new MenuPrincipal();
			menuPrincipal.ImprimeMenu();
			menuPrincipal.SetChosed();

            //Teste de Save
			Save memoria = new Save();
			Caretaker armazenador = new Caretaker(memoria);

			/*
			memoria.SetState("Estado 1");
			armazenador.SaveState();
			Console.WriteLine(memoria.GetState());

			memoria.SetState("Estado 2");
			Console.WriteLine(memoria.GetState());

			armazenador.RestoreState();
			Console.WriteLine(memoria.GetState());
            */
           

			return 0;
		}
	}
}