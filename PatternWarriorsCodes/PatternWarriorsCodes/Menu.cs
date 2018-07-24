using Library;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

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
		private string Username { get; set; }
		private string Senha { get; set; }

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
            }

            //Salvar no arquivo

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

        public void SetChosed()
		{

			library.slowWrite("\n> Select your option: ", Constants.TEXT_SPEED2, false);
			chosed = Convert.ToInt32(Console.ReadLine());

			switch(chosed)
			{
				case 1:
					Conta nova = new Conta();
					nova.CriaConta();
					break;
				case 2:
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

			memoria.SetState("Estado 1");
			armazenador.SaveState();
			Console.WriteLine(memoria.GetState());

			memoria.SetState("Estado 2");
			Console.WriteLine(memoria.GetState());

			armazenador.RestoreState();
			Console.WriteLine(memoria.GetState());

			return 0;
		}
	}
}