/**
 * ..:: Library for stages ::..
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
using HeroSpace;
using Library;

namespace Library
{
    class Vertex<T>
    {

        List<Vertex<T>> neighbors;  
        T value;
        bool isVisited;
        Ambient oAmbient;

        public List<Vertex<T>> Neighbors { get { return neighbors; } set { neighbors = value; } }
        public T Value { get { return value; } set { this.value = value; } }
        public bool IsVisited { get { return isVisited; } set { isVisited = value; } }
        public int NeighborsCount { get { return neighbors.Count; } }

        public void setAmbient (Ambient oAmbient)
        {
            this.oAmbient = oAmbient;
        }
        public Ambient getAmbient()
        {
            return oAmbient;
        }

        public Vertex(T value)
        {
            this.value = value;
            isVisited = false;
            neighbors = new List<Vertex<T>>();
        }

        public Vertex(T value, List<Vertex<T>> neighbors)
        {
            this.value = value;
            isVisited = false;
            this.neighbors = neighbors;
        }

        public void Visit()
        {
            isVisited = true;
        }

        public void AddEdge(Vertex<T> vertex)
        {
            neighbors.Add(vertex);
        }

        public void AddEdges(List<Vertex<T>> newNeighbors)
        {
            neighbors.AddRange(newNeighbors);
        }

        public void RemoveEdge(Vertex<T> vertex)
        {
            neighbors.Remove(vertex);
        }


        public override string ToString()
        {

            StringBuilder allNeighbors = new StringBuilder("");
            allNeighbors.Append(value + ": ");

            foreach (Vertex<T> neighbor in neighbors)
            {
                allNeighbors.Append(neighbor.value + "  ");
            }

            return allNeighbors.ToString();

        }
    }

    public class Stage
    {
        #region Declarations
        
        private Random random = new Random();
        
        private List<Vertex<int>> vertices;
        private Vertex<int> vertex;
        private int graphSize;
        private int level;
        private Tema oTema;
		private Hero oHero;
        
        #endregion
        
        #region Methods

		public Stage(int level,Tema oTema, Hero oHero)
        {
			this.oHero = oHero;
            this.oTema = oTema;
            this.level = level;
            graphSize = oTema.getTamanhoDaListaDeAmbientes();
        }
        public void createGraph()
        {
            // Create a list of vertices using the Vertex<T> class
            vertices = new List<Vertex<int>>();
            for(int i = 0; i < graphSize; i++)
            {
                Vertex<int> aux = new Vertex<int>(i);
                aux.setAmbient( oTema.ListaDeAmbientes[i] );

                vertices.Add( aux );
            }

            // Establish edges; Ex. local 1 -> local 2, local 5, local 6
            int c = -1;
            for (int index = 1; index < graphSize; index++)
            {
               if(index % 2 == 1)
               {
                   c++;
               }
               vertices[index].AddEdge(vertices[c]);
            }
            c = 1;
            for (int i = 0; i < graphSize; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    vertices[i].AddEdge(vertices[c++]);
                    if(c == graphSize)
                    {
                        i = graphSize;
                        j = 2;
                    }
                }
            }

            // Set boss's position
            setHeroPosition();
            setBossPosition();

        }

        private void setHeroPosition()
        {
            int HeroPosition = 0;
            Ambient oAmbient = vertices[HeroPosition].getAmbient();
			oAmbient.setHero(oHero);
            vertex = vertices[HeroPosition];
        }

        private void setBossPosition()
        {
            int BossPosition = random.Next(0,graphSize);
            Ambient oAmbient = vertices[BossPosition].getAmbient();
            oAmbient.setBoss(true);
        }

        public void startStage()
        {
            Console.WriteLine("PROLOGO");
			bool acabou = false;
			while(acabou == false)
            {
                Console.Write("Você está em: ");
                Console.WriteLine(vertex.getAmbient().getName());
				Console.WriteLine (vertex.getAmbient ().getDescription ());
                
				Console.WriteLine("Suas opções são:");
                
                int i = 1;
                Console.WriteLine("0 - Explorar");
                foreach (Vertex<int> item in vertex.Neighbors)
                {
                    Console.Write(i + " - Ir para ");
                    Console.WriteLine(item.getAmbient().getName());
                    i++;
                }

                int option = Convert.ToInt32(Console.ReadLine());
                while(option > i)
                {
                    option = Convert.ToInt32(Console.ReadLine());
                }
                if(option == 0)
                {
                    Console.WriteLine("EXPLORANDO");
                    //Explorar ambiente
                    acabou = vertex.getAmbient().explorar();
                }
                else
                {
                    Console.WriteLine("Viajando ...");
					oTema.journey.setHero (oHero);
                    oTema.journey.explorar();
					oTema.journey.setHero (null);

                    //Andar pelo mapa
                    List<Vertex<int>> verticesDisponiveis = vertex.Neighbors;
                    vertex.getAmbient().setHero(null);                
                    vertex = verticesDisponiveis[option-1];
					vertex.getAmbient().setHero(oHero);
                }
				if (acabou == true)
                {
                    Console.WriteLine("ACABOU KRL");
                }
                //showHeroPosition();
            }
        }
        public void showHeroPosition()
        {
            int i = 0;
            foreach(Vertex<int> vertex in vertices)
            {
                Console.Write(vertex.getAmbient().getName() + " " + i + " ");
                i++;
                if ( vertex.getAmbient().getHero() != null )
                {
                    Console.WriteLine("Hero is here!!");
                }
                else
                {
                    Console.WriteLine("");
                }
            }
        }

        #endregion


    }
    class Program
    {
        static void Main(string[] args)
        {
            Tema oTema = new Forest();
			Hero oHero = new Mage ("Romeu");
			Stage oStage = new Stage(1, oTema, oHero);
            oStage.createGraph();
            oStage.startStage();

        }
    }
}