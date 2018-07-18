using System;

namespace Adapter
{
    
    class Caldeira
    {
        private float temperaturaFahrenheit;

        public float Temperatura
        {
            get
            {
                return temperaturaFahrenheit;
            }
            set
            {
                temperaturaFahrenheit = value;
            }
        }
    }

    class Adapter
    {
        private float temperaturaFahrenheit;
        private Caldeira oCaldeira;
        
        public Adapter(Caldeira oCaldeira)
        {
            this.oCaldeira = oCaldeira;
        }
        
        public float Temperatura
        {
            set
            {
                temperaturaFahrenheit = ((value * 9) / 5) + 32;
                oCaldeira.Temperatura = temperaturaFahrenheit;
            }
        }
    }

    class Acionador
    {
        private float TemperaturaCelsius;
        private Adapter oAdaptador;
        public Acionador(Adapter oAdaptador)
        {
            this.oAdaptador = oAdaptador;
        }
        
        public float Temperatura
        {
            get
            {
                return TemperaturaCelsius;
            }
            set
            {
                TemperaturaCelsius = value;
                oAdaptador.Temperatura = TemperaturaCelsius;
            }
        }
    }

    class AdapterMain 
    {
        static void Main() 
        {
            Caldeira oCaldeira = new Caldeira();
            Adapter oAdaptador = new Adapter(oCaldeira);            
            Acionador oAcionador = new Acionador(oAdaptador);
            

            while(true)
            {
                System.Console.WriteLine("Digite a temperatura desejada em Celcius:");
                float TemperaturaCelsius = float.Parse(Console.ReadLine());

                oAcionador.Temperatura = TemperaturaCelsius;
                Console.WriteLine("Temperatura Acionador = " + oAcionador.Temperatura);
                Console.WriteLine("Temperatura Caldeira = " + oCaldeira.Temperatura);

            }
        }
    }
}