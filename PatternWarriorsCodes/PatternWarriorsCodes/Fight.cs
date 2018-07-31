/*
 * ..:: Library for Monsters and Fights ::..
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
using Library;
using HeroSpace;

namespace Library
{
/** ..:: Template Pattern ::.. 
 */
    public abstract class Fight
    {        
        protected Random random = new Random();
        protected Hero hero;
        protected List<Monster> EnemyList = new List<Monster>();
        int IndexMonster;
        
        public Fight(){}

        public virtual int startFight()
        {
			hero = new Mage("Jovinelson");
            hero.setCombo(0);
            createEnemies();

            while(true)
            {
                if (EnemyList.Count < 1)
                {
                    Console.WriteLine("You Win!");
                    return 0;
                }
                if (hero.getLife() < 1)
                {
                    Console.WriteLine("You Lose!");
                    return 1;
                }

				hero.showStatus();
                showEnemies();
                
                round();
                calculateDamage();
                removeDeads();
            }
        }
        public abstract void createEnemies();
        
        public virtual void showEnemies()
        {
            string[,] arrValues = new string[5, EnemyList.Count];

            foreach (Monster item in EnemyList)
            {
                arrValues[0, EnemyList.IndexOf(item)] = "Monster " + item.getID().ToString();                
            }

            foreach (Monster item in EnemyList)
            {
                arrValues[1, EnemyList.IndexOf(item)] = item.getName();                
            }
            foreach (Monster item in EnemyList)
            {
                arrValues[2, EnemyList.IndexOf(item)] = "Life " + item.getLife().ToString();                
            }
            foreach (Monster item in EnemyList)
            {
                arrValues[3, EnemyList.IndexOf(item)] = "ATK " + item.getATK().ToString();                
            }
            foreach (Monster item in EnemyList)
            {
                arrValues[4, EnemyList.IndexOf(item)] = "DEF " + item.getDEF().ToString();                
            }
            ArrayPrinter.PrintToConsole(arrValues);            
        }

        public virtual void round()
		{
            roundHero();
            foreach (Monster item in EnemyList)
            {
                roundEnemies(item);
            }
        }
        public virtual void roundHero()
        {
            Console.WriteLine("A to ATK");
            Console.WriteLine("D to DEF");
            string option = Console.ReadLine();
            
            
            if ( (option == "A") || (option == "a") )
            {
                hero.Attack();
				hero.setSpecAttack(hero.ImprimeAtaque());
                                
                bool check = false;
                while(check == false)
                {
                    Console.WriteLine("Select the monster to attack");
                    IndexMonster = Convert.ToInt32(Console.ReadLine());
                    foreach (Monster item in EnemyList)
                    {
                        if(item.getID() == IndexMonster)
                        {
                            check = true;
                        }        
                    }
                }
            }
            else
            {
                hero.Defence();
            }
        }

        public abstract void roundEnemies(Monster Enemy);
        
        public virtual void calculateDamage()
        {
            calculateDamageHero();
            foreach (Monster item in EnemyList)
            {
                calculateDamageEnemies(item);
            }
        }

        public virtual void calculateDamageHero()
        {
            Monster aux = null;

            if(hero.getMode().Equals("ATK"))
            {

                for(int i = 0; i < EnemyList.Count; i++)
                {
                    if(EnemyList[i].getID() == IndexMonster)
                    {
                        aux = EnemyList[i];
                    }
                }

                Console.Write("Voce atacou seu inimigo " + aux.getName() + " com toda sua força, ");

                if(aux.getMode().Equals("DEF")){
                    int dano = hero.getATK() - aux.getDEF();
                    aux.receiveDamage(dano);
                    if (dano > 0)
                    {
                        if(aux.getLife() > 0)
                        {
                            Console.WriteLine("ele defende seu ataque parcialmente e ganha uma nova cicatriz");
                            Console.WriteLine("Dano: " + dano);                        
                        }
                        else
                        {
                            Console.WriteLine("mesmo se defendendo ele não resiste ao ataque e morre");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ele defende completamente seu ataque!!");
                    }
                }
                else
                {
					if(hero.getCombo() == 3)
					{
						
						aux.receiveDamage(hero.getATK());
						Console.WriteLine("You used the combo! SUPER DAMAGE!!!");
						Console.WriteLine("Damage: " + hero.getATK());
						hero.setCombo(0);
					}
					else
					{
						
						aux.receiveDamage(hero.getATK());
                        Console.WriteLine("Great attack!");
						Console.WriteLine("Damage: " + hero.getATK());
						hero.addCombo();
                    }
                }
            }
            else
            {
                Console.WriteLine("Voce decide se defender nessa rodada");
            }
        }

        public virtual void calculateDamageEnemies(Monster Enemy)
        {
            if (Enemy.getLife() < 1){
            }
            else if(Enemy.getMode().Equals("RUN"))
            {
                Enemy.receiveDamage(Enemy.getLife());
                Console.WriteLine("Monster " + Enemy.getID() + " foge de voce");
            }
            else if (Enemy.getMode().Equals("ATK") ) 
            {
                Console.Write("Monster " + Enemy.getID() + " lhe ameaça com um poderoso golpe, ");

                if(hero.getMode().Equals("DEF"))
                {
                    int dano = Enemy.getATK() - hero.getDEF();
                    hero.receiveDamage(dano);
                    if (dano > 0)
                    {
                        Console.WriteLine("voce se defende mas mesmo assim recebe algum dano!!");
                        Console.WriteLine("Dano: " + dano);                        
                    }
                    else
                    {
                        Console.WriteLine("voce defende completamente o ataque!!");
                    }
                }
                else
                {
                    int dano = Enemy.getATK();
                    hero.receiveDamage(dano);
                    Console.WriteLine("o ataque lhe acerta com tudo!!");
                    Console.WriteLine("Dano: " + dano);                                            
                }
            }
            else if(Enemy.getMode().Equals("DEF"))
            {
                Console.WriteLine("Monster " + Enemy.getID() + " mantem a guarda alta");
            }
        }

        public virtual void removeDeads()
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                Monster aux = EnemyList[i];
                if (aux.getLife() < 1)
                {
                    EnemyList.Remove(aux);
                    i = -1;
                }
            }
        }    
    }

    public class normalFight: Fight
    {        
        public override void createEnemies()
        {
            MonsterFactory oFactory = new MonsterFactory();      
            Monster Enemy;
            int rand = random.Next(5,8);

            for (int i = 0; i < rand; i++)
            {
                Enemy = oFactory.CreateMonster("GigantSpider", 1);
                Enemy.setID(i);
                EnemyList.Add(Enemy);            
                System.Threading.Thread.Sleep(100);
            }
        }

        public override void roundEnemies(Monster Enemy)
        {
            int Mode = random.Next(0,100);
            int chanceATK = 33;
            int chanceDEF = 33;

            if (Enemy.getLife() >= Enemy.getLifeSize()*0.80)
            {
                chanceATK = 45;
                chanceDEF = 45;
            }
            else if (Enemy.getLife() <= Enemy.getLifeSize()*0.80)
            {
                chanceATK = 25;
                chanceDEF = 50;
            }
            else if (Enemy.getLife() <= Enemy.getLifeSize()*0.50)
            {
                chanceATK = 15;
                chanceDEF = 40;
            }
            else if (Enemy.getLife() <= Enemy.getLifeSize()*0.15)
            {
                chanceATK = 5;
                chanceDEF = 30;
            }

            if ( (0 < Mode) && (Mode <= chanceATK) )
            {
                Enemy.Attack();
            }
            else if( (chanceATK < Mode) && (Mode <= (chanceATK + chanceDEF)) )
            {
                Enemy.Defence();
            }
            else
            {
                Enemy.runAway();
            }
        }
    }
    public class Boss: Fight
    {   
        public override void createEnemies()
        {
            
        }   
        public override void roundEnemies(Monster Enemy)
        {
            int Mode = random.Next(0,100);
            int chanceATK = 33;

            if (Enemy.getLife() >= Enemy.getLifeSize()*0.80)
            {
                chanceATK = 75;
            }
            else if (Enemy.getLife() <= Enemy.getLifeSize()*0.80)
            {
                chanceATK = 50;
            }
            else if (Enemy.getLife() <= Enemy.getLifeSize()*0.50)
            {
                chanceATK = 25;
            }
            else if (Enemy.getLife() <= Enemy.getLifeSize()*0.15)
            {
                chanceATK = 10;
            }

            if ( (0 < Mode) && (Mode <= chanceATK) )
            {
                Enemy.Attack();
            }
            else if( (chanceATK < Mode) && (Mode <= 100 ) )
            {
                Enemy.Defence();
            }
        }
    }

	public class Program
    {
        public static void Main()
        {
            //MonsterFactory oFactory = new MonsterFactory();
            //Monster oZombie = oFactory.CreateMonster("Zombie", 1);
            //Monster oSpider = oFactory.CreateMonster("GigantSpider", 1);
            //oZombie.showStatus();
            //oSpider.showStatus();

            normalFight oFight = new normalFight();
            oFight.startFight();
            
        }
    }
}