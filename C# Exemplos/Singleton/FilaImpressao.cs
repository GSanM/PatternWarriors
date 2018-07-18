using System;
using System.Collections;
using System.Collections.Generic;


namespace FilaImpressao
{
	class Requisicao
	{
		private string nomeDocumentoASerImpresso;
		private string conteudoDocumentoASerImpresso;
		private int IdentificadorDaRequisicao;

		public Requisicao(string nome, string conteudo)
		{
			nomeDocumentoASerImpresso = nome;
			conteudoDocumentoASerImpresso = conteudo;
			Random NumeroRandomico = new Random();
			IdentificadorDaRequisicao = NumeroRandomico.Next();
		}
		public string getSetnome
		{
			get 
			{
				return nomeDocumentoASerImpresso;
			}
			set
			{
				nomeDocumentoASerImpresso = value;
			}
		}
		public string getSetconteudo
		{
			get
			{
				return conteudoDocumentoASerImpresso;	
			}
			set
			{
				conteudoDocumentoASerImpresso = value;
			}
		}
		public int getIdentificador
		{
			get 
			{
				return IdentificadorDaRequisicao;
			}
		}
		public void MostraRequisicao()
		{
			Console.WriteLine("Nome: \t\t{0,8:c}", nomeDocumentoASerImpresso);
			Console.WriteLine("Identificador: \t{0,8:c}", IdentificadorDaRequisicao);
			Console.WriteLine("Conteúdo: \t{0,8:c}", conteudoDocumentoASerImpresso);
		}

	}

	class FilaImpressao
	{
		private static FilaImpressao Instancia = null;
		Queue<Requisicao> Fila;

		private FilaImpressao()
		{
			Fila = new Queue<Requisicao>();
		}
		public static FilaImpressao getInstancia
		{
			get
			{
				if(null == Instancia)
				{
					Instancia = new FilaImpressao();
				}
				return Instancia;
			}
		}
		public void enfileraRequisicao(Requisicao proximaRequisicao)
		{
			Fila.Enqueue(proximaRequisicao);
		}
		public void imprimeRequisicao()
		{
			Requisicao elementoASerImpresso;
			elementoASerImpresso = Fila.Dequeue();
			elementoASerImpresso.MostraRequisicao();
		}
	}

	class main
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello this is csharp");
			Requisicao A = new Requisicao("1° Requisição", "Conteudo A");
			Requisicao B = new Requisicao("2° Requisição", "Conteudo B");			
			
			FilaImpressao Impressora = FilaImpressao.getInstancia;

			Impressora.enfileraRequisicao(A);
			Impressora.enfileraRequisicao(A);
			Impressora.enfileraRequisicao(B);

			Impressora.imprimeRequisicao();
			Impressora.imprimeRequisicao();
			Impressora.imprimeRequisicao();
		}
	}
}
