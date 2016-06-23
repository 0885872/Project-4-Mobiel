using System;
using System.IO;
using System.Collections.Generic;

public class Parser
{
    private List<String> paths;

    public List<List<String>> readCSV(StreamReader reader)
    {
        List<List<String>> convertedCSV = new List<List<string>>();

        while (!reader.EndOfStream)
        {
            String line = reader.ReadLine();
            string[] values = line.Split(',');
            List<String> listValues = new List<String>(values);
            convertedCSV.Add(listValues);
        }

        return convertedCSV;
    }

    public List<List<String>> CSVLoader(string filename)
    {
        StreamReader newReader = new StreamReader(File.OpenRead(filename));
        return readCSV(newReader);
    }

    public List<List<List<String>>> AllCVS()
    {
        List<List<List<String>>> allLists = new List<List<List<String>>>();

        for (int i = 0; i < paths.Count; i++)
        {
            List<List<String>> newList = CSVLoader(paths[i]);
            allLists.Add(newList);
        }

        // StreamReader trommelReader = new StreamReader(File.OpenRead(@"C:\Users\Evelyn\Text\School\Project 4\Fietstrommels-maart-2013.csv"));
        // StreamReader diefstalReader = new StreamReader(File.OpenRead(@"C:\Users\Evelyn\Text\School\Project 4\fietsdiefstal-rotterdam-2011-2013.csv"));
        
        return allLists;
    }

    Parser(params string[] pathsArray)
    {
        List<String> pathsList = new List<String>();

        for (int i = 0; i < pathsArray.Length; i++)
        {
            pathsList.Add(pathsArray[i]);
        }

        this.paths = pathsList;
    }
}