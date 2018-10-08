/**
 * ..:: Library for Objects ::..
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
    public abstract class Upgrades
    {
        #region Declarations
        protected string name;
        protected string description;
        protected string type;
        protected int value;

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
        public string getType()
        {
            return type;;
        }
        
        public int getValue()
        {
            return value;
        }

        #endregion
    }

    public class Sword: Upgrades
    {
        public Sword()
        {
            name = "Sword upgrade";
            description = "Badass sword";
            type = "ATK";
            value = 15;
        }
    }
    public class Shield: Upgrades
    {
        public Shield()
        {
            name = "Shield upgrade";
            description = "Badass Shield";
            type = "DEF";
            value = 15;
        }
    }
    public class Potion: Upgrades
    {
        public Potion()
        {
            name = "Potion";
            description = "Potion of Life";
            type = "Life";
            value = 90;
        }
    }

    public class Stuff
    {
        #region Declarations
        protected Random random = new Random();
        protected string name;
        protected string description;
        protected List<String> ListaDeNomes = new List<string>();
        protected List<String> ListaDeDescricoes = new List<string>();

        #endregion

        #region Methods

        public Stuff()
        {
            ListaDeNomes.Add("Animal Morto");
            ListaDeDescricoes.Add("Parece que foi morto por um caçador");

            ListaDeNomes.Add("Cadeira Quebrada");
            ListaDeDescricoes.Add("Essa cadeira deve estar quebrada a anos");

            int i = random.Next(0,ListaDeNomes.Count);
            name = ListaDeNomes[i];
            description = ListaDeDescricoes[i];
        }
        
        public string getName()
        {
            return name;
        }
        public string getDescription()
        {
            return description;
        }

        #endregion
    }
}