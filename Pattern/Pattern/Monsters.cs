/**
 * ..:: Library for Monsters ::..
 * 
 * @author Rafael Romeu
 * @author Gabriel Moraes 
 * 
 */
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Library
{

/** ..:: Factory Pattern ::.. 
 *
 */
    public class MonsterFactory
	{
		public Monster CreateMonster(string name, int level)
		{
			Monster oMonster = null;
			switch (name)
            {
                case "Gigant Spider":
                    oMonster = new GigantSpider(level);
                break;
                case "Zombie":
                    oMonster = new Zombie(level);
                break;
				case "Zombie Bear":
					oMonster = new ZombieBear(level);
					break;
				case "Big Foot":
					oMonster = new BigFoot(level);
                    break;
				case "Big Mosquito":
					oMonster = new BigMosquito(level);
                    break;
				case "Chupacabra":
					oMonster = new Chupacabra(level);
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
        protected string description;
        protected int numeroDeIdentificacao;
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
        public void setID(int ID)
        {
            numeroDeIdentificacao = ID;
        }
        public int getID()
        {
            return numeroDeIdentificacao;
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

	public class ZombieBear : Monster
    {
        public ZombieBear(int level)
        {
            name = "Zombie Bear";
            this.level = level;

            ATK = random.Next(10, 25) * level;
            DEF = random.Next(0, 20) * level;

            lifeSize = 60 * level;
            actualLife = lifeSize;

            ATKMode = false;
            DEFMode = false;
            RUNMode = false;
        }
    }

	public class BigFoot : Monster
    {
        public BigFoot(int level)
        {
            name = "Big Foot";
            this.level = level;

            ATK = random.Next(5, 30) * level;
            DEF = random.Next(0, 15) * level;

            lifeSize = 30 * level;
            actualLife = lifeSize;

            ATKMode = false;
            DEFMode = false;
            RUNMode = false;
        }
    }

	public class BigMosquito : Monster
    {
		public BigMosquito(int level)
        {
            name = "Big Mosquito";
            this.level = level;

            ATK = random.Next(20, 30) * level;
            DEF = random.Next(0, 5) * level;

            lifeSize = 10 * level;
            actualLife = lifeSize;

            ATKMode = false;
            DEFMode = false;
            RUNMode = false;
        }
    }

	public class Chupacabra : Monster
    {
		public Chupacabra(int level)
        {
            name = "Chupacabra";
            this.level = level;

            ATK = random.Next(20, 40) * level;
            DEF = random.Next(0, 5) * level;

            lifeSize = 30 * level;
            actualLife = lifeSize;

            ATKMode = false;
            DEFMode = false;
            RUNMode = false;
        }
    }
}