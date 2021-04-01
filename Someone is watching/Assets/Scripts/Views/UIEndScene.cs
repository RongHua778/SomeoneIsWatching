using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class UIEndScene : View
{
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
                endText = "day4";
                SetEndImg("Pieces");
                break;

            case "PlayMusicBox":
                m_VideoPlayer.clip = Resources.Load<VideoClip>("Video/Ending/PlayMusicBox");
                m_VideoPlayer.Play();
                StartCoroutine(CheckVideoFinish(PlayMusicBoxEnd));
                EndTextTrans.gameObject.SetActive(false);
                MainBtn.gameObject.SetActive(false);
                endText = "ending4";
                Sound.Instance.PlayEffect("SoundEffect/Music_MusicBox");

                break;
            case "DeadManNoSay":
                endText = "ending1";
                Sound.Instance.PlayBg("BGMusic/GameOverMusic", 1f);
                SetEndImg("CannotSpeak");
                break;

        }
        if (!lose)
        {
            MainBtnTxt.SetText("next");
            SetItemsGet(false);
        }
        else
        {
            MainBtnTxt.SetText("restart");
            SetItemsGet(true);
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


    void PlayMusicBoxEnd()
    {
        EndTextTrans.gameObject.SetActive(true);
        MainBtn.gameObject.SetActive(true);
    }

    public void Restart()
    {
        m_GameModel.Reset();

        GridUI.OnLeftBeginDrag = null;
        GridUI.OnLeftEndDrag = null;
        GridUI.OnLeftClick = null;
        EmailGrid.OnLeftClick = null;
        //Game.Instance.LoadScene(2);
        Game.Instance.LoadScene(2);
    }

    public override void RegisterEvents()
    {

    }

    public override void HandleEvent(string eventName, object obj)
    {

    }


}
