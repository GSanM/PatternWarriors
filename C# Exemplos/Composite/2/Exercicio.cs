//Exercicio2
using System;
using System.Collections;
using System.Collections.Generic;

namespace Composite
{
    interface Icomposite
    {
        void VerificaItens();
    }
 
    class Carrinho: Icomposite
    {
        private double precoTotal;
        private List<Item> ListaItens = new List<Item>();

        public Carrinho()
        {
            precoTotal = 0;
        }

        public void VerificaItens()
        {
            precoTotal = 0;
            Console.WriteLine("Carrinho de Compras:");

            foreach (Item item in ListaItens)
            {
                item.VerificaItens();
                
                precoTotal = precoTotal + (item.getPreco() * item.getUnidades());
            }
            Console.WriteLine("Preço Total: " + precoTotal + " R$");
        }
        public void AdicionaItem(Item oItem)
        {
            ListaItens.Add(oItem);
        }
        public void RemoveItem(Item oItem)
        {
            ListaItens.Remove(oItem);
        }
    }
    class Item: Icomposite
    {
        private string name;
        private string tipo;
        private double preco;
        private int unidades;
        public Item(string name,string tipo, double preco, int unidades)
        {
            this.name = name;
            this.tipo = tipo;
            this.preco = preco;
            this.unidades = unidades;
        }
        public void VerificaItens()
        {
            Console.WriteLine("-> " + tipo + " (" + name + ", " + preco + " R$, " + unidades + " Unidade(s) )");
        }
        public double getPreco()
        {
            return preco;
        }
        public int getUnidades()
        {
            return unidades;
        }
    }

    class main
    {
        static void Main()
        {
            Item oItem1 = new Item("O Senhor dos Aneis", "Livro", 37.90, 1);
            Item oItem2 = new Item("O Homem que Calculava", "Livro", 43.90, 1);
            Item oItem3 = new Item("James Gang Rides Again", "CD", 23.90, 2);

            Carrinho oCarrinho = new Carrinho();

            oCarrinho.AdicionaItem(oItem1);
            oCarrinho.AdicionaItem(oItem2);
            oCarrinho.AdicionaItem(oItem3);
            oCarrinho.VerificaItens();
        }
    }
}