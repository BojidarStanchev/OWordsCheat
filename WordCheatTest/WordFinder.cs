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
		private string[] words;
		private string[] dictionary;

		public WordFinder(char[,] matrix)
		{
			this.matrix = matrix;
			LoadDictionaryFromFile();	
		}

		private void LoadDictionaryFromFile()
		{
			dictionary = File.ReadAllLines("..\\..\\Dictionary.txt");
		}

		private bool IsWord(string str)
		{
			return dictionary.Contains(str);
		}

		private void SortWords()
		{
			words = words.OrderByDescending(word => word.Length).ToArray();
		}

		private void Search(string str = "")
		{

		}

	}
}
