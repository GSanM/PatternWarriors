using System;
using System.Collections;
using System.Collections.Generic;


namespace Observer
{
    class SubjectPDSII
    {
        private List<ObserverAlunos> ListaObservers = new List<ObserverAlunos>();

        public void register(ObserverAlunos oObserver)
        {
            ListaObservers.Add(oObserver);
        } 
        public void unregister(ObserverAlunos oObserver)
        {
            ListaObservers.Remove(oObserver);
        } 
        public void notify()
        {
            foreach (ObserverAlunos item in ListaObservers)
            {
                item.update();
            }
        }
    }
    class ConcreteSubjectPDSII: SubjectPDSII
    {
        private string StateMSG;

        public string getState()
        {
            return StateMSG;
        }
        public void setState(string State)
        {
            this.StateMSG = State;

            notify();
        }
    }
    
    interface ObserverAlunos
    {
        void update();
    }
    class ConcreteObserverAlunos: ObserverAlunos
    {
        private string StateMSG;
        private ConcreteSubjectPDSII PDSII;
        
        public ConcreteObserverAlunos(ConcreteSubjectPDSII PDSII)
        {
            this.PDSII = PDSII;
        }
        public void update()
        {
            this.StateMSG = PDSII.getState();
            ShowState();
        }
        public void ShowState()
        {
            Console.WriteLine(StateMSG);
        }
    }

	class main
	{
		public static void Main (string[] args)
		{
            ConcreteSubjectPDSII PDSII = new ConcreteSubjectPDSII();
            ConcreteObserverAlunos Aluno = new ConcreteObserverAlunos(PDSII);

            PDSII.register(Aluno);
            PDSII.setState("Boa Tarde a todos!");
		}
	}
}