using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordCheatTest
{
		class WordFinder
		{
		private char[,] matrix;
		private bool[,] availabillityMap;

		private List<string> words = new List<string>();
		private string[] dictionary;

		public WordFinder(char[,] matrix)
		{
			this.matrix = matrix;
			PrintMatrix(matrix);
			LoadDictionaryFromFile();
			InitializeAvailabillityMap();
			Search();
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
			availabillityMap = new bool[matrix.GetLength(0), matrix.GetLength(1)];

			for(int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					availabillityMap[i, j] = true;
				}
			}
		}

		private bool IsWord(string str)
		{
			return dictionary.Contains(str);
		}

		private void SortWords()
		{
			if(words.Count > 0)
			{
				words = words.OrderByDescending(word => word.Length).ToList();
			}
		}
		private void Search()
		{
			for(int i = 0; i < matrix.GetLength(0); i++)
			{
				for(int j = 0; j < matrix.GetLength(1); j++)
				{
					Search(i, j);
				}
			}
		}

		private void Search(int x, int y, string word = "")
		{
			if(word.Length > 9 || x < 0 || x > 3 || y < 0 || y > 3 || availabillityMap[x, y] == false)
			{
				return;
			}

			word += matrix[x, y];
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
