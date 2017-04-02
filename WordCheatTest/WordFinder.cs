using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace WordCheatTest
{
		class WordFinder
		{
		public char[,] Matrix
		{
			get;
			set;
		}

		private bool[,] availabillityMap;

		private List<string> words;
		private string[] dictionary;

		public WordFinder()
		{
			LoadDictionaryFromFile();
		}
	
		public void Search(char[,] matrix)
		{
			Matrix = matrix;
			words = new List<string>();
			PrintMatrix(Matrix);
			InitializeAvailabillityMap();

			for(int i = 0; i < Matrix.GetLength(0); i++)
			{
				for(int j = 0; j < Matrix.GetLength(1); j++)
				{
					Search(i, j);
				}
			}

			SortWords();
			PrintWords();
		}

		public void PrintMatrix(char[,] array)
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


		private void PrintWords()
		{
			for(int i = 0; i < words.Count; i++)
			{
				Console.WriteLine(words[i]);
			}
		}

		private void LoadDictionaryFromFile()
		{
			dictionary = File.ReadAllLines("..\\..\\Dictionary.txt");
		}

		private void InitializeAvailabillityMap()
		{
			availabillityMap = new bool[Matrix.GetLength(0), Matrix.GetLength(1)];

			for(int i = 0; i < Matrix.GetLength(0); i++)
			{
				for (int j = 0; j < Matrix.GetLength(1); j++)
				{
					availabillityMap[i, j] = true;
				}
			}
		}

		private bool IsWord(string str)
		{
			return dictionary.Contains(str) && (str.Length > 2);
		}

		private void SortWords()
		{
			if(words.Count > 0)
			{
				words = words.OrderByDescending(word => word.Length).ToList();
			}
		}
		

		private void Search(int x, int y, string word = "")
		{
			if(word.Length > 9 || x < 0 || x > 3 || y < 0 || y > 3 || availabillityMap[x, y] == false)
			{
				return;
			}

			word += Matrix[x, y];
			availabillityMap[x, y] = false;

			if(IsWord(word))
			{
				words.Add(word);
			}

			Search(x + 1, y, word);
			Search(x - 1, y, word);
			Search(x, y + 1, word);
			Search(x, y - 1, word);
			Search(x + 1, y + 1, word);
			Search(x + 1, y - 1, word);
			Search(x - 1, y + 1, word);
			Search(x - 1, y - 1, word);

			availabillityMap[x, y] = true;
		
			return;
		}
	}
}
