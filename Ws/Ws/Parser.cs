using System;
using System.IO;
using System.Collections.Generic;

public class Parser
{
    private List<String> filenames;

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
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        string assets = "assets";
        string finalPath = Path.Combine(path, assets);
        string finalFilename = Path.Combine(finalPath, filename);

        StreamReader newReader = new StreamReader(finalFilename);
        return readCSV(newReader);
    }

    public List<List<List<String>>> AllCVS()
    {
        List<List<List<String>>> allLists = new List<List<List<String>>>();

        for (int i = 0; i < filenames.Count; i++)
        {
            List<List<String>> newList = CSVLoader(filenames[i]);
            allLists.Add(newList);
        }

        return allLists;
    }

    Parser(params string[] filenameArray)
    {
        List<String> fns = new List<String>();

        for (int i = 0; i < filenameArray.Length; i++)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string assets = "assets";
            string finalPath = Path.Combine(path, assets);
            string finalFilename = Path.Combine(finalPath, filenameArray[i]);
            fns.Add(finalFilename);
        }

        this.filenames = fns; // test
    }
}