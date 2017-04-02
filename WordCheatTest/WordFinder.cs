using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;

namespace WordCheatTest
{
		class WordFinder
		{
		public char[,] Matrix
		{
			get;
			set;
		}

		

		private List<string> words;
		private List<string> dictionary = new List<string>();
		private Queue<Thread> workers = new Queue<Thread>();

		public WordFinder()
		{
			LoadDictionaryFromFile();
		}
	
		public void Search(char[,] matrix)
		{
			Matrix = matrix;
			words = new List<string>();
			PrintMatrix(Matrix);
			

			for(int i = 0; i < Matrix.GetLength(0); i++)
			{
				for(int j = 0; j < Matrix.GetLength(1); j++)
				{
					Thread worker = new Thread(delegate() {
						bool[,] availabillityMap;
						InitializeAvailabillityMap(out availabillityMap);
						Search(i, j, ref availabillityMap);
					});
					workers.Enqueue(worker);
					worker.Start();
				}
			}

			while(workers.Count > 0)
			{
				if(workers.Peek().IsAlive)
				{
					Thread.Sleep(2000);
				}
				else
				{
					workers.Dequeue();
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
			string[] contents = File.ReadAllLines("..\\..\\Dictionary.txt");
			for(int i = 0; i < contents.Length; i++)
			{
				if(contents[i].Length >= Global.MinWordLength && contents[i].Length <= Global.MaxWordLength)
				{
					dictionary.Add(contents[i].ToLower());
				}
				
			}

			Console.WriteLine("Dictionary initialized with " + dictionary.Count + " words inside.");
		}

		private void InitializeAvailabillityMap(out bool[,] map)
		{
			map = new bool[Matrix.GetLength(0), Matrix.GetLength(1)];

			for(int i = 0; i < Matrix.GetLength(0); i++)
			{
				for (int j = 0; j < Matrix.GetLength(1); j++)
				{
					map[i, j] = true;
				}
			}
		}

		private void SortWords()
		{
			if(words.Count > 0)
			{
				words = words.OrderByDescending(word => word.Length).ToList();
			}
		}
		

		private void Search(int x, int y, ref bool[,] availabillityMap, string word = "")
		{
			//Console.WriteLine("x=" + x + " y=" + y);

			if(word.Length > Global.MaxWordLength || x < 0 || x > 3 || y < 0 || y > 3 || availabillityMap[x, y] == false)
			{
				return;
			}

			word += Matrix[x, y];
			availabillityMap[x, y] = false;

			if((word.Length >= Global.MinWordLength) && dictionary.Contains(word))
			{
				lock(words)
				{
					words.Add(word);
				}
			}

			Search(x + 1, y, ref availabillityMap, word);
			Search(x - 1, y, ref availabillityMap, word);
			Search(x, y + 1, ref availabillityMap, word);
			Search(x, y - 1, ref availabillityMap, word);
			Search(x + 1, y + 1, ref availabillityMap, word);
			Search(x + 1, y - 1, ref availabillityMap, word);
			Search(x - 1, y + 1, ref availabillityMap, word);
			Search(x - 1, y - 1, ref availabillityMap, word);

			availabillityMap[x, y] = true;
		
			return;
		}
	}
}
