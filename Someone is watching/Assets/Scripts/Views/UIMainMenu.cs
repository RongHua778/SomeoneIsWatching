using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu :View
{
    Image BG;
    GameModel m_GameModel;
    [SerializeField] Button loadGameBtn;

    public override string Name { get { return Const.V_MainMenu; } }

    void Start()
    {
        m_GameModel = GetModel<GameModel>() as GameModel;
        BG = transform.Find("BG").GetComponent<Image>();
        Sound.Instance.PlayBg("BGMusic/MenuMusic",0.35f);

        if (!PlayerPrefs.HasKey("SaveDay"))
        {
            loadGameBtn.enabled = false;
            loadGameBtn.transform.Find("Text").GetComponent<Text>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
    }

    public void StartGame()
    {
        PlayerPrefs.DeleteKey("SaveDay");
        m_GameModel.Day = 1;
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

    public void LoadGameClick()
    {
        if (PlayerPrefs.HasKey("SaveDay"))
        {
            int day = PlayerPrefs.GetInt("SaveDay");
            m_GameModel.Day = day;
            Game.Instance.LoadScene(2);
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
