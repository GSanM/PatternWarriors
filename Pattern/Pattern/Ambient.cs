/**
 * ..:: Library for Ambient ::..
 * 
 * @author Rafael Romeu
 * @author Gabriel Moraes 
 * 
 * 
 */
using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Library;
using HeroSpace;

namespace Library
{
    
/** ..:: Composite Pattern ::.. 
*
*/
    interface Icomposite
    {
        string getName();
        
        List<string> getMonsters();
        
        string getRandomMonster();
        
        void setBoss(bool Hero);
        
        bool getBoss();
        
        void setHero(Hero oHero);
        
		Hero getHero();
       
		Hero explorar(Hero oHero);
    }
    public abstract class Ambient: Icomposite
    {
		/*
         * Lista de Monstros
         * Boss
         * Ambientes internos (Composite)
         * Fights
         * Nome
         * Descrição
         * Objetos
         */
		#region Declarations

        //Singleton para numero randomico
		private static Random random = null;
        private static readonly object lock_ = new object();

        public static Random Random_
        {
            get
            {
                lock (lock_)
                {
                    if (random == null)
                    {
                        random = new Random(1);
                    }
                    return random;
                }
            }
        }

        protected string name;
        protected string description;
        protected bool boss;
		protected Hero oHero;
		protected bool acabou = false;

        protected List<string> monstersList = new List<string>();
        protected List<Ambient> AmbientList = new List<Ambient>();
        protected List<Upgrades> ListaDeUpgrades = new List<Upgrades>();
        protected List<Stuff> ListaDeStuff = new List<Stuff>();

        #endregion

        #region Methods

        public virtual string getName()
        {
            return name;
        }
		public virtual string getDescription()
		{
			return description;
		}
        public virtual List<string> getMonsters()
        {
            return monstersList;
        }
        public virtual string getRandomMonster()
        {
			if (monstersList.Count != 0) {
				string monster = null;
				monster = monstersList [Random_.Next (0, monstersList.Count)];
				return monster;
			} 
			else
			{
				return null;
			}
        }
        public Upgrades getRandomUpgrade()
        {
			if (ListaDeUpgrades.Count == 0) 
			{
				return null;
			}
            int index = Random_.Next(0, ListaDeUpgrades.Count);
            return ListaDeUpgrades[index];
        }

        public Stuff getRandomStuff()
        {
			if (ListaDeStuff.Count == 0) 
			{
				return null;
			}
            int index = Random_.Next(0, ListaDeStuff.Count);
            return ListaDeStuff[index];
        }
        public virtual void setBoss(bool boss)
        {
            if(AmbientList.Count == 0)
            {
                this.boss = boss;
            }
            else
            {
                int index = Random_.Next(-1,AmbientList.Count);
                if(index == -1)
                {
                    this.boss = boss;
                }
                else
                {
                    AmbientList[index].setBoss(boss);
                }
            }
        }
        public virtual bool getBoss()
        {
            return boss;
        }
		public virtual void setHero(Hero oHero)
        {
            this.oHero = oHero;
        }
		public virtual Hero getHero()
        {
            return oHero;
        }
		public virtual bool battle()
		{
			int rand = Random_.Next (0, 2);
			string monster = getRandomMonster();
			if (boss == true) 
			{
				BossFight oFight = new BossFight (monster, oHero);
				oFight.startFight (this);

				if (oHero.getLife () > 0) 
				{
					oHero.setPergaminho (true);
				}
			}
			else if ( (monster != null) ) 
			{
				if (rand == 0) 
				{
					Console.WriteLine ("Exploring the place you see a group of monsters");
					normalFight oFight = new normalFight (monster, oHero);
					oFight.startFight (this);
					if (oHero.getLife () < 1) 
					{
						return true;
					} 
					else 
					{
						//sortear uns objetos
					}
				}
			}
			return false;
		}
		public virtual Hero explorar(Hero oHero)
        {
			this.oHero = oHero;
            Console.WriteLine("You entered at " + name);
			bool sair = battle ();
			//sortear uns objetos

			while(sair == false)
            {
                Console.WriteLine("Your options: ");
                int i = 0;
                foreach (Ambient item in AmbientList)
                {
                    Console.Write(i + " - ");
                    Console.WriteLine("Explore " + item.getName());
                    i++;
                }
                Console.WriteLine(i+ " - Leave");
                int option = Convert.ToInt32(Console.ReadLine());
                if(option == i)
                {
                    sair = true;
                }
                else
                {
					this.oHero = AmbientList[option].explorar(oHero);
					if( (this.oHero.getLife () <= 0) || (this.oHero.getPergaminho() == true) )
					{
						return this.oHero;
					}
					if(acabou == true)
                    {
                        sair = true;
                    }
                }
            }

			return this.oHero;

        }

        #endregion
        
    }

    public class journey:Ambient
    {
        public journey()
        {
            oHero = null;
            boss = false;
            name = "journey";
            description = "journey DESCRIPTION";
			monstersList.Add("Giant Spider");
            monstersList.Add("Zombie");
            monstersList.Add("Zombie Bear");
			monstersList.Add("Big Mosquito");
			monstersList.Add("Big Foot");
			monstersList.Add("Chupacabra");
        }
		public override bool battle()
		{
			int rand = Random_.Next (0, 2);
			string monster = getRandomMonster();
			if (monster != null) 
			{
				if (rand == 0) {
					Console.WriteLine ("You got surrounded by enemies!");				
					normalFight oFight = new normalFight (monster, oHero);
					oFight.startFight (this);

					if (oHero.getLife () < 1) 
					{
						return true;
					}
				}
			}

			return false;
		}
		public override Hero explorar(Hero oHero)
        {
			this.oHero = oHero;
            Console.WriteLine("Traveling ...");
			battle ();

			return this.oHero;
        }
    }
    public class EntradaDaFlorestaMaldita:Ambient
    {
        public EntradaDaFlorestaMaldita()
        {
            oHero = null;
            boss = false;
            name = "Cursed Forest Entry";
			description = "A dark entry to a cursed forest.";
			monstersList.Add("Giant Spider");
            monstersList.Add("Zombie");
            monstersList.Add("Zombie Bear");
            monstersList.Add("Big Mosquito");
            monstersList.Add("Big Foot");
            monstersList.Add("Chupacabra");
        }
    }
    public class Cave:Ambient
    {
        public Cave()
        {
            oHero = null;
            boss = false;
            name = "Cave";
            AmbientList.Add(new MatadouroDasAranhas());
            AmbientList.Add(new CorredorCaverna());
            description = "A deep cave with powerful enemies inside.";
			monstersList.Add("Giant Spider");
            monstersList.Add("Zombie");
            monstersList.Add("Zombie Bear");
        }
    }
    public class MatadouroDasAranhas:Ambient
    {
        public MatadouroDasAranhas()
        {
            oHero = null;
            boss = false;
			name = "Spider's Slaughterhouse";
			description = "I would not get in there. Spiders are already creepy, imagine giant ones.";
			monstersList.Add("Giant Spider");
        }
    }
    public class CorredorCaverna:Ambient
    {
        public CorredorCaverna()
        {
            oHero = null;
            boss = false;
            AmbientList.Add(new Tumulo());
            name = "Cave's Corridor";
            description = "As dark as the rest of the cave.";
			monstersList.Add("Giant Spider");
            monstersList.Add("Big Mosquito");
            monstersList.Add("Big Foot");
            monstersList.Add("Chupacabra");
        }
    }
    public class Tumulo:Ambient
    {
        public Tumulo()
        {
            oHero = null;
            boss = false;
            name = "Grave";
            description = "I will not doubt of something getting out of there.";
            monstersList.Add("Zombie");
            monstersList.Add("Zombie Bear");
        }
    }
    
    public class TreeOfLife:Ambient
    {
        public TreeOfLife()
        {
            oHero = null;
            boss = false;
            name = "Tree Of Life";
			description = "It is strange a lot of monsters on a Life Tree;";
			monstersList.Add("Giant Spider");
            monstersList.Add("Big Mosquito");
            monstersList.Add("Big Foot");
        }
    }
    public class Swamp:Ambient
    {
        public Swamp()
        {
            oHero = null;
            boss = false;
            name = "Swamp";
            description = "Watch out! You don't know what could appear.";
            monstersList.Add("Big Foot");
            monstersList.Add("Chupacabra");
        }
    }

    public class BosqueAssombrado:Ambient
    {
        public BosqueAssombrado()
        {
            oHero = null;
            boss = false;
            name = "Cursed Grove";
            description = "Is this the forest? I think i'm lost.";
			monstersList.Add("Giant Spider");
            monstersList.Add("Zombie Bear");
            monstersList.Add("Big Mosquito");
            monstersList.Add("Big Foot");
        }
    }
    public class BigTree:Ambient
    {
        public BigTree()
        {
            oHero = null;
            boss = false;
            name = "Big Tree";
            description = "You can find big monsters in here.";
            monstersList.Add("Big Mosquito");
            monstersList.Add("Big Foot");
        }
    }

    public class LagoSul:Ambient
    {
        public LagoSul()
        {
            oHero = null;
            boss = false;
            name = "South Lake";
            description = "How much creatures can live on a lake?";
			monstersList.Add("Big Mosquito");
            monstersList.Add("Chupacabra");
        }
    }
    public class LagoNordeste:Ambient
    {
        public LagoNordeste()
        {
            oHero = null;
            boss = false;
            AmbientList.Add(new Ilha());
            name = "Northeast Lake";
            description = "A big lake with more than you can imagine in it.";
			monstersList.Add("Giant Spider");
            monstersList.Add("Zombie");
            monstersList.Add("Big Foot");
        }
    }
    public class Ilha:Ambient
    {
        public Ilha()
        {
            oHero = null;
            boss = false;
            name = "Island";
            description = "Can you live at an island with Chupacabra?";
			monstersList.Add("Giant Spider");
            monstersList.Add("Chupacabra");
        }
    }
    
    public class CabanaAbandonada:Ambient
    {
        public CabanaAbandonada()
        {
            oHero = null;
            boss = false;
            name = "Abandoned Shack";
            AmbientList.Add(new Sala());

            description = "Why someone abandoned this?";
			monstersList.Add("Giant Spider");
            monstersList.Add("Zombie");
            monstersList.Add("Zombie Bear");
            monstersList.Add("Big Mosquito");        }
    }
    public class Sala:Ambient
    {
        public Sala()
        {
            oHero = null;
            boss = false;
            name = "Room";
            AmbientList.Add(new Escadas());
            AmbientList.Add(new Corredor());
            AmbientList.Add(new Porao());
            description = "A common room.";
			monstersList.Add("Giant Spider");
            monstersList.Add("Zombie");
        }
    }
    public class Corredor:Ambient
    {
        public Corredor()
        {
            oHero = null;
            boss = false;
            name = "Corridor";
            AmbientList.Add(new Banheiro());
            AmbientList.Add(new Cozinha());
            AmbientList.Add(new Quarto());
            description = "Dark corridor. You can barely see.";
			monstersList.Add("Giant Spider");
            monstersList.Add("Zombie");
            monstersList.Add("Big Mosquito");
        }
    }
    public class Porao:Ambient
    {
        public Porao()
        {
            oHero = null;
            boss = false;
            name = "Basement";
            description = "Are you brave enough to enter there?";
			monstersList.Add("Giant Spider");
            monstersList.Add("Zombie");
            monstersList.Add("Big Mosquito");
        }
    }
    public class Escadas:Ambient
    {
        public Escadas()
        {
            oHero = null;
            boss = false;
            name = "Stairs";
            AmbientList.Add(new Sotao());
            description = "A long flight of stairs.";
			monstersList.Add("Giant Spider");
            monstersList.Add("Zombie");
            monstersList.Add("Zombie Bear");
            monstersList.Add("Big Mosquito");
        }
    }
    public class Sotao:Ambient
    {
        public Sotao()
        {
            oHero = null;
            boss = false;
            name = "Attic";
            description = "Strange things happen at an attic.";
			monstersList.Add("Giant Spider");
            monstersList.Add("Zombie");
            monstersList.Add("Zombie Bear");
            monstersList.Add("Big Mosquito");
        }
    }
    public class Cozinha:Ambient
    {
        public Cozinha()
        {
            oHero = null;
            boss = false;
            name = "Kitchen";
            description = "You can cook on the kitchen. Or you can be cooked...";
			monstersList.Add("Giant Spider");
            monstersList.Add("Zombie");
            monstersList.Add("Zombie Bear");
            monstersList.Add("Big Mosquito");
            monstersList.Add("Chupacabra");
        }
    }

    public class Quarto:Ambient
    {
        public Quarto()
        {
            oHero = null;
            boss = false;
            name = "Bedroom";
            description = "Let's sleep?";
			monstersList.Add("Giant Spider");
            monstersList.Add("Zombie");
            monstersList.Add("Big Mosquito");
            monstersList.Add("Big Foot");
            monstersList.Add("Chupacabra");
        }
    }

    public class Banheiro:Ambient
    {
        public Banheiro()
        {
            oHero = null;
            boss = false;
            name = "Bathroom";
			description = "Oh you definitely got to pee.";
			monstersList.Add("Giant Spider");
            monstersList.Add("Big Mosquito");
            monstersList.Add("Big Foot");
            monstersList.Add("Chupacabra");
        }
    }

    public class Acampamento:Ambient
    {
        public Acampamento()
        {
            oHero = null;
            boss = false;
            name = "Camp";
            AmbientList.Add(new Barraca());
            description = "You saw that?";
			monstersList.Add("Giant Spider");
            monstersList.Add("Big Foot");
            monstersList.Add("Chupacabra");
        }
    }
    public class Barraca:Ambient
    {
        public Barraca()
        {
            oHero = null;
            boss = false;
            name = "Tent";
            description = "Are you sure you will open this tent?";
			monstersList.Add("Giant Spider");
            monstersList.Add("Zombie");
            monstersList.Add("Zombie Bear");
            monstersList.Add("Big Mosquito");
            monstersList.Add("Big Foot");
            monstersList.Add("Chupacabra");
        }
    }
    
}