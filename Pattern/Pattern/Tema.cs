/**
 * ..:: Library for temas ::..
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