using System;
using System.IO;
using System.Collections.Generic;

public class Parser
{
    private string filename;

    private List<List<string>> readCSV(StreamReader reader)
    {
        List<List<string>> convertedCSV = new List<List<string>>();

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            List<string> listValues = new List<string>(values);
            convertedCSV.Add(listValues);
        }

        return convertedCSV;
    }

    public List<List<string>> CSVLoader()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        string assets = "assets";
        string finalPath = Path.Combine(path, assets);
        string finalFilename = Path.Combine(finalPath, filename);

        StreamReader newReader = new StreamReader(finalFilename);
        return readCSV(newReader);
    }

    public List<Tuple<string, int>> sum(string targetColumn, int amount)
    {
        List<List<string>> csv = this.CSVLoader();
        List<Tuple<string, int>> sumTuples = new List<Tuple<string, int>>();
        Dictionary<string, int> columnSumPairs = new Dictionary<string, int>();

        bool first = true;
        int columnIndex = -1;

        for(int i = 0; i < csv[0].Count; i++)
        {
            if (csv[0][i] == targetColumn)
            {
                columnIndex = i;
            }
        }

        foreach(List<string> row in csv)
        {
            if (first)
            {
                first = false;
                continue;
            }

            if (row[columnIndex] != "")
            {
                columnSumPairs[row[columnIndex]] = columnSumPairs[row[columnIndex]] + 1;
            }
        }

        for (int i = 0; i < amount; i++)
        {
            string currentCellValue = "";
            int currentHighest = 0;

            if(columnSumPairs.Count == 0)
            {
                break;
            }

            foreach(KeyValuePair<string, int> entry in columnSumPairs)
            {
                if(entry.Value > currentHighest)
                {
                    currentCellValue = entry.Key;
                }
            }

            Tuple<string, int> newTuple = new Tuple<string, int>(currentCellValue, columnSumPairs[currentCellValue);
            sumTuples.Add(newTuple);
            columnSumPairs.Remove(currentCellValue);
        }

        return sumTuples;
    }

    public List<List<string>> map(string targetColumn, Func<string, string> func)
    {
        List<List<string>> csv = this.CSVLoader();

        return map(csv, targetColumn, func);
    }

    public List<List<string>> map(List<List<string>> csv, string targetColumn, Func<string, string> func)
    {
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

        foreach (List<string> row in csv)
        {
            if (first)
            {
                first = false;
                table.Add(row);
            } else
            {
                row[columnIndex] = func(row[columnIndex]);
                table.Add(row);
            }
        }

        return table;
    }

    Parser(string fn)
    {
        this.filename = fn;
    }
}