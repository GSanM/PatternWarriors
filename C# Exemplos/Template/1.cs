//Rextester.Program.Main is the entry point for your code. Don't change it.
//Compiler version 4.0.30319.17929 for Microsoft (R) .NET Framework 4.5

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rextester
{
    abstract class PratoDeMassa
    {
         public void preparaReceita()
         {
             aqueceAgua();
             cozinhaMassa(20);
             preparaIngredientes();
             preparaMolho();
             finaliza();
         }
         public abstract void preparaIngredientes();
         public abstract void preparaMolho();
         public abstract void finaliza();
        
         private void aqueceAgua()
         {
             Console.WriteLine("Aquecendo agua");
         }
        private void cozinhaMassa(int tempo)
        {
            Console.WriteLine("Cozinhando massa: " + tempo + " minutos");
        }
    }
    class MassaCarbonara: PratoDeMassa
    {
        public override void preparaIngredientes(){
            Console.WriteLine("Cortar 200g bacon, bater 2 ovos, ralar 100g de queijo pecorino, pimento do reino à gosto");
        }
        public override void preparaMolho(){
            Console.WriteLine("fritar o bacon e misturar ao ovo batido, pimenta e queijo");
        }
        public override void finaliza(){
            Console.WriteLine("Retirar a água da massa, misturar o molho e misturar.");
        }
    }
    
    class MassaAlhoeOleo: PratoDeMassa
    {
        public override void preparaIngredientes(){
            Console.WriteLine("Picar Alho");
        }
        public override void preparaMolho(){
            Console.WriteLine("fritar o alho no oleo");
        }
        public override void finaliza(){
            Console.WriteLine("Retirar a água da massa, misturar a massa ao alho frito");
        }
    }
    
    
    public class Program
    {
        public static void Main(string[] args)
        {
            MassaCarbonara oMassaC = new MassaCarbonara();
            oMassaC.preparaReceita();
            Console.WriteLine("_________________________________________________________");
            MassaAlhoeOleo oMassaA = new MassaAlhoeOleo();
            oMassaA.preparaReceita();
        }
    }
}
