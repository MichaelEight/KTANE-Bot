using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KTANE_Bot
{
    public class Symbols : KTANE_Module
    {
        private static readonly Dictionary<string, int>[] Columns;
        private List<string> _input;

        // Add InputLength property
        public int InputLength => _input.Count;

        static Symbols()
        {
            // Adjust the path to the location of your Symbols.txt file
            var allSymbols = File.ReadAllLines("Symbols.txt");
            Columns = new Dictionary<string, int>[6];

            int[][] columnOrders = new int[6][]
            {
                new int[] { 0, 1, 2, 3, 4, 5, 6 }, // Order for the first column
                new int[] { 7, 0, 6, 8, 9, 5, 10 }, // Order for the second column
                new int[] { 11, 12, 8, 13, 14, 2, 9 }, // Order for the third column
                new int[] { 15, 16, 17, 4, 13, 10, 18 }, // Order for the fourth column
                new int[] { 19, 18, 17, 20, 16, 21, 22 }, // Order for the fifth column
                new int[] { 15, 7, 23, 24, 19, 25, 26 } // Order for the sixth column
            };

            for (int i = 0; i < 6; i++)
            {
                Columns[i] = columnOrders[i].ToDictionary(index => allSymbols[index], index => Array.IndexOf(columnOrders[i], index));
            }
        }

        public Symbols(Bomb bomb) : base(bomb)
        {
            _input = new List<string>();
        }

        public override string Solve()
        {
            foreach (var column in Columns)
            {
                if (_input.All(column.ContainsKey))
                {
                    var output = _input.OrderBy(x => column[x]).ToList();
                    return $"First is {output[0]}; then {output[1]}; then {output[2]}; then {output[3]}.";
                }
            }

            return "Wrong sequence.";
        }

        public void AppendSymbol(string symbol)
        {
            if (_input.Count == 4) return;
            _input.Add(symbol);
        }
    }
}