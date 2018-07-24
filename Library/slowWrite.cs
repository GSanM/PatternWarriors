/**
 * ..:: Library for Prints ::..
 * 
 * @author Rafael Romeu
 * @author Gabriel Moraes 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Library
{
	public static class library
	{
		/**
        * slowWrite
        *
        * Metodo para a escrever na tela letra a letra(Estilo Maquina de Escrever).
        * 
        * @param string message
        * @param int delay
        * @return void
        * 
        */
		public static void slowWrite(string message, int delay, bool endline)
		{
			foreach (char letter in message)
			{
				Console.Write(letter);
				System.Threading.Thread.Sleep(delay);
			}
			if (endline == true)
				Console.WriteLine("");
		}
	}
}

