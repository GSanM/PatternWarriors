/**
 * ..:: Library for Monsters and Fights ::..
 * 
 * @author Rafael Romeu
 * @author Gabriel Moraes 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Library
{
/** ..:: Factory Pattern ::.. 
 *
 */
    class MonsterFactory
	{
		public Monster CreateMonster(string name, int level)
		{
			Monster oMonster = null;
			switch (name)
            {
                case "GigantSpider":
                    oMonster = new GigantSpider(level);
                break;
                case "Zombie":
                    oMonster = new Zombie(level);
                break;
                default:
                Console.WriteLine("não foi possivel criar monstro");
                break;
            }
			
			return oMonster;
		}
	}
	public abstract class Monster
	{
        protected Random random = new Random();

		protected string name;
        protected int level;
        protected int ATK;
        protected int DEF;
        protected int lifeSize;
        protected int actualLife;
        protected string description;
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
            if(ATKMode == true)
            { 
                return "ATK";
            }
            if(DEFMode == true)
            {
               return "DEF";
            }
            if(RUNMode == true)
            {
               return "RUN";
            }
            return "DEF";
        }
        public int getLife()
        {
            return actualLife;
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
	}
    public class GigantSpider: Monster
	{
		public GigantSpider(int level)
        {
            name = "Gigant Spider";
            this.level = level;    

            ATK = random.Next(10,20) * level;
            DEF = random.Next(5,15) * level;
            
            lifeSize = 60 * level;
            actualLife = lifeSize;

            ATKMode = false;
            DEFMode = false;
            RUNMode = false;
        }
	}
    public class Zombie: Monster
	{
		public Zombie(int level)
        {
            name = "Zombie";
            this.level = level;    

            ATK = random.Next(16,30) * level;
            DEF = random.Next(0,5) * level;
            
            lifeSize = 40 * level;
            actualLife = lifeSize;

            ATKMode = false;
            DEFMode = false;
            RUNMode = false;
        }
	}
/** ..:: Template Pattern ::.. 
 */
    abstract class Fight
    {        
        protected Random random = new Random();
        
        public void startFight()
        {
            
        }
        //verificarstatusdegeral
        //turno
        //metodos virtuais 
        //metodos abstratos
            
    }
    class normal: Fight
    {        
        //metodos override
    }
    class Boss: Fight
    {        
    }
    public class Program
    {
        public static void Main()
        {
            MonsterFactory oFactory = new MonsterFactory();
            Monster oZombie = oFactory.CreateMonster("Zombie", 1);
            Monster oSpider = oFactory.CreateMonster("GigantSpider", 1);
            oZombie.showStatus();
            oSpider.showStatus();
        }
    }
}