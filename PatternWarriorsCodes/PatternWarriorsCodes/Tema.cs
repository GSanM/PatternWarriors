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

namespace Library
{
    
    public abstract class Tema
    {
        /*
         * Lista de Ambientes
         * Nome
         * Descrição
         * Pergaminho
         * Objetos
         */
        #region Declarations
        protected Random rand = new Random();

        protected string name;
        protected string description;

        public Ambient journey = new journey();
        protected List<Upgrades> ListaDeUpgrades;
        protected List<Stuff> ListaDeStuff;
        public List<Ambient> ListaDeAmbientes;
        //BOSS

        #endregion

        #region Methods
        
        public string getName()
        {
            return name;
        }
        public string getDescription()
        {
            return description;
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
        public int getTamanhoDaListaDeAmbientes()
        {
            return ListaDeAmbientes.Count;
        }

        #endregion

    }

    public class Forest: Tema
    {
        public Forest()
        {
            name = "Forest";
            description = "FOREST DESCRIPTION";
            Pergaminho = null;
            ListaDeUpgrades = new List<Upgrades>();
            ListaDeUpgrades.Add( new Sword());
            ListaDeUpgrades.Add( new Shield());
            ListaDeUpgrades.Add( new Potion());

            ListaDeStuff = new List<Stuff>();
            ListaDeStuff.Add( new Stuff());
            ListaDeStuff.Add( new Stuff());

            ListaDeAmbientes = new List<Ambient>();
            ListaDeAmbientes.Add(new EntradaDaFlorestaMaldita());
            ListaDeAmbientes.Add(new Cave());
            ListaDeAmbientes.Add(new TreeOfLife());
            ListaDeAmbientes.Add(new BigTree());
            ListaDeAmbientes.Add(new BosqueAssombrado());
            ListaDeAmbientes.Add(new CabanaAbandonada());
            ListaDeAmbientes.Add(new Acampamento());
            ListaDeAmbientes.Add(new Tumulo());
            ListaDeAmbientes.Add(new Swamp());
            ListaDeAmbientes.Add(new LagoNordeste());
            ListaDeAmbientes.Add(new LagoSul());
        }
    }
}