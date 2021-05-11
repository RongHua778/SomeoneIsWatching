using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] UIFunctionManager _uifunctionManager=default;

    public void ChangeLanguage()
    {
        if (StaticData.language == "ch")
        {
            StaticData.language = "en";
            GameEvents.Instance.LanguageChange();
        }
        else
        {
            StaticData.language = "ch";
            GameEvents.Instance.LanguageChange();

        }
    }

    public void SaveAndQuit()
    {
        PlayerPrefs.SetInt("SaveDay", _uifunctionManager.m_GameModel.Day);
        _uifunctionManager.m_GameModel.Reset();
        Game.Instance.LoadScene(1);
    }
}
