/**
 * ..:: Library for Monsters and Fights ::..
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
        
        void setHero(bool Hero);
        
        bool getHero();
       
        int explorar();
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
        protected bool Hero;
        protected int acabou = 0;

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
        public virtual List<string> getMonsters()
        {
            return monstersList;
        }
        public virtual string getRandomMonster()
        {
            string monster = null;
            monster = monstersList[ random.Next(1,monstersList.Count) ];
            return monster;
        }
        public Upgrades getRandomUpgrade()
        {
            int index = rand.Next(0, ListaDeUpgrades.Count);
            return ListaDeUpgrades[index];
        }

        public Stuff getRandomStuff()
        {
            int index = rand.Next(0, ListaDeStuff.Count);
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
        public virtual void setHero(bool Hero)
        {
            this.Hero = Hero;
        }
        public virtual bool getHero()
        {
            return Hero;
        }
        
        public virtual int explorar()
        {
            if (boss == true)
            {
                return 1;
            }
            Console.WriteLine("Voce entrou na " + name);
            Console.WriteLine("Ao explorar o local voce se depara com: disparar eventos");
            
            bool sair = false;
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
                    acabou = AmbientList[option].explorar();
                    if(acabou == 1)
                    {
                        sair = true;
                    }
                }
            }

            return acabou;

        }

        #endregion
        
    }

    public class journey:Ambient
    {
        public journey()
        {
            Hero = false;
            boss = false;
            name = "journey";
            description = "journey DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
        public new int explorar()
        {
            Console.WriteLine("Viajando ...");
            return 0;
        }
    }
    public class EntradaDaFlorestaMaldita:Ambient
    {
        public EntradaDaFlorestaMaldita()
        {
            Hero = false;
            boss = false;
            name = "Entrada Da Floresta Maldita";
            description = "Entrada Da Floresta Maldita DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }
    public class Cave:Ambient
    {
        public Cave()
        {
            Hero = false;
            boss = false;
            name = "Cave";
            AmbientList.Add(new MatadouroDasAranhas());
            AmbientList.Add(new CorredorCaverna());
            description = "Cave DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }
    public class MatadouroDasAranhas:Ambient
    {
        public MatadouroDasAranhas()
        {
            Hero = false;
            boss = false;
            name = "MatadouroDasAranhas";
            description = "MatadouroDasAranhas DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }
    public class CorredorCaverna:Ambient
    {
        public CorredorCaverna()
        {
            Hero = false;
            boss = false;
            AmbientList.Add(new Tumulo());
            name = "CorredorCaverna";
            description = "Corredor Caverna DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }
    public class Tumulo:Ambient
    {
        public Tumulo()
        {
            Hero = false;
            boss = false;
            name = "Tumulo";
            description = "Tumulo DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }
    
    public class TreeOfLife:Ambient
    {
        public TreeOfLife()
        {
            Hero = false;
            boss = false;
            name = "TreeOfLife";
            description = "TreeOfLife DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }
    public class Swamp:Ambient
    {
        public Swamp()
        {
            Hero = false;
            boss = false;
            name = "Swamp";
            description = "Swamp Description";
            monstersList.Add("Gigant Spider");
        }
    }

    public class BosqueAssombrado:Ambient
    {
        public BosqueAssombrado()
        {
            Hero = false;
            boss = false;
            name = "BosqueAssombrado";
            description = "BosqueAssombrado DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }
    public class BigTree:Ambient
    {
        public BigTree()
        {
            Hero = false;
            boss = false;
            name = "BigTree";
            description = "BigTree DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }

    public class LagoSul:Ambient
    {
        public LagoSul()
        {
            Hero = false;
            boss = false;
            name = "LagoSul";
            description = "LagoSul DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }
    public class LagoNordeste:Ambient
    {
        public LagoNordeste()
        {
            Hero = false;
            boss = false;
            AmbientList.Add(new Ilha());
            name = "LagoNordeste";
            description = "LagoNordeste DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }
    public class Ilha:Ambient
    {
        public Ilha()
        {
            Hero = false;
            boss = false;
            name = "Ilha";
            description = "Ilha DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }
    
    public class CabanaAbandonada:Ambient
    {
        public CabanaAbandonada()
        {
            Hero = false;
            boss = false;
            name = "CabanaAbandonada";
            AmbientList.Add(new Sala());

            description = "CabanaAbandonada DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }
    public class Sala:Ambient
    {
        public Sala()
        {
            Hero = false;
            boss = false;
            name = "Sala";
            AmbientList.Add(new Escadas());
            AmbientList.Add(new Corredor());
            AmbientList.Add(new Porao());
            description = "Sala DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }
    public class Corredor:Ambient
    {
        public Corredor()
        {
            Hero = false;
            boss = false;
            name = "Corredor";
            AmbientList.Add(new Banheiro());
            AmbientList.Add(new Cozinha());
            AmbientList.Add(new Quarto());
            description = "Corredor DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }
    public class Porao:Ambient
    {
        public Porao()
        {
            Hero = false;
            boss = false;
            name = "Porao";
            description = "Porao DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }
    public class Escadas:Ambient
    {
        public Escadas()
        {
            Hero = false;
            boss = false;
            name = "Escadas";
            AmbientList.Add(new Sotao());
            description = "Escadas DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }
    public class Sotao:Ambient
    {
        public Sotao()
        {
            Hero = false;
            boss = false;
            name = "Sotao";
            description = "Sotao DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }
    public class Cozinha:Ambient
    {
        public Cozinha()
        {
            Hero = false;
            boss = false;
            name = "Cozinha";
            description = "Cozinha DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }

    public class Quarto:Ambient
    {
        public Quarto()
        {
            Hero = false;
            boss = false;
            name = "Quarto";
            description = "Quarto DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }

    public class Banheiro:Ambient
    {
        public Banheiro()
        {
            Hero = false;
            boss = false;
            name = "Banheiro";
            description = "Banheiro DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }

    public class Acampamento:Ambient
    {
        public Acampamento()
        {
            Hero = false;
            boss = false;
            name = "Acampamento";
            AmbientList.Add(new Barraca());
            description = "Acampamento DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }
    public class Barraca:Ambient
    {
        public Barraca()
        {
            Hero = false;
            boss = false;
            name = "Barraca";
            description = "Barraca DESCRIPTION";
            monstersList.Add("Gigant Spider");
        }
    }
    
}