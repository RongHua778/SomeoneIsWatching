using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;


public class UIFunctionManager : View
{
    public override string Name
    {
        get { return Const.V_Function; }
    }
    public PostProcessVolume Volume;
    public GameModel m_GameModel;

    public UIRealworld m_RealWorld;
    public UIDesktop m_Desktop;
    public UIPhone m_UIPhone;

    [SerializeField] GameObject NextDayBtnObj = default;
    private void Start()
    {
        m_GameModel = GetModel<GameModel>() as GameModel;
        DayStart(m_GameModel.Day);
        Sound.Instance.PlayBg("BGMusic/GameMusic",0.1f);
        //Debug.Log(m_GameModel.Day);
    }

    public void DayStart(int day)
    {
        switch (day)
        {
            case 1:
                ShowPanel(1);
                SendEvent(Const.E_AddRecord, 1);
                SendEvent(Const.E_AddRecord, 2);
                SendEvent(Const.E_AddRecord, 3);
                Segment seg1 = new Segment(10, "Video/Animation/Opening_1", "Opening", false, false, null, Day1Openning);
                Segment seg2 = new Segment(10, "Video/Animation/Opening_2", "Opening", false, true);

                VideoManager.Instance.PlaySeq(new List<Segment> { seg1, seg2 });

                break;

            case 2:
                ShowPanel(1);
                Sound.Instance.PlayEffect("SoundEffect/Sound_Knock");
                SendEvent(Const.E_UnlockRecord, true);
                SendEvent(Const.E_GetItem, 1);//获得时钟
                SendEvent(Const.E_AddPiece, 2);//获得记忆碎片1
                SendEvent(Const.E_AddRecord, 1);
                SendEvent(Const.E_AddRecord, 2);
                SendEvent(Const.E_AddRecord, 3);
                VideoManager.Instance.ShowImage(10, "Day2_Desk", "Image/CG/Day2_Desk", false);
                if (!m_GameModel.guide8)
                {
                    SendEvent(Const.E_AddChat, "guide08");
                    m_GameModel.guide8 = true;
                }

                break;

            case 3:
                ShowPanel(1);
                VideoManager.Instance.ShowImage(10, "Day2_Desk", "Image/CG/Day2_Desk", false);
                SendEvent(Const.E_UnlockRecord, true);
                SendEvent(Const.E_AddRecord, 1); 
                SendEvent(Const.E_AddRecord, 2); 
                SendEvent(Const.E_AddRecord, 3);
                SendEvent(Const.E_AddRecord, 4);
                SendEvent(Const.E_AddPiece, 2);
                SendEvent(Const.E_AddPiece, 6);
                SendEvent(Const.E_AddPiece, 4);

                break;

            case 4:
                ShowPanel(1);
                VideoManager.Instance.ShowImage(10, "Day2_Desk", "Image/CG/Day2_Desk", false);
                //SendEvent(Const.E_AddRecord, 0);
                //SendEvent(Const.E_AddRecord, 4);//新增录音片段
                SendEvent(Const.E_AddPiece, 2);
                SendEvent(Const.E_AddPiece, 4);
                SendEvent(Const.E_AddPiece, 6);
                SendEvent(Const.E_AddPiece, 8);
                SendEvent(Const.E_SendEmail, 4);
                SendEvent(Const.E_SendEmail, 5);

                break;

        }
    }

    void Day1Openning()
    {
        SendEvent(Const.E_TriggerDialogue, 0);
    }

    public void ShowPanel(int id)
    {
        switch (id)
        {
            case 0:
                Volume.weight = 1;
                m_RealWorld.transform.SetAsFirstSibling();
                Sound.Instance.PlayEffect("SoundEffect/Sound_ComputerOpen");
                break;
            case 1:
                Volume.weight = 0.3f;
                m_Desktop.transform.SetAsFirstSibling();
                m_Desktop.Show();
                SendEvent(Const.E_DayEndCheck,m_GameModel.DayEndCheck());
                break;
        }


    }


    public override void RegisterEvents()
    {
        AttentionEvents.Add(Const.E_ShowPanel);
        AttentionEvents.Add(Const.E_DayEndCheck);

        AttentionEvents.Add(Const.E_DialogEnd);
    }

    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {
            case Const.E_ShowPanel:
                int id = (int)obj;
                ShowPanel(id);
                
                break;
            case Const.E_DayEndCheck:
                bool check = (bool)obj;
                DayEndCheck(check);
                break;
            case Const.E_DialogEnd:
                m_UIPhone.ShowCloseBtn(true);
                break;
        }
    }

    public void Test(int day)
    {
        m_GameModel.Day = day;
    }

    public void DayPass()//test only
    {
        m_GameModel.NextDay(true);
        Game.Instance.LoadScene(3);
    }

    public void DayEndCheck(bool check)
    {
        if (check)
        {
            NextDayBtnObj.SetActive(true);
            NextDayBtnObj.GetComponentInChildren<TextHandler>().SetText("nextday");
        }
        else
        {
            NextDayBtnObj.SetActive(false);
        }
    }

    public void NextDayBtn()
    {
        m_GameModel.NextDay();
    }

}
