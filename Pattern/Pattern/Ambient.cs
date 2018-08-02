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

        protected Random random = new Random();

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
				monster = monstersList [random.Next (0, monstersList.Count)];
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
            int index = random.Next(0, ListaDeUpgrades.Count);
            return ListaDeUpgrades[index];
        }

        public Stuff getRandomStuff()
        {
			if (ListaDeStuff.Count == 0) 
			{
				return null;
			}
            int index = random.Next(0, ListaDeStuff.Count);
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
                int index = random.Next(-1,AmbientList.Count);
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
			int rand = random.Next (0, 2);
			string monster = getRandomMonster();
			if (boss == true) 
			{
				BossFight oFight = new BossFight (monster, oHero);
				oFight.startFight ();

				if (oHero.getLife () > 0) 
				{
					oHero.setPergaminho (true);
				}
			}
			else if ( (monster != null) ) 
			{
				if (rand == 0) 
				{
					Console.WriteLine ("Ao explorar o local voce se depara com " + monster);
					normalFight oFight = new normalFight (monster, oHero);
					oFight.startFight ();
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
            Console.WriteLine("Voce entrou na " + name);
			bool sair = battle ();
			//sortear uns objetos

			while(sair == false)
            {
                Console.WriteLine("Suas opção são: ");
                int i = 0;
                foreach (Ambient item in AmbientList)
                {
                    Console.Write(i + " - ");
                    Console.WriteLine("Explorar " + item.getName());
                    i++;
                }
                Console.WriteLine(i+ " - Sair");
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
            monstersList.Add("GigantSpider");
        }
		public override bool battle()
		{
			int rand = random.Next (0, 2);
			string monster = getRandomMonster();
			if (monster != null) 
			{
				if (rand == 0) {
					Console.WriteLine ("Em meio a sua viagem você é cercado pelos inimigos");				
					normalFight oFight = new normalFight (monster, oHero);
					oFight.startFight ();

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
            Console.WriteLine("Viajando ...");
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
            name = "Entrada Da Floresta Maldita";
            description = "Entrada Da Floresta Maldita DESCRIPTION";
            monstersList.Add("GigantSpider");
			monstersList.Add ("Zombie");
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
            description = "Cave DESCRIPTION";
            //monstersList.Add("GigantSpider");
        }
    }
    public class MatadouroDasAranhas:Ambient
    {
        public MatadouroDasAranhas()
        {
            oHero = null;
            boss = false;
            name = "MatadouroDasAranhas";
            description = "MatadouroDasAranhas DESCRIPTION";
            //monstersList.Add("GigantSpider");
        }
    }
    public class CorredorCaverna:Ambient
    {
        public CorredorCaverna()
        {
            oHero = null;
            boss = false;
            AmbientList.Add(new Tumulo());
            name = "CorredorCaverna";
            description = "Corredor Caverna DESCRIPTION";
            //monstersList.Add("GigantSpider");
        }
    }
    public class Tumulo:Ambient
    {
        public Tumulo()
        {
            oHero = null;
            boss = false;
            name = "Tumulo";
            description = "Tumulo DESCRIPTION";
            monstersList.Add("Zombie");
        }
    }
    
    public class TreeOfLife:Ambient
    {
        public TreeOfLife()
        {
            oHero = null;
            boss = false;
            name = "TreeOfLife";
            description = "TreeOfLife DESCRIPTION";
            monstersList.Add("GigantSpider");
        }
    }
    public class Swamp:Ambient
    {
        public Swamp()
        {
            oHero = null;
            boss = false;
            name = "Swamp";
            description = "Swamp Description";
            monstersList.Add("GigantSpider");
        }
    }

    public class BosqueAssombrado:Ambient
    {
        public BosqueAssombrado()
        {
            oHero = null;
            boss = false;
            name = "BosqueAssombrado";
            description = "BosqueAssombrado DESCRIPTION";
            monstersList.Add("GigantSpider");
        }
    }
    public class BigTree:Ambient
    {
        public BigTree()
        {
            oHero = null;
            boss = false;
            name = "BigTree";
            description = "BigTree DESCRIPTION";
            monstersList.Add("GigantSpider");
        }
    }

    public class LagoSul:Ambient
    {
        public LagoSul()
        {
            oHero = null;
            boss = false;
            name = "LagoSul";
            description = "LagoSul DESCRIPTION";
            monstersList.Add("GigantSpider");
        }
    }
    public class LagoNordeste:Ambient
    {
        public LagoNordeste()
        {
            oHero = null;
            boss = false;
            AmbientList.Add(new Ilha());
            name = "LagoNordeste";
            description = "LagoNordeste DESCRIPTION";
            monstersList.Add("GigantSpider");
        }
    }
    public class Ilha:Ambient
    {
        public Ilha()
        {
            oHero = null;
            boss = false;
            name = "Ilha";
            description = "Ilha DESCRIPTION";
            monstersList.Add("GigantSpider");
        }
    }
    
    public class CabanaAbandonada:Ambient
    {
        public CabanaAbandonada()
        {
            oHero = null;
            boss = false;
            name = "CabanaAbandonada";
            AmbientList.Add(new Sala());

            description = "CabanaAbandonada DESCRIPTION";
            monstersList.Add("GigantSpider");
        }
    }
    public class Sala:Ambient
    {
        public Sala()
        {
            oHero = null;
            boss = false;
            name = "Sala";
            AmbientList.Add(new Escadas());
            AmbientList.Add(new Corredor());
            AmbientList.Add(new Porao());
            description = "Sala DESCRIPTION";
            monstersList.Add("GigantSpider");
        }
    }
    public class Corredor:Ambient
    {
        public Corredor()
        {
            oHero = null;
            boss = false;
            name = "Corredor";
            AmbientList.Add(new Banheiro());
            AmbientList.Add(new Cozinha());
            AmbientList.Add(new Quarto());
            description = "Corredor DESCRIPTION";
            monstersList.Add("GigantSpider");
        }
    }
    public class Porao:Ambient
    {
        public Porao()
        {
            oHero = null;
            boss = false;
            name = "Porao";
            description = "Porao DESCRIPTION";
            monstersList.Add("GigantSpider");
        }
    }
    public class Escadas:Ambient
    {
        public Escadas()
        {
            oHero = null;
            boss = false;
            name = "Escadas";
            AmbientList.Add(new Sotao());
            description = "Escadas DESCRIPTION";
            monstersList.Add("GigantSpider");
        }
    }
    public class Sotao:Ambient
    {
        public Sotao()
        {
            oHero = null;
            boss = false;
            name = "Sotao";
            description = "Sotao DESCRIPTION";
            monstersList.Add("GigantSpider");
        }
    }
    public class Cozinha:Ambient
    {
        public Cozinha()
        {
            oHero = null;
            boss = false;
            name = "Cozinha";
            description = "Cozinha DESCRIPTION";
            monstersList.Add("GigantSpider");
        }
    }

    public class Quarto:Ambient
    {
        public Quarto()
        {
            oHero = null;
            boss = false;
            name = "Quarto";
            description = "Quarto DESCRIPTION";
            monstersList.Add("GigantSpider");
        }
    }

    public class Banheiro:Ambient
    {
        public Banheiro()
        {
            oHero = null;
            boss = false;
            name = "Banheiro";
            description = "Banheiro DESCRIPTION";
            monstersList.Add("GigantSpider");
        }
    }

    public class Acampamento:Ambient
    {
        public Acampamento()
        {
            oHero = null;
            boss = false;
            name = "Acampamento";
            AmbientList.Add(new Barraca());
            description = "Acampamento DESCRIPTION";
            monstersList.Add("GigantSpider");
        }
    }
    public class Barraca:Ambient
    {
        public Barraca()
        {
            oHero = null;
            boss = false;
            name = "Barraca";
            description = "Barraca DESCRIPTION";
            monstersList.Add("GigantSpider");
        }
    }
    
}