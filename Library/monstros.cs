/**
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

namespace Library
{

    class ArrayPrinter
    {
    #region Declarations

    static bool isLeftAligned = false;
    const string cellLeftTop = "┌";
    const string cellRightTop = "┐";
    const string cellLeftBottom = "└";
    const string cellRightBottom = "┘";
    const string cellHorizontalJointTop = "┬";
    const string cellHorizontalJointbottom = "┴";
    const string cellVerticalJointLeft = "├";
    const string cellTJoint = "┼";
    const string cellVerticalJointRight = "┤";
    const string cellHorizontalLine = "─";
    const string cellVerticalLine = "│";

    #endregion

    #region Private Methods

    private static int GetMaxCellWidth(string[,] arrValues)
    {
        int maxWidth = 1;

        for (int i = 0; i < arrValues.GetLength(0); i++)
        {
            for (int j = 0; j < arrValues.GetLength(1); j++)
            {
                int length = arrValues[i, j].Length;
                if (length > maxWidth)
                {
                    maxWidth = length;
                }
            }
        }

        return maxWidth;
    }

    private static string GetDataInTableFormat(string[,] arrValues)
    {
        string formattedString = string.Empty;

        if (arrValues == null)
            return formattedString;

        int dimension1Length = arrValues.GetLength(0);
        int dimension2Length = arrValues.GetLength(1);

        int maxCellWidth = GetMaxCellWidth(arrValues);
        int indentLength = (dimension2Length * maxCellWidth) + (dimension2Length - 1);
        //printing top line;
        formattedString = string.Format("{0}{1}{2}{3}", cellLeftTop, Indent(indentLength), cellRightTop, System.Environment.NewLine);

        for (int i = 0; i < dimension1Length; i++)
        {
            string lineWithValues = cellVerticalLine;
            string line = cellVerticalJointLeft;
            for (int j = 0; j < dimension2Length; j++)
            {
                string value = (isLeftAligned) ? arrValues[i, j].PadRight(maxCellWidth, ' ') : arrValues[i, j].PadLeft(maxCellWidth, ' ');
                lineWithValues += string.Format("{0}{1}", value, cellVerticalLine);
                line += Indent(maxCellWidth);
                if (j < (dimension2Length - 1))
                {
                    line += cellTJoint;
                }
            }
            line += cellVerticalJointRight;
            formattedString += string.Format("{0}{1}", lineWithValues, System.Environment.NewLine);
            if (i < (dimension1Length - 1))
            {
                formattedString += string.Format("{0}{1}", line, System.Environment.NewLine);
            }
        }

        //printing bottom line
        formattedString += string.Format("{0}{1}{2}{3}", cellLeftBottom, Indent(indentLength), cellRightBottom, System.Environment.NewLine);
        return formattedString;
    }

    private static string Indent(int count)
    {
        return string.Empty.PadLeft(count, '─');                 
    }

    #endregion

    #region Public Methods

    public static void PrintToStream(string[,] arrValues, StreamWriter writer)
    {
        if (arrValues == null)
            return;

        if (writer == null)
            return;

        writer.Write(GetDataInTableFormat(arrValues));
    }

    public static void PrintToConsole(string[,] arrValues)
    {
        if (arrValues == null)
            return;

        Console.WriteLine(GetDataInTableFormat(arrValues));
    }
    #endregion
    }

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
        protected Monster hero;
        protected List<Monster> EnemyList = new List<Monster>();
        int IndexMonster;
        
        public Fight(){}

        public virtual int startFight()
        {
            createEnemies();

            while(true)
            {
                if (EnemyList.Count < 1)
                {
                    Console.WriteLine("You Are The Winner");
                    return 0;
                }
                if (hero.getLife() < 1)
                {
                    Console.WriteLine("se fudeu");
                    return 1;
                }

                showHero();
                showEnemies();
                
                round();
                calculateDamage();
                removeDeads();
            }
        }
        public abstract void createEnemies();

        public virtual void showHero()
        {
        //    Console.Clear();
            string[,] arrValues = new string[4, 1];
            
            arrValues[0, 0] = "Hero " + hero.getName();
            arrValues[1, 0] = "Life " + hero.getLife().ToString();               
            arrValues[2, 0] = "ATK " + hero.getATK().ToString();  
            arrValues[3, 0] = "ATK " + hero.getDEF().ToString();                 

            ArrayPrinter.PrintToConsole(arrValues);
        }

        public virtual void showEnemies()
        {
            string[,] arrValues = new string[5, EnemyList.Count];

            foreach (Monster item in EnemyList)
            {
                arrValues[0, EnemyList.IndexOf(item)] = "Monster " + EnemyList.IndexOf(item).ToString();                
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
            Console.ReadLine();
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
            
            if(option == "A")
            {
                hero.Attack();

                Console.WriteLine("Select the monster to attack");
                IndexMonster = Convert.ToInt32(Console.ReadLine());
            }
            else
            {
                hero.Defence();
            }
        }

        public virtual void roundEnemies(Monster Enemy)
        {
            int Mode = random.Next(0,3);
            if (Mode == 0)
            {
                Enemy.Attack();
            }
            else if(Mode == 1)
            {
                Enemy.Defence();
            }
            else
            {
                Enemy.runAway();
            }
        }
        
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
            if(hero.getMode().Equals("ATK"))
            {
                Monster aux = EnemyList[IndexMonster];

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
                    aux.receiveDamage(hero.getATK());
                    Console.WriteLine("seu ataque pega em cheio!!");
                    Console.WriteLine("Dano: " + hero.getATK());                        
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
                Console.WriteLine("Monster " + EnemyList.IndexOf(Enemy) + " foge de voce");
            }
            else if (Enemy.getMode().Equals("ATK") ) 
            {
                Console.Write("Monster " + EnemyList.IndexOf(Enemy) + " lhe ameaça com um poderoso golpe, ");

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
                Console.WriteLine("Monster " + EnemyList.IndexOf(Enemy) + " mantem a guarda alta");
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

    class normalFight: Fight
    {        
        public override void createEnemies()
        {
            MonsterFactory oFactory = new MonsterFactory();
            hero = oFactory.CreateMonster("Zombie", 3);
            Monster Enemy;
            int rand = random.Next(5,8);

            for (int i = 0; i < rand; i++)
            {
                Enemy = oFactory.CreateMonster("GigantSpider", 1);
                EnemyList.Add(Enemy);            
                System.Threading.Thread.Sleep(100);
            }
        }
    }
    class Boss: Fight
    {   
        public override void createEnemies()
        {
            
        }     
    }
    public class Program
    {
        public static void Main()
        {
            MonsterFactory oFactory = new MonsterFactory();
            Monster oZombie = oFactory.CreateMonster("Zombie", 1);
            Monster oSpider = oFactory.CreateMonster("GigantSpider", 1);
            //oZombie.showStatus();
            //oSpider.showStatus();

            normalFight oFight = new normalFight();
            oFight.startFight();
            

        }
    }
}