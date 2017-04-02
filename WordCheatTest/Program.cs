using System;

namespace WordCheatTest
{
	class Program
	{
		static void Main(string[] args)
		{
			WordFinder finder = new WordFinder();

			while(true)
			{
				char[,] array = new char[4, 4];
				string chars = Console.ReadLine();
				FillArray(ref array, string.Join("", chars.Split(' ')).ToCharArray());
				finder.Search(array);
				Console.WriteLine("Program is ready to accept new input.");
			}
			
		}

		static void FillArray(ref char[,] array, char[] input)
		{
			for(int i = 0; i < array.GetLength(0); i++)
			{
				for(int j = 0; j < array.GetLength(1); j++)
				{
					array[j, i] = input[i + 4 * j];
				}
			}
		}
	}
}
