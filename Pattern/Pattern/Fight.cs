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
		protected string monsterName;
        
        public Fight(){}

		public virtual int startFight(Ambient ambient)
        {
            hero.setCombo(0);
			createEnemies(ambient);

            while(true)
            {
                if (EnemyList.Count < 1)
                {
					library.slowWrite("You Win!", Constants.TEXT_SPEED1, true);
                    return 0;
                }
                if (hero.getLife() < 1)
                {
					library.slowWrite("You Lose!", Constants.TEXT_SPEED1, true);
                    return 1;
                }

				hero.showStatus();
                showEnemies();
                
                round();
                calculateDamage();
                removeDeads();
            }
        }
		public abstract void createEnemies(Ambient ambient);
        
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

				library.slowWrite("You have attacked your enemy " + aux.getName() + " with all your strenght! ", Constants.TEXT_SPEED1, false);

                if(aux.getMode().Equals("DEF")){
                    int dano = hero.getATK() - aux.getDEF();
                    aux.receiveDamage(dano);
                    if (dano > 0)
                    {
                        if(aux.getLife() > 0)
                        {
							library.slowWrite("He partially defends your attack and wins a beatiful scar.",Constants.TEXT_SPEED1,true);
							library.slowWrite("Damage given: " + dano, Constants.TEXT_SPEED1, true);                        
                        }
                        else
                        {
							library.slowWrite("He tries to defend himself, but doesn't resist and dies!", Constants.TEXT_SPEED1, true);
                        }
                    }
                    else
                    {
						library.slowWrite("He completely defend your attack!!!", Constants.TEXT_SPEED1, true);
                    }
                }
                else
                {
					if(hero.getCombo() == 3)
					{
						
						aux.receiveDamage(hero.getATK());
						library.slowWrite("You used the combo! SUPER DAMAGE!!!", Constants.TEXT_SPEED1, true);
						library.slowWrite("Damage given: " + hero.getATK(), Constants.TEXT_SPEED1, true);
						hero.setCombo(0);
					}
					else
					{
						
						aux.receiveDamage(hero.getATK());
						library.slowWrite("Great attack!", Constants.TEXT_SPEED1, true);
						library.slowWrite("Damage: " + hero.getATK(), Constants.TEXT_SPEED1, true);
						hero.addCombo();
                    }
                }
            }
            else
            {
				library.slowWrite("You decide to defend yourself this turn.", Constants.TEXT_SPEED1, true);
            }
        }

        public virtual void calculateDamageEnemies(Monster Enemy)
        {
            if (Enemy.getLife() < 1){
            }
            else if(Enemy.getMode().Equals("RUN"))
            {
                Enemy.receiveDamage(Enemy.getLife());
				library.slowWrite("Monster " + Enemy.getID() + " ran from you", Constants.TEXT_SPEED1, true);
            }
            else if (Enemy.getMode().Equals("ATK") ) 
            {
				library.slowWrite("Monster " + Enemy.getID() + " threatens you with a powerful hit! ", Constants.TEXT_SPEED1, true);

                if(hero.getMode().Equals("DEF"))
                {
                    int dano = Enemy.getATK() - hero.getDEF();
                    hero.receiveDamage(dano);
                    if (dano > 0)
                    {
						library.slowWrite("You defend yourself, turning the attack not so effective.", Constants.TEXT_SPEED1, true);
						library.slowWrite("Damage taken: " + dano, Constants.TEXT_SPEED1, true);                        
                    }
                    else
                    {
						library.slowWrite("You defend all the damage!!", Constants.TEXT_SPEED1, true);
                    }
                }
                else
                {
                    int dano = Enemy.getATK();
                    hero.receiveDamage(dano);
					library.slowWrite("The attack hit the bull's eye!!", Constants.TEXT_SPEED1, true);
					library.slowWrite("Damage taken: " + dano, Constants.TEXT_SPEED1, true);                                            
                }
            }
            else if(Enemy.getMode().Equals("DEF"))
            {
				library.slowWrite("Monster " + Enemy.getID() + " keeps on his guard", Constants.TEXT_SPEED1, true);
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
		public normalFight(string monster, Hero oHero)
		{
			monsterName = monster;
			hero = oHero;

		}
		public override void createEnemies(Ambient ambient)
		{
            MonsterFactory oFactory = new MonsterFactory();      
            Monster Enemy;
            int rand = random.Next(5,8);

            for (int i = 0; i < rand; i++)
            {
				string generatedMonster = ambient.getRandomMonster();

				Enemy = oFactory.CreateMonster(generatedMonster, 1);
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
    public class BossFight: Fight
    {   
		public BossFight(string monster, Hero oHero)
		{
			monsterName = monster;
			hero = oHero;
		}
		public override void createEnemies(Ambient ambient)
        {
			MonsterFactory oFactory = new MonsterFactory();      
			Monster Enemy;
			Enemy = oFactory.CreateMonster(monsterName, 5);
			Enemy.setID(0);
			EnemyList.Add(Enemy);            

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
}