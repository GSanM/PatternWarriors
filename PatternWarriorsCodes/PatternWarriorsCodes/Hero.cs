using System;
using Library;

static class Constants
{
    public const int TEXT_SPEED1 = 20;
    public const int TEXT_SPEED2 = 50;
    public const int TEXT_SPEED3 = 100;
}

namespace HeroSpace
{
	public abstract class Hero
	{
		public class Ataque
        {
			private int id;
			public int Id 
			{ 
				get { return id; } 
				set { id = value; }
			}
			private string nome;
			public string Nome 
			{ 
				get { return nome; }
                set { nome = value; } 
			}
			private int dano;
			public int Dano 
			{
				get { return dano; }
                set { dano = value; }
			}

            public void Atacar()
            {
                //Possivelmente utilizar composite para combo
            }

        }
        
		protected Random random = new Random();

		protected string name;
		protected string classe;
		protected string description;
		protected Ataque atk1 = new Ataque();
		protected Ataque atk2 = new Ataque();
		protected Ataque atk3 = new Ataque();
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
            library.slowWrite(this.classe + " - " + this.description, Constants.TEXT_SPEED1, true);
			Console.WriteLine("");
		}
	}

	public class Warrior: Hero
	{
		public Warrior(string nome)
		{
			//Atk 1
			atk1.Id = 1;
			atk1.Nome = "";
			atk1.Dano = 1;

			//Atk 2
			atk2.Id = 2;
			atk2.Nome = "";
			atk2.Dano = 1;
            
			//Atk 3
			atk3.Id = 3;
			atk3.Nome = "";
			atk3.Dano = 1;

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
            atk1.Id = 1;
            atk1.Nome = "";
            atk1.Dano = 1;

            //Atk 2
            atk2.Id = 2;
            atk2.Nome = "";
            atk2.Dano = 1;

            //Atk 3
            atk3.Id = 3;
            atk3.Nome = "";
            atk3.Dano = 1;

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
            atk1.Id = 1;
            atk1.Nome = "";
            atk1.Dano = 1;

            //Atk 2
            atk2.Id = 2;
            atk2.Nome = "";
            atk2.Dano = 1;

            //Atk 3
            atk3.Id = 3;
            atk3.Nome = "";
            atk3.Dano = 1;

			classe = "Assassin";
            name = nome += "The Unstoppable Assasin";
            level = 1;

            description = "The Assassin has a lot of damage but could be killed easily if don't take care.";

            ATK = 30;
            DEF = 8;
            lifeSize = 150;
        }
	}
}