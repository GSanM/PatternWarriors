using System;
using System.IO;
using System.Text;

namespace Menu
{
	class Conta
	{
		private string Username { get; set; }
		private string Senha { get; set; }

		Conta(string un, string sen)
		{
			Username = un;
			Senha = sen;
		}

        public int CriaConta()
		{
            //Salvar no arquivo

			return 1;
		}

	}

    class Save //Implementar com Memento
	{
		
	}

	class MenuPrincipal
	{
		private int chosed;
        

        public void ImprimeMenu()
		{
			Console.WriteLine( "[-----------------]" );
            Console.WriteLine( "[--- Game Name ---]" );
            Console.WriteLine( "[-----------------]" );
            Console.WriteLine( "[---- > Start ----]" );
			Console.WriteLine( "[-----------------]" );
		}
        public static int Main()
		{
			Menu.MenuPrincipal menuPrincipal = new MenuPrincipal();
			menuPrincipal.ImprimeMenu();

			return 0;
		}
	}
}