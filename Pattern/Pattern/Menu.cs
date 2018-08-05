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
	public const int TEXT_SPEED_MENU = 25;
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

        public bool ContaExistente(string username)
		{
			XmlDocument doc = new XmlDocument();
            doc.Load("database.xml");
            
            XmlNodeList aNodes = doc.SelectNodes("/Users/User");
            foreach(XmlNode node in aNodes)
            {
                XmlNode NodeUsername = node.SelectSingleNode("username");
                if (NodeUsername.InnerText == username)
                {
					return true;
                }
            }
			return false;
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

        //Salva char no XML
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
					XmlNode NodeChars = node.SelectSingleNode("Chars");
					XmlElement elementNewChar = doc.CreateElement(string.Empty, "char", string.Empty);
					XmlAttribute attCharClass = doc.CreateAttribute("class");
					attCharClass.Value = charclass;
					elementNewChar.Attributes.SetNamedItem(attCharClass);
					XmlAttribute attCharStage = doc.CreateAttribute("level");
					attCharStage.Value = "1";
					elementNewChar.Attributes.SetNamedItem(attCharStage);
					XmlText charNameText = doc.CreateTextNode(charname);
					elementNewChar.AppendChild(charNameText);
					NodeChars.AppendChild(elementNewChar);                        
					break;

				}
			}

            doc.Save("database.xml");
		}

        //Salva level ao passar de fase no XML
        public void SalvaLevel(string username, int level)
		{
			XmlDocument doc = new XmlDocument();
            doc.Load("database.xml");

            XmlNodeList aNodes = doc.SelectNodes("/Users/User");
            foreach (XmlNode node in aNodes)
            {
				XmlNode NodeUsername = node.SelectSingleNode("username");
                if (NodeUsername.InnerText == username)
                {
                    XmlNode NodeChars = node.SelectSingleNode("Chars");
                    foreach (XmlNode char_ in NodeChars)
                    {
						char_.Attributes["level"].Value = level.ToString();
                    }
                }
			}
		}

        //Load Menu
		public string ImprimeLoadMenu()
		{
			library.slowWrite("###################################################", Constants.TEXT_SPEED_MENU, true);
            library.slowWrite("#################### 1 Continue  ##################", Constants.TEXT_SPEED_MENU, true);
            library.slowWrite("#################### 2 New Char  ##################", Constants.TEXT_SPEED_MENU, true);
            library.slowWrite("#################### 3 Quit      ##################", Constants.TEXT_SPEED_MENU, true);
            library.slowWrite("###################################################", Constants.TEXT_SPEED_MENU, true);

			library.slowWrite("Select your option: ", Constants.TEXT_SPEED_MENU, false);
			return Console.ReadLine();
		}

        //Procura o save do usuario no arquivo XML
		public string Load(string username, string passw)
		{
			bool found = false;
			string answer = "noSave";
			if (!File.Exists("database.xml"))
                this.CriaXml();
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
				Thread.Sleep(2000);
			}

			return answer;
		}

        //Carrega Chars da conta
		public Hero LoadChars(string username)
		{
			XmlDocument doc = new XmlDocument();
            doc.Load("database.xml");
			Stack <string> chars = new Stack<string>();
			int n = 1;
			List<XmlNode> charNodes = new List<XmlNode>();
            
            XmlNodeList aNodes = doc.SelectNodes("/Users/User");
            foreach (XmlNode node in aNodes)
            {
                XmlNode NodeUsername = node.SelectSingleNode("username");
                if (NodeUsername.InnerText == username)
                {
                    XmlNode NodeChars = node.SelectSingleNode("Chars");
                    foreach (XmlNode char_ in NodeChars)
					{
						charNodes.Add(char_);
						Console.WriteLine(n + " - " + char_.InnerText + " - " + char_.Attributes["class"].Value);
						n++;
					}
                }
            }

			library.slowWrite("Select your char: ", Constants.TEXT_SPEED2, false);
            
			int input = Convert.ToInt32(Console.ReadLine());           

			switch(charNodes[input-1].Attributes["class"].Value)
			{
				case "Warrior":
					return new Warrior(charNodes[input-1].InnerText, Convert.ToInt32(charNodes[input-1].Attributes["level"].Value));
                
				case "Mage":
					return new Mage(charNodes[input-1].InnerText, Convert.ToInt32(charNodes[input - 1].Attributes["level"].Value));

				case "Assassin":
					return new Assassin(charNodes[input-1].InnerText, Convert.ToInt32(charNodes[input - 1].Attributes["level"].Value));
				
				default:
					return null;
			}
            
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
                if (key.Key == ConsoleKey.Enter)
                    break;
                if (key.Key == ConsoleKey.Backspace)
                    Senha.Remove(Senha.Length - 1);
                else
                    Senha += key.KeyChar;
                Console.Write("*");
            }

			//Salvar no arquivo
            if (!File.Exists("database.xml"))
                this.CriaXml();

			while(ContaExistente(Username))
			{
				Console.WriteLine("");
				library.slowWrite("Username already exists! Try another one.", Constants.TEXT_SPEED2, true);
				Senha = "";
                library.slowWrite("Username: ", Constants.TEXT_SPEED2, false);
                Username = Console.ReadLine();
                library.slowWrite("Password: ", Constants.TEXT_SPEED2, false);
                while (true)
                {
                    var key = System.Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    if (key.Key == ConsoleKey.Backspace)
                        Senha.Remove(Senha.Length - 1);
                    else
                        Senha += key.KeyChar;
                    Console.Write("*");
                }
			}
            
			//Salvar no arquivo
			if (!File.Exists("database.xml"))
			    this.CriaXml();
			this.InsereContaXml(Username, Senha);
           
			return 1;
		}

        //Faz login em uma conta ja existente
        public Hero Login()
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
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    if (key.Key == ConsoleKey.Backspace)
                        Senha.Remove(Senha.Length - 1);
                    else
                        Senha += key.KeyChar;
					Console.Write("*");
                }
                
                save = this.Load(Username, Senha);
			}
			Console.Clear();
			string RespMenu = ImprimeLoadMenu();
            
			switch(Int32.Parse(RespMenu))
			{
				case 1:
					Hero resp = LoadChars(Username);
					if (resp == null)
					{
						library.slowWrite("No char found!", Constants.TEXT_SPEED1, true);
						MenuPrincipal menuPrincipal1 = MenuPrincipal.Instance;
                        menuPrincipal1.NewChar(this);
						break;
					}
					else 
					{
						return resp;
					}

				case 2:
					MenuPrincipal menuPrincipal2 = MenuPrincipal.Instance;
					menuPrincipal2.NewChar(this);
					break;
				case 3:
					Console.Clear();
                    library.slowWrite("Thank you for playing! Goodbye.", Constants.TEXT_SPEED3, true);
                    Environment.Exit(1);
                    break;
			}
			return null;
		}

	}

	public class Save //Memento utilizado para salvar estados
	{
		private int State;

		public class Memento
		{
			private int state;
			public int State
			{
				get
				{
					return state;
				}
			}

			public Memento(int s)
			{
				state = s;
			}
		}

		public void SetState(int s)
		{
			State = s;
		}

        public int GetState()
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

		public void ImprimeMenu2()
		{
			library.slowWrite( "##################################################", 5, true );
			library.slowWrite( "# ______         _    _                          #",    Constants.TEXT_SPEED_MENU, true );
			library.slowWrite( "# | ___ \\       | |  | |                         #",   Constants.TEXT_SPEED_MENU, true );
			library.slowWrite( "# | |_/ /  __ _ | |_ | |_   ___  _ __  _ __      #",    Constants.TEXT_SPEED_MENU, true );
			library.slowWrite( "# |  __/  / _` || __|| __| / _ \\ | '__|| '_  \\   # ", Constants.TEXT_SPEED_MENU, true );
			library.slowWrite( "# | |    | (_| || |_ | |_  | __/ | |   | | | |   #",    Constants.TEXT_SPEED_MENU, true );
			library.slowWrite( "# \\_|     \\__,_| \\__| \\__| \\___| |_| |_| | |_|   #", Constants.TEXT_SPEED_MENU, true );
			library.slowWrite( "#                                                #",    Constants.TEXT_SPEED_MENU, true );
			library.slowWrite( "# _    _                     _                   #",    Constants.TEXT_SPEED_MENU, true );
			library.slowWrite( "#| |  | |                   (_)                  #",    Constants.TEXT_SPEED_MENU, true );
			library.slowWrite( "#| |  | |  __ _  _ __  _ __  _   ___   _ __  ___ #",    Constants.TEXT_SPEED_MENU, true );
			library.slowWrite( "#| |/\\| | / _` || '__|| '__|| | / _ \\ | '__|/ __|#",  Constants.TEXT_SPEED_MENU, true );
			library.slowWrite( "#\\  /\\  /| (_| || |   | |   | || (_) || |   \\__ \\# ", Constants.TEXT_SPEED_MENU, true );
			library.slowWrite( "# \\/  \\/  \\__,_||_|   |_|   |_| \\___/ |_|   |___/#",Constants.TEXT_SPEED_MENU, true );
			library.slowWrite( "#                                                #",    Constants.TEXT_SPEED_MENU, true );

			library.slowWrite( "##################################################", Constants.TEXT_SPEED_MENU, true );
			library.slowWrite( "#################### 1 Start  ####################", Constants.TEXT_SPEED_MENU, true );
			library.slowWrite( "#################### 2 Load   ####################", Constants.TEXT_SPEED_MENU, true );
			library.slowWrite( "#################### 3 Quit   ####################", Constants.TEXT_SPEED_MENU, true );
			library.slowWrite( "##################################################", Constants.TEXT_SPEED_MENU, true );
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
					Console.WriteLine("");
                    library.slowWrite(username += " was searching for knowledge, then the Pattern Scrolls' journey seems to be the best way to find some. Each scroll hold the secret of a certain pattern. To reach them, it is necessary to defeat the monsters that keep them.", Constants.TEXT_SPEED2, true);
                    break;
            }
        }

        public Hero NewChar(Conta conta)
		{
			string charname;
			string charclass;

			library.slowWrite("Let's create a new character!", Constants.TEXT_SPEED2, true);
			library.slowWrite("What will be his/her name? ", Constants.TEXT_SPEED2, false);
			charname = Console.ReadLine();

			//Colocar tabelhinha maneira aqui
            Console.WriteLine("");
			library.slowWrite("Choose a class:", Constants.TEXT_SPEED2, true);

            Hero Warrior = new Warrior("", 1);
            Hero Mage = new Mage("", 1);
            Hero Assassin = new Assassin("", 1);

            Warrior.showPresentation();
            Mage.showPresentation();
            Assassin.showPresentation();

            while(true)
			{
				library.slowWrite("Class: ", Constants.TEXT_SPEED2, false);
				charclass = Console.ReadLine();
				if (charclass == "Assassin" || charclass == "assassin" || charclass == "a" || charclass == "A")
                {
					charclass = "Assassin";
                    library.slowWrite("Creation succeed! Welcome to Assassins' World.", Constants.TEXT_SPEED2, true);
					PrintHeroes.printAssassin ();

					conta.SalvaChar(conta.Username, conta.Senha, charname, charclass);

                    this.ImprimeFala(1, charname);

					return new Assassin(charname, 1);
                }
				else if (charclass == "Mage" || charclass == "mage" || charclass == "m" || charclass == "M")
                {
					charclass = "Mage";
                    library.slowWrite("Creation succeed! Welcome to Mages' World.", Constants.TEXT_SPEED2, true);
					PrintHeroes.printMage ();

					conta.SalvaChar(conta.Username, conta.Senha, charname, charclass);

                    this.ImprimeFala(1, charname);

					return new Mage(charname, 1);
                }
				else if (charclass == "Warrior" || charclass == "warrior" || charclass == "w" || charclass == "W")
                {
					charclass = "Warrior";
                    library.slowWrite("Creation succeed! Welcome to Warriors' World.", Constants.TEXT_SPEED2, true);
					PrintHeroes.printWarrior ();

					conta.SalvaChar(conta.Username, conta.Senha, charname, charclass);

                    this.ImprimeFala(1, charname);

					return new Warrior(charname, 1);
                }
				else
				{
					Console.WriteLine("Incorret name! Try again");
				}
			}
     
		}

        public Hero SetChosed()
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
				    return menuPrincipal.NewChar(nova);
				case 2:
					Conta nova2 = new Conta();
					return nova2.Login();
				case 3:
					Console.Clear();
					library.slowWrite("Thank you for playing! Goodbye.", Constants.TEXT_SPEED3, true);
					Environment.Exit(1);
					break;
				default:
					return null;
			}
			return null;
		}

        public bool TryAgain()
		{
			int op;

            library.slowWrite("Try again?", Constants.TEXT_SPEED3, true);
            library.slowWrite("1 - Yes", Constants.TEXT_SPEED3, true);
            library.slowWrite("2 - No", Constants.TEXT_SPEED3, true);
			op = Convert.ToInt32(Console.ReadLine());

			switch (op)
			{
				case 2:
					Console.Clear();
                    library.slowWrite("Thank you for playing! Goodbye.", Constants.TEXT_SPEED3, true);
                    Environment.Exit(1);
					return false;
				default:
					return true;
			}        
        }

        public static int Main()
		{
			bool win = false;
			int state = 1;

			Menu.MenuPrincipal menuPrincipal = new MenuPrincipal();
			menuPrincipal.ImprimeMenu2();
         
			Tema oTema = new Forest();
			Hero oHero = menuPrincipal.SetChosed();
            
			Save memoria = new Save();
            Caretaker armazenador = new Caretaker(memoria);
            memoria.SetState(state);

			while(!win)
			{
				armazenador.SaveState();
				oHero.revive();
				Stage oStage = new Stage(state, oTema, oHero);

				//Inicia prologo
				oStage.createGraph();
				win = oStage.startStage();
                if(!win)
				    menuPrincipal.TryAgain();

			}
            
			return 0;
		}
	}
}