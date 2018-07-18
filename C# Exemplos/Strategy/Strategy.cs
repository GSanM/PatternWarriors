using System;

namespace Strategy
{
    class Composition /*Context*/
    {
        private string nome;
        private IAnimal oAnimal;
        public Composition(string nome, IAnimal Animal)
        {
            this.nome = nome;
            this.oAnimal = Animal;
        }
        public void EmiteSom()
        {
            Console.WriteLine("O " + oAnimal.GetType().Name + " " + nome + " fez: ");
            oAnimal.EmiteSom();
        }
    }
    interface IAnimal /*Strategy ou Compositor*/
    {
        void EmiteSom();
    }
    class Pato: IAnimal /*ConcreteStrategy*/
    {
        public void EmiteSom()
        {
            Console.WriteLine("Quack");

        }
    }
    class Pombo: IAnimal /*ConcreteStrategy*/
    {
        public void EmiteSom()
        {
            Console.WriteLine("Pruu");

        }
    }
    class StrategyMain 
    {
        static void Main() 
        {
            Composition Donald = new Composition("Donald", new Pato());
            Donald.EmiteSom();
            Composition Doodle = new Composition("Doodle", new Pombo());
            Doodle.EmiteSom();
        }
    }
}