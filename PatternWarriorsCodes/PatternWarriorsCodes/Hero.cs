using System;

static class Constants
{
    public const int TEXT_SPEED1 = 20;
    public const int TEXT_SPEED2 = 50;
    public const int TEXT_SPEED3 = 100;
}

namespace Library
{
	public abstract class Hero
	{
		public abstract class Ataque
        {
			public int id { get; set; }
			public string nome { get; set; }
			public int dano { get; set; }

            public void Atacar()
            {
                //Possivelmente utilizar composite para combo
            }


        }
        
		protected Random random = new Random();

		protected string name;
		protected string classe;
		protected string description;
		protected Ataque atk1;
		protected Ataque atk2;
		protected Ataque atk3;
		protected int level;
		protected int ATK;
		protected int DEF;
		protected int lifeSize;
		protected int actualLife;
		protected bool ATKMode;
		protected bool DEFMode;
		protected bool RUNMode;

		public void Attack()
		{
			ATKMode = true;
			DEFMode = false;
			RUNMode = false;
		}
		public void Defence()
		{
			ATKMode = false;
			DEFMode = true;
			RUNMode = false;
		}
		public void runAway()
		{
			ATKMode = false;
			DEFMode = false;
			RUNMode = true;
		}

		public void receiveDamage(int damage)
		{
			actualLife = actualLife - damage;
		}

		public string getMode()
		{
			if (ATKMode == true)
			{
				return "ATK";
			}
			if (DEFMode == true)
			{
				return "DEF";
			}
			if (RUNMode == true)
			{
				return "RUN";
			}
			return "DEF";
		}
		public int getLife()
		{
			return actualLife;
		}
		public int getLifeSize()
		{
			return lifeSize;
		}
		public int getATK()
		{
			return ATK;
		}
		public int getDEF()
		{
			return DEF;
		}
		public string getName()
		{
			return name;
		}
		public string getDescription()
		{
			return description;
		}

		public void showStatus()
		{
			Console.WriteLine("Name: " + name);
			Console.WriteLine("Life: " + lifeSize + "/" + actualLife);
			Console.WriteLine("Level: " + level);
			Console.WriteLine("ATK: " + ATK);
			Console.WriteLine("DEF: " + DEF);
		}

        public void showPresentation()
		{
			//Colocar na tabela
			Console.WriteLine("Teste");
			library.slowWrite(this.classe += this.description, Constants.TEXT_SPEED1, true);
		}
	}

	public class Warrior: Hero
	{
		public Warrior(string nome)
		{
			//Atk 1
			atk1.id = 1;
			atk1.nome = "";
			atk1.dano = 1;

			//Atk 2
			atk2.id = 2;
			atk2.nome = "";
			atk2.dano = 1;

			//Atk 3
			atk3.id = 3;
			atk3.nome = "";
			atk3.dano = 1;

			classe = "Warrior";
			name = nome += " The Grand Warrior";
			level = 1;

			description = "The Warrior is a strong and powerful class that should be choosed under a immeasurable certitude. With him, your path will can be hard as a rock, but he can handle it.";

			ATK = 15;
			DEF = 15;
			lifeSize = 200;
		}
	}

	public class Mage: Hero
	{
		public Mage(string nome)
		{
			//Atk 1
            atk1.id = 1;
            atk1.nome = "";
            atk1.dano = 1;

            //Atk 2
            atk2.id = 2;
            atk2.nome = "";
            atk2.dano = 1;

            //Atk 3
            atk3.id = 3;
            atk3.nome = "";
            atk3.dano = 1;

			classe = "Mage";
			name = nome += " The Reliable Mage";
            level = 1;

            description = "The Mage is a wise class that should be choosed carefully. A mage without control can be dangerous.";

            ATK = 20;
            DEF = 10;
            lifeSize = 130;
		}
	}

	public class Assassin: Hero
	{
		public Assassin(string nome)
        {
			//Atk 1
            atk1.id = 1;
            atk1.nome = "";
            atk1.dano = 1;

            //Atk 2
            atk2.id = 2;
            atk2.nome = "";
            atk2.dano = 1;

            //Atk 3
            atk3.id = 3;
            atk3.nome = "";
            atk3.dano = 1;

			classe = "Assassin";
            name = nome += "The Unstoppable Assasin";
            level = 1;

            description = "The Assassin has a lot of damage but could be killed easily if don't take care.";

            ATK = 30;
            DEF = 8;
            lifeSize = 150;
        }
	}

	public class Mainha
	{
		public static int Main()
		{
			Hero Warrior = new Warrior("");
			Hero Mage = new Mage("");
			Hero Assassin = new Assassin("");

			Warrior.showPresentation();
			Mage.showPresentation();
			Assassin.showPresentation();

            return 0;
        }
	}	
}