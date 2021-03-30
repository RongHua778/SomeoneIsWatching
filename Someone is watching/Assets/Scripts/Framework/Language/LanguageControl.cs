using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LanguageControl
{
    static Dictionary<string, string> LangMap_en = new Dictionary<string, string>();
    static Dictionary<string, string> LangMap_ch = new Dictionary<string, string>();


    // Start is called before the first frame update
    public static void LoadLanguage()
    {

        List<string[]> lang = CSVReader.ReadCSV(Resources.Load<TextAsset>("Translation/Translation"));
        
        foreach(string[] lan in lang)
        {
            if(!LangMap_ch.ContainsKey(lan[0]))
                LangMap_ch.Add(lan[0], lan[1]);
            if (!LangMap_en.ContainsKey(lan[0]))
                LangMap_en.Add(lan[0], lan[2]);
        }
    }




    public static string GetValue(string key)
    {
        string languageType = StaticData.language;
        string data;
        if (!LangMap_en.TryGetValue(key,out data))
        {
            Debug.LogError("This key not present in language DIC:" + key);
            return "";
        }
        if (languageType == "en")
            return LangMap_en[key];
        else if (languageType == "ch")
            return LangMap_ch[key];
        return "N/A";
    }


}
