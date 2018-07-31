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
        }

		protected Random random = new Random();

		protected string name;
		protected string classe;
		protected string description;
		protected Ataque atk1 = new Ataque();
		protected Ataque atk2 = new Ataque();
		protected Ataque atk3 = new Ataque();
		protected int spec_atk;
		protected int combo;
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
		public abstract int getATK();
        
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
		public void setCombo(int c)
		{
			combo = c;
		}
		public int getCombo()
		{
			return combo;
		}
		public void addCombo()
		{
			combo++;
		}
		public void setSpecAttack(int sa)
		{
			spec_atk = sa;
		}

        //Metodos de itens
		public void ItemAtk(int item)
		{
			ATK += item;
		}

		public void ItemDef(int item)
        {
            DEF += item;
        }

		public void ItemLife(int item)
        {
            lifeSize += item;
        }

		public void Potion(int item)
        {
            actualLife += item;
        }
        //============================

		public int ImprimeAtaque()
		{
			library.slowWrite("1 - " + atk1.Nome, Constants.TEXT_SPEED1, true);
			library.slowWrite("2 - " + atk2.Nome, Constants.TEXT_SPEED1, true);
			library.slowWrite("3 - " + atk3.Nome, Constants.TEXT_SPEED1, true);
			library.slowWrite("4 - Basic Attack", Constants.TEXT_SPEED1, true);

			return Convert.ToInt32(Console.ReadLine());
		}
                                
		public void showStatus()
        {
            //    Console.Clear();
            string[,] arrValues = new string[4, 1];

            arrValues[0, 0] = "Hero " + name;
			arrValues[1, 0] = "Life " + actualLife.ToString() + "/" + lifeSize.ToString();
			arrValues[2, 0] = "ATK " + ATK.ToString();
            arrValues[3, 0] = "DEF " + DEF.ToString();

            ArrayPrinter.PrintToConsole(arrValues);
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
			classe = "Warrior";
            name = nome += " The Grand Warrior";
            level = 1;

            description = "The Warrior is a strong and powerful class that should be choosed under a immeasurable certitude. With him, your path will can be hard as a rock, but he can handle it.";

			ATK = 15 * level;
            DEF = 15 * level;
            lifeSize = 200 * level;
            actualLife = lifeSize;
			spec_atk = 0;

			//Atk 1
            atk1.Id = 1;
            atk1.Nome = "The Power Hammer";
            atk1.Dano = 2 * ATK;

            //Atk 2
            atk2.Id = 2;
            atk2.Nome = "Infinite Smash";
            atk2.Dano = 3 * ATK;
            
            //Atk 3
            atk3.Id = 3;
            atk3.Nome = "Ultimate Blade";
            atk3.Dano = 4 * ATK;
                                                
        }

		//Composite para o combo
		public override int getATK()
        {
			
            if(combo == 3)
			{
				return (atk1.Dano + atk2.Dano + atk3.Dano);
            }
            else
            {                                
                switch(spec_atk)
                {
                    case 1:
                        return atk1.Dano;                            
                    case 2:
                        return atk2.Dano;                            
                    case 3:
                        return atk3.Dano;                            
                    case 4:
                        return ATK; 
					default:
                        return ATK;
                }   
            }
        }   
	}

	public class Mage: Hero
	{
		public Mage(string nome)
		{
			classe = "Mage";
			name = nome += " The Reliable Mage";
            level = 1;

            description = "The Mage is a wise class that should be choosed carefully. A mage without control can be dangerous.";

            ATK = 20 * level;
            DEF = 10 * level;
            lifeSize = 130 * level;
			actualLife = lifeSize;

			//Atk 1
            atk1.Id = 1;
            atk1.Nome = "Blue Flame";
            atk1.Dano = 3 * ATK;

            //Atk 2
            atk2.Id = 2;
            atk2.Nome = "Ice Edge";
            atk2.Dano = 2 * ATK;

            //Atk 3
            atk3.Id = 3;
            atk3.Nome = "Magic Storm";
            atk3.Dano = 5 * ATK;


        }

		//Composite para o combo
		public override int getATK()
        {
            if(combo == 3)
            {
                return atk1.Dano + atk2.Dano + atk3.Dano;
            }
            else
            {               
                switch(spec_atk)
                {
                    case 1:
                        return atk1.Dano;
                    case 2:
                        return atk2.Dano;
                    case 3:
                        return atk3.Dano;
                    case 4:
                        return ATK;
					default:
                        return ATK;
                }   
            }
        }   
	}

	public class Assassin: Hero
	{
		public Assassin(string nome)
        {
			classe = "Assassin";
            name = nome += "The Unstoppable Assasin";
            level = 1;

            description = "The Assassin has a lot of damage but could be killed easily if don't take care.";

            ATK = 30 * level;
            DEF = 8 * level;
            lifeSize = 150 * level;
			actualLife = lifeSize;

			//Atk 1
            atk1.Id = 1;
            atk1.Nome = "Backdoor";
            atk1.Dano = 5 * ATK;

            //Atk 2
            atk2.Id = 2;
            atk2.Nome = "Furtive Attack";
            atk2.Dano = 5 * ATK;

            //Atk 3
            atk3.Id = 3;
            atk3.Nome = "Killing Force";
            atk3.Dano = 8 * ATK;
        }

		//Composite para o combo
		public override int getATK()
        {
            if(combo == 3)
            {
                return atk1.Dano + atk2.Dano + atk3.Dano;
            }
            else
            {                                
                switch(spec_atk)
                {
                    case 1:
                        return atk1.Dano;                            
                    case 2:
                        return atk2.Dano;                           
                    case 3:
                        return atk3.Dano;                            
                    case 4:
                        return ATK; 
					default:
						return ATK;
                }   
            }
        }   
	}

}