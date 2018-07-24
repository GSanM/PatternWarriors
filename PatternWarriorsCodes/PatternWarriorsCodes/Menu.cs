using Library;
using System;
using System.IO;
using System.Text;

static class Constants
{
	public const int TEXT_SPEED1 = 50;
	public const int TEXT_SPEED2 = 100;
	public const int TEXT_SPEED3 = 200;
}

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
        
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //capture up arrow key
            if (keyData == Keys.Up)
            {
                MessageBox.Show("You pressed Up arrow key");
                return true;
            }
            //capture down arrow key
            if (keyData == Keys.Down)
            {
                MessageBox.Show("You pressed Down arrow key");
                return true;
            }
        }

        public void ImprimeMenu()
		{
			library.slowWrite( "||||||||||||||||||||||||||", Constants.TEXT_SPEED1 );
			library.slowWrite( "|||| Pattern Warriors ||||", Constants.TEXT_SPEED2 );
			library.slowWrite( "||||||||||||||||||||||||||", Constants.TEXT_SPEED1 );
			library.slowWrite( "|||||||| > Start  ||||||||", Constants.TEXT_SPEED2 );
			library.slowWrite(" |||||||| > Load   ||||||||", Constants.TEXT_SPEED2);
			library.slowWrite(" |||||||| > Quit   ||||||||", Constants.TEXT_SPEED2);
			library.slowWrite( "||||||||||||||||||||||||||", Constants.TEXT_SPEED1 );
		}

        public static int Main()
		{
			Menu.MenuPrincipal menuPrincipal = new MenuPrincipal();
			menuPrincipal.ImprimeMenu();

			return 0;
		}
	}
}