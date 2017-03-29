using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCheatTest
{
	class Program
	{
		static void Main(string[] args)
		{
			char[,] array = new char[4, 4];
			WordFinder finder = new WordFinder(array);
			Console.ReadKey();
		}

		static void FillArrayWithRandom(ref char[,] array)
		{
			Random rand = new Random();
			string abc = "abcdefghijklmnopqrstuvwxyz";

			for(int i = 0; i < array.GetLength(0); i++)
			{
				for(int j = 0; j < array.GetLength(1); j++)
				{
					array[i, j] = abc[rand.Next(0, 25)];
				}
			}
		}

		static void PrintArray(char[,] array)
		{
			for(int i = 0; i < array.GetLength(0); i++)
			{
				for(int j = 0; j < array.GetLength(1); j++)
				{
					Console.Write(array[i, j] + " ");
				}
				Console.WriteLine();
			}
		}
	}
}
