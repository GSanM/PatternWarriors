using System;
using System.Collections;
using System.Collections.Generic;

namespace Composite
{
    interface Icomposite
    {
        void desenha();
    }
    class Imagem: Icomposite
    {
        private string name;
        private List<Linha> ListaLinhas = new List<Linha>();
        private List<Quadrado> ListaQuadrado = new List<Quadrado>();
        private List<Circulo> ListaCirculo = new List<Circulo>();

        public Imagem(string name)
        {
            this.name = name;
        }
        public void desenha()
        {
            Console.WriteLine(name + ":");

            foreach (Linha item in ListaLinhas)
            {
                item.desenha();
            }
            foreach (Quadrado item in ListaQuadrado)
            {
                item.desenha();
            }
            foreach (Circulo item in ListaCirculo)
            {
                item.desenha();
            }
        }
        public void AdicionaLinha(Linha oLinha)
        {
            ListaLinhas.Add(oLinha);
        }
        public void AdicionaQuadrado(Quadrado oQuadrado)
        {
            ListaQuadrado.Add(oQuadrado);
        }
        public void AdicionaCirculo(Circulo oCirculo)
        {
            ListaCirculo.Add(oCirculo);
        }
        public void RemoveLinha(Linha oLinha)
        {
            ListaLinhas.Remove(oLinha);
        }
        public void RemoveQuadrado(Quadrado oQuadrado)
        {
            ListaQuadrado.Remove(oQuadrado);
        }
        public void RemoveCirculo(Circulo oCirculo)
        {
            ListaCirculo.Remove(oCirculo);
        }
    }
    class Linha: Icomposite
    {
        private string name;
        public Linha(string name)
        {
            this.name = name;
        }
        public void desenha()
        {
            Console.WriteLine("Desenhou Linha: " + name);
        }
    }
    class Quadrado: Icomposite
    {
        private string name;
        public Quadrado(string name)
        {
            this.name = name;
        }
        public void desenha()
        {
            Console.WriteLine("Desenhou Quadrado: " + name);
        }
    }
    class Circulo: Icomposite
    {
        private string name;
        public Circulo(string name)
        {
            this.name = name;
        }
        public void desenha()
        {
            Console.WriteLine("Desenhou Circulo: " + name);
        }
    }
    class main
    {
        static void Main()
        {
            Linha oLinha = new Linha("Primeira Linha");
            Linha oLinha2 = new Linha("Segunda Linha");
            Circulo oCirculo = new Circulo("Circulo");
            Quadrado oQuadrado = new Quadrado("Quadrado");
            Imagem oImagem = new Imagem("Primeira Imagem");

            oImagem.AdicionaLinha(oLinha);
            oImagem.AdicionaLinha(oLinha2);
            oImagem.AdicionaCirculo(oCirculo);
            oImagem.AdicionaQuadrado(oQuadrado);

            oImagem.desenha();
        }
    }
}