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
			BossPosition = 0;
            Ambient oAmbient = vertices[BossPosition].getAmbient();
            oAmbient.setBoss(true);
        }

        public bool startStage()
        {
			library.slowWrite("PROLOGO",Constants.TEXT_SPEED2,true);
			bool acabou = false;
			bool win = false;
			while(acabou == false)
            {
				if (oHero.getLife() <= 0)
				{
					acabou = true;
				}
				library.slowWrite("Você está em: ",Constants.TEXT_SPEED2,false);
				library.slowWrite(vertex.getAmbient().getName(),Constants.TEXT_SPEED2,true);
				library.slowWrite (vertex.getAmbient ().getDescription (),Constants.TEXT_SPEED2,true);
                
				library.slowWrite("Suas opções são:",Constants.TEXT_SPEED2,true);
                
                int i = 1;
				library.slowWrite("0 - Explorar",Constants.TEXT_SPEED2,true);
                foreach (Vertex<int> item in vertex.Neighbors)
                {
					library.slowWrite(i + " - Ir para ",Constants.TEXT_SPEED2,false);
					library.slowWrite(item.getAmbient().getName(),Constants.TEXT_SPEED2,true);
                    i++;
                }

                int option = Convert.ToInt32(Console.ReadLine());
                while(option > i)
                {
                    option = Convert.ToInt32(Console.ReadLine());
                }
                if(option == 0)
                {
					library.slowWrite("Explorando ...",Constants.TEXT_SPEED2,true);
                    //Explorar ambiente
					oHero = vertex.getAmbient().explorar(oHero);
                }
                else
                {
					library.slowWrite("Viajando ...",Constants.TEXT_SPEED2,true);
					oTema.journey.setHero (oHero);
					oHero = oTema.journey.explorar(oHero);
					oTema.journey.setHero (null);

                    //Andar pelo mapa
                    List<Vertex<int>> verticesDisponiveis = vertex.Neighbors;
                    vertex.getAmbient().setHero(null);                
                    vertex = verticesDisponiveis[option-1];
					vertex.getAmbient().setHero(oHero);
                }
				if ( (oHero.getLife () <= 0) || (oHero.getPergaminho() == true) )
				{
					acabou = true;
					if (oHero.getPergaminho())
						win = true;
				}
                //showHeroPosition();
            }
			return win;
        }
        public void showHeroPosition()
        {
            int i = 0;
            foreach(Vertex<int> vertex in vertices)
            {
							library.slowWrite(vertex.getAmbient().getName() + " " + i + " ",Constants.TEXT_SPEED2,false);
                i++;
                if ( vertex.getAmbient().getHero() != null )
                {
					library.slowWrite("Hero is here!!",Constants.TEXT_SPEED2,false);
                }
                else
                {
					library.slowWrite("",Constants.TEXT_SPEED2,true);
                }
            }
        }

        #endregion


	}
}