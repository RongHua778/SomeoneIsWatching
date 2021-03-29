using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class CSVReader
{

    public static List<string>[] ReadFile(TextAsset asset, int col)
    {
        List<string>[] lists = new List<string>[col];
        for (int i = 0; i < col; i++)
        {
            lists[i] = new List<string>();
        }


        string[] data = asset.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });
            for (int g = 0; g < row.Length; g++)
            {
                if (row[g].Contains("，"))
                {
                    row[g] = row[g].Replace("，", ",");
                    //tempDescription = str.replace("\"", "\"\"");
                }
            }


            for (int m = 0; m < col; m++)
            {
                lists[m].Add(row[m]);
            }
        }

        return lists;
    }

    public static List<string[]> ReadCSV(TextAsset asset)
    {
        List<string[]> list = new List<string[]>();
        string[] data = asset.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length - 1; i++)
        {
            if (data[i] != null)
            {
                string[] row = data[i].Split(new char[] { ',' });
                for (int g = 0; g < row.Length; g++)
                {
                    if (row[g].Contains("，"))
                    {
                        row[g] = row[g].Replace("，", ",");
                        //tempDescription = str.replace("\"", "\"\"");
                    }
                }

                list.Add(row);
            }
            //list.Add()

            //for (int m = 0; m < col; m++)
            //{
            //    list[i][m] = row[m];
            //}

        }
        return list;
    }


}
