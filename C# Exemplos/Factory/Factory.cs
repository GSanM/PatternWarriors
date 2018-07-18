using System;
using System.Collections;
using System.Collections.Generic;


namespace Factory
{
	class PizzaFactory
	{
		public PizzaFactory()
		{
			
		}
		public Pizza CriaPizza(string nome)
		{
			Pizza pizza = null;

			if (nome == "Carbonara")
			{
				pizza = new Pizza.Carbonara();
			}
			if (nome == "Marguerita")
			{
				pizza = new Pizza.Marguerita();
			}
			if (nome == "Especial" )
			{
				pizza = new Pizza.Especial();
			}
			
			return pizza;
		}
	}
	class Pizza
	{
		private string nome;
		private int preco;
		private string ingredientes;

		public int Preco 
		{
			get
			{
				return preco;
			}
		}

		public string Nome
		{
			get
			{
				return nome;
			}
		}

		public string Ingredientes
		{
			get
			{
				return ingredientes;
			}
		}

		public class Marguerita: Pizza
		{
			public Marguerita()
			{
				nome = "Marguerita";
				preco = 28;
				ingredientes = "Molho de tomate, Mussarela, Oregano, Manjericão e Azeite de Oliva";
			}
		}
		public class Carbonara: Pizza
		{
			public Carbonara()
			{
				nome = "Carbonara";
				preco = 32;
				ingredientes = "Molho de tomate, Mussarela, Oregano, Bacon e Cebola";
			}
		}
		public class Especial: Pizza
		{
			public Especial()
			{
				nome = "Especial";
				preco = 37;
				ingredientes = "Molho de tomate, Mussarela, Oregano, Brócolis e Azeitona";
			}
		}
	}
	
	class Cliente
	{
		public void PizzaDetalhes()
		{
			PizzaFactory FabricaDePizza = new PizzaFactory();
			Pizza NovaPizza;

			//----Pizza-Margherita
			NovaPizza = FabricaDePizza.CriaPizza("Marguerita");
			Console.WriteLine("Sabor:        " + NovaPizza.Nome);
			Console.WriteLine("Valor:        " + NovaPizza.Preco);
			Console.WriteLine("Ingredientes: " + NovaPizza.Ingredientes);
			Console.WriteLine("-----------------------------------------------------------------");


			//----Pizza-Carbonara
			NovaPizza = FabricaDePizza.CriaPizza("Carbonara");
			Console.WriteLine("Sabor:        " + NovaPizza.Nome);
			Console.WriteLine("Valor:        " + NovaPizza.Preco);
			Console.WriteLine("Ingredientes: " + NovaPizza.Ingredientes);
			Console.WriteLine("-----------------------------------------------------------------");

			//----Pizza-Especial
			NovaPizza = FabricaDePizza.CriaPizza("Especial");
			Console.WriteLine("Sabor:        " + NovaPizza.Nome);
			Console.WriteLine("Valor:        " + NovaPizza.Preco);
			Console.WriteLine("Ingredientes: " + NovaPizza.Ingredientes);
			Console.WriteLine("-----------------------------------------------------------------");

		}

	}

	class main
	{
		public static void Main (string[] args)
		{
			Cliente c = new Cliente();
			c.PizzaDetalhes();
			
		}
	}
}
