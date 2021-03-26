using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu :View
{
    Image BG;

    public override string Name { get { return Const.V_MainMenu; } }

    void Start()
    {
        BG = transform.Find("BG").GetComponent<Image>();
        Sound.Instance.PlayBg("BGMusic/MenuMusic",0.35f);
    }

    public void StartGame()
    {
        Game.Instance.LoadScene(2);
    }

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

    public void QuitBtnClick()
    {
        Application.Quit();
    }

    public override void RegisterEvents()
    {
        
    }

    public override void HandleEvent(string eventName, object obj)
    {
        
    }
    // Start is called before the first frame update



}
