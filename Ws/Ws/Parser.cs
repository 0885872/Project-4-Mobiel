using System;
using System.IO;
using System.Collections.Generic;
using Android;
using Android.Content;
using Android.Content.Res;
using Patterns;

namespace Ws
{
    public class CSVIterator : Iterator<List<string>>
    {
        private int index = -1;
        private List<List<string>> csv;

        public Option<List<string>> GetNext()
        {
            index = index + 1;

            if (index < csv.Count)
            {
                return new Some<List<string>>(csv[index]);
            } else
            {
                return new None<List<string>>();
            }
        }

        public void Reset()
        {
            index = -1;
        }
    }

    public class CSVParser
    {
        private AssetManager assets;

        public List<List<string>> readCSV(string filename)
        {
            List<List<string>> convertedCSV = new List<List<string>>();

            StreamReader streamReader = new StreamReader(assets.Open(filename));
            string line;

            while ((line = streamReader.ReadLine()) != null)
            {
                string[] values = line.Split(',');
                List<string> listValues = new List<string>(values);
                convertedCSV.Add(listValues);
            }

            Console.WriteLine("End count: " + convertedCSV.Count);

            return convertedCSV;
        }

        public List<Tuple<string, int>> sum(string filename, string targetColumn, int amount)
        {
            List<List<string>> csv = this.readCSV(filename);
            List<Tuple<string, int>> sumTuples = new List<Tuple<string, int>>();
            Dictionary<string, int> columnSumPairs = new Dictionary<string, int>();

            bool first = true;
            int columnIndex = -1;

            for (int i = 0; i < csv[0].Count; i++)
            {
                if (csv[0][i] == targetColumn)
                {
                    columnIndex = i;
                }
            }

            Console.WriteLine("Metadata");
            Console.WriteLine("Index: " + columnIndex);
            Console.WriteLine(csv[0][columnIndex]);

            if (columnIndex == -1)
            {
                throw new System.IndexOutOfRangeException("Targeted column does not exist in list.");
            }

            int properCount = csv[0].Count;

            foreach (List<string> row in csv)
            {
                if (first)
                {
                    first = false;
                    continue;
                }

                if (row.Count == properCount && row[columnIndex] != "")
                {
                    if (columnSumPairs.ContainsKey(row[columnIndex]))
                    {
                        columnSumPairs[row[columnIndex]] = columnSumPairs[row[columnIndex]] + 1;
                    } else
                    {
                        columnSumPairs[row[columnIndex]] = 1;
                    }
                }
            }

            for (int i = 0; i < amount; i++)
            {
                string currentCellValue = "";
                int currentHighest = 0;

                if (columnSumPairs.Count == 0)
                {
                    break;
                }

                foreach (KeyValuePair<string, int> entry in columnSumPairs)
                {
                    if (entry.Value > currentHighest)
                    {
                        currentHighest = entry.Value;
                        currentCellValue = entry.Key;
                    }
                }

                Tuple<string, int> newTuple = new Tuple<string, int>(currentCellValue, columnSumPairs[currentCellValue]);
                sumTuples.Add(newTuple);
                columnSumPairs.Remove(currentCellValue);

                currentCellValue = "";
                currentHighest = 0;
            }

            return sumTuples;
        }

        public List<List<string>> map(string filename, string targetColumn, Func<string, string> func)
        {
            List<List<string>> csv = readCSV(filename);

            Console.WriteLine("Starting row count of original: " + csv.Count);

            List<List<string>> table = new List<List<string>>();
            bool first = true;
            int columnIndex = -1;

            for (int i = 0; i < csv[0].Count; i++)
            {
                if (csv[0][i] == targetColumn)
                {
                    columnIndex = i;
                }
            }

            if (columnIndex == -1)
            {
                throw new System.IndexOutOfRangeException("Targeted column does not exist in list.");
            }

            foreach (List<string> row in csv)
            {
                if (first)
                {
                    first = false;
                    table.Add(row);
                }
                else
                {
                    row[columnIndex] = func(row[columnIndex]);
                    table.Add(row);
                }
            }

            Console.WriteLine("Ultimate column count: " + table.Count);

            return table;
        }

        public CSVParser(AssetManager am)
        {
            this.assets = am;
        }
    }
}