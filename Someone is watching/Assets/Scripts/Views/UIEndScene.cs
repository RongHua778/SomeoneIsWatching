using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class UIEndScene : View
{
    [SerializeField] CreditPanel creditPanel = default;
    [SerializeField]
    TextHandler EndText = default;
    GameModel m_GameModel;
    public override string Name { get { return Const.V_EndScene; } }
    VideoPlayer m_VideoPlayer;
    public TextHandler MainBtnTxt;

    [SerializeField]
    Image EndSceneImg = default;

    Transform MainBtn;
    Transform EndTextTrans;

    private void Awake()
    {
        m_VideoPlayer = this.transform.Find("VideoPlayer").GetComponent<VideoPlayer>();
        MainBtn = transform.Find("Restart");
        EndTextTrans = transform.Find("EndText");
        m_GameModel = GetModel<GameModel>() as GameModel;
        SetEndText(m_GameModel.GameOverState);
    }

    void SetEndText(string text)
    {
        bool lose = true;
        bool demo = false;
        string endText = "";
        switch (text)
        {
            case "CloseCamera":
                endText = "ending2";
                Sound.Instance.PlayBg("BGMusic/GameOverMusic", 1f);
                SetEndImg("HasBeenSeen");
                m_GameModel.unlockRecord = false;
                break;
            case "ClockWrong":
            case "Day1":
                endText = "ending3";
                Sound.Instance.PlayBg("BGMusic/GameOverMusic", 1f);
                SetEndImg("WrongTime");
                m_GameModel.unlockRecord = false;
                break;
            case "Day2":
                endText = "day2";
                MainBtnTxt.SetText("next");
                lose = false;
                SetEndImg("Pieces");
                break;
            case "Day3":
                endText = "day3";
                lose = false;
                SetEndImg("Pieces");
                break;
            case "Day4":
                lose = false;
                demo = true;
                endText = "day4";
                SetEndImg("Pieces");
                break;

            case "PlayMusicBox":
                Sound.Instance.PlayBg("BGMusic/GameOverMusic", 1f);
                SetEndImg("CannotSpeak");
                endText = "ending4";
                break;
            case "DeadManNoSay":
                endText = "ending1";
                Sound.Instance.PlayBg("BGMusic/GameOverMusic", 1f);
                SetEndImg("CannotSpeak");
                break;

        }
        if (demo)
        {
            MainBtnTxt.SetText("return");
        }
        else if (!lose)
        {
            MainBtnTxt.SetText("next");
            //SetItemsGet(false);
        }
        else
        {
            MainBtnTxt.SetText("restart");
            //SetItemsGet(true);
        }
        EndText.SetText(endText);
    }


    private void SetItemsGet(bool lose)
    {
        if (lose)
        {
            m_GameModel.collectedItems_Temp.Clear();
            m_GameModel.collectedPieces_Temp.Clear();
        }
        else
        {
            m_GameModel.collectedItems.AddRange(m_GameModel.collectedItems_Temp);
            m_GameModel.collectedPieces.AddRange(m_GameModel.collectedPieces_Temp);
        }

    }

    IEnumerator CheckVideoFinish(CallBackFunc callback = null)
    {
        float time = (float)m_VideoPlayer.clip.length;
        yield return new WaitForSeconds(time);
        if (callback != null)
            callback();
    }


    void SetEndImg(string path)
    {
        EndSceneImg.sprite = Resources.Load<Sprite>("Image/Ending/" + path);
    }



    public void Restart()
    {
        m_GameModel.Reset();

        if (m_GameModel.Day == 4)
        {
            PlayCredit();
        }
        else
        {
            Game.Instance.LoadScene(2);
        }
    }

    public void SkipCredit()
    {
        Game.Instance.LoadScene(1);
    }

    public void PlayCredit()
    {
        creditPanel.gameObject.SetActive(true);
        creditPanel.OpenPanel();
    }

    public override void RegisterEvents()
    {

    }

    public override void HandleEvent(string eventName, object obj)
    {

    }


}
