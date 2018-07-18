using System;
using System.Collections;
using System.Collections.Generic;


namespace Command
{
    class CommandManager
    {
        private Stack<ICommand> History = new Stack<ICommand>();

        public void AdicionaComandos(ICommand Comando)
        {
            History.Push(Comando);
        }
        public ICommand RemoveComandos()
        {
            return History.Pop();
        }
        public void Show()
        {
            foreach (ICommand item in History)
            {
                Console.WriteLine(item);                
            }
        }
    }
    class Invoker
    {
        //Solicita a execução do comando
        private CommandManager History;

        public Invoker(CommandManager History)
        {
            this.History = History;
        }

        public void Do(ICommand comando)
        {
            comando.Execute();
            History.AdicionaComandos(comando);
        }
        public void Undo()
        {
            History.RemoveComandos().Undo();
        }
    }
    interface ICommand
    {
        void Execute();
        void Undo();
    }
    class CommandColar: ICommand
    {
        private ReceiverDocumento Documento;
        private string texto;
        private int posicao;

        public CommandColar(ReceiverDocumento Documento, string texto, int posicao)
        {
            this.Documento = Documento;
            this.texto = texto;
            this.posicao = posicao;
        }

        public void Execute()
        {
            Documento.Insere(texto, posicao);
        }
        public void Undo()
        {
            Documento.Remove(posicao);
        }
    }
    class CommandLog: ICommand
    {
        private CommandManager History;

        public CommandLog(CommandManager History)
        {
            this.History = History;
        }

        public void Execute()
        {
            History.Show();
        }
        public void Undo(){}
    }
    class ReceiverDocumento
    {
        //Implementa ação
        private List<string> documento = new List<string>();
        /*
        Atributo documento é uma abstração de um documento de texto,
        Foi utilizado uma lista a fim de simplificar a implementação
         */

        public void Insere(string texto, int posicao)
        {
            documento.Insert(posicao,texto);
        }
        public void Remove(int posicao)
        {
            documento.RemoveAt(posicao);
        }
        public void Show()
        {
            foreach (string item in documento)
            {
                Console.WriteLine(item);
            }
        }
    }

	class main
	{
		public static void Main (string[] args)
		{
            ReceiverDocumento Documento = new ReceiverDocumento();
            CommandColar Colar = new CommandColar(Documento, "AAAH", 0);
            CommandManager History = new CommandManager();
            CommandLog Log = new CommandLog(History);            
            Invoker invocador = new Invoker(History);
            invocador.Do(Colar);
            invocador.Do(Colar);
            invocador.Do(Colar);
            invocador.Undo();
            Documento.Show();
            invocador.Do(Log);
		}
	}
}