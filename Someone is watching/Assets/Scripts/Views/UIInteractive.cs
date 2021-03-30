using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIInteractive : MonoBehaviour
{
    public UIMonitor m_Monitor;
    public Transform[] m_Interactives;
    public ItemPanel ItemPanel;
    public GameObject Subtitle;
    public TextHandler subtitle_TextHandler;
    //public GameModel m_GameModel;
    //public string currentState = "";

    Image ItemImage;

    public TextHandler Message_TextHandler;
    public Text Message_Text;

    Dictionary<string, Transform> m_InterDic = new Dictionary<string, Transform>();

    public UIRecordPen recordPenPanel;
    public DigitClock digitClockPanel;
    public PieceInfo pieceInfo;
    public MultiPanel multiPanel;

    //public override string Name
    //{
    //    get { return Const.V_Interactive; }
    //}

    private void Start()
    {

        ItemImage = ItemPanel.transform.Find("Image").GetComponent<Image>();

    }
    //找到对应的交互Panel
    //public void FindAndSet(string state)
    //{
    //    foreach(string key in m_InterDic.Keys)
    //    {
    //        if (key == state)
    //        {
    //            m_InterDic[key].gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            m_InterDic[key].gameObject.SetActive(false);
    //        }
    //    }
    //}

    public void CloseAll()
    {
        foreach (string item in m_InterDic.Keys)
        {
            m_InterDic[item].gameObject.SetActive(false);
        }
    }


    void ShowItemPanel()
    {
        ItemPanel.gameObject.SetActive(true);

    }

    public void HideItemPanel()
    {
        ItemPanel.gameObject.SetActive(false);
        m_Monitor.FindAndSetInteract();
    }

    public void HideMultiPanel()
    {
        multiPanel.Close();

    }


    public void CheckAll()
    {
        D2CheckCG();
        D2CheckEnd();
    }

    void D2CheckEnd()
    {
        if (m_Monitor.m_GameModel.day2_Camera2Repair && m_Monitor.m_GameModel.day2_Camera4Repair && m_Monitor.m_GameModel.day2_WaterPungRepair)
        {
            m_Monitor.m_GameModel.GameOverState = "Day3";
            m_Monitor.m_GameModel.Day = 3;
            Game.Instance.LoadScene(3);
        }
    }

    //直接把手机亮起来
    void D2CheckCG()//检查是否已经收集齐2个电池和查看了药瓶//需要等待弱干时间
    {
        if (!m_Monitor.m_GameModel.day2_CG1)
        {
            if (m_Monitor.m_GameModel.day2_GetBattery1 && m_Monitor.m_GameModel.day2_GetBattery2 && m_Monitor.m_GameModel.day2_Tool)
            {
                VideoManager.Instance.ShowImage(10, "Day2_End", "Image/CG/MainPhoneOn", false);
                m_Monitor.m_GameModel.day2_CG1 = true;
            }
        }

    }

    void ShowSubtitle(string key)
    {
        Subtitle.SetActive(true);
        subtitle_TextHandler.SetText(key);
    }

    public void HideSubtitle()
    {
        Subtitle.SetActive(false);
    }


    public void ShowItemInBag(Item item)
    {
        switch (item.ID)
        {
            case 1:
                digitClockPanel.gameObject.SetActive(true);
                ShowSubtitle("digitclock");
                break;
            case 2:
            case 4:
            case 6:
            case 7:
            case 8:
                pieceInfo.gameObject.SetActive(true);
                pieceInfo.UpdatePieceInfo(item);
                break;

            default:
                break;
        }
    }

    public void ShowItemInBag(int id)
    {
        switch (id)
        {
            case 3:

                multiPanel.UpdatePanel(0);
                multiPanel.State = "Medicine";
                multiPanel.gameObject.SetActive(true);
                VideoManager.Instance.CloseAll();
                //CloseAll();
                break;

            case 4:
                multiPanel.UpdatePanel(1);
                multiPanel.State = "MedicineBook";
                multiPanel.gameObject.SetActive(true);
                //CloseAll();
                VideoManager.Instance.CloseAll();

                break;
            default:
                break;
        }
    }
    public void StartShowMessage(string key)
    {
        StartCoroutine(ShowMessage(key));
    }

    public IEnumerator ShowMessage(string key)
    {
        Message_TextHandler.gameObject.SetActive(true);
        Message_TextHandler.SetText(key);
        yield return new WaitForSeconds(3f);
        Message_TextHandler.gameObject.SetActive(false);
    }


    #region InteractiveFunctions_Buttons

    public void InteractiveClick(string code)
    {
        switch (code)
        {
            case "D11aProtagonist":
                m_Monitor.m_GameModel.GameOverState = "CloseCamera";
                Game.Instance.LoadScene(3);
                VideoManager.Instance.CloseAll();

                //CloseAll();
                break;

            case "D11cDesk":
                if (m_Monitor.m_GameModel.overAllState == "Day1Period3"||
                    m_Monitor.m_GameModel.overAllState == "Day1Period4")//女主离开家后才可以拿病历本
                {
                    if (!m_Monitor.m_GameModel.day1_MemoryPiece1)
                    {
                        //CloseAll();
                        VideoManager.Instance.CloseAll();

                        VideoManager.Instance.ShowImage(9, "Desk", "Image/Camera1/Day1.1d(抽屉)", false);
                        ShowItemPanel();
                    }
                    else
                    {
                        //CloseAll();
                        VideoManager.Instance.CloseAll();

                        //ShowItemPanel("Image/Camera1/Day1.1d(空抽屉)",0);

                        VideoManager.Instance.ShowImage(9, null, "Image/Camera1/Day1.1d(空抽屉)", false);
                        ShowItemPanel();
                    }
                    Sound.Instance.PlayEffect("SoundEffect/Sound_Drawer");
                }
                break;

            case "D11cPiece1":
                //CloseAll();
                VideoManager.Instance.CloseAll();

                m_Monitor.SendEvent(Const.E_GetItem, 2);
                m_Monitor.m_GameModel.day1_MemoryPiece1 = true;
                //ShowItemPanel("Image/Camera1/Day1.1d(空抽屉)",0);
                VideoManager.Instance.ShowImage(9, null, "Image/Camera1/Day1.1d(空抽屉)", false);
                ShowItemPanel();

                
                StartCoroutine(ShowMessage("tips8"));
                //Debug.Log("GetPiece1");
                break;

            case "D16aCalender":
                //ShowItemPanel("Image/Camera6/日历特写",5);
                VideoManager.Instance.ShowImage(9, null, "Image/Camera6/日历特写", false);
                ShowItemPanel();
                //VideoManager.Instance.ShowImage(9, null, "Image/Camera6/日历特写");
                //CloseAll();
                VideoManager.Instance.CloseAll();

                ShowSubtitle("calendar");
                break;

            case "D16aDigitClock":
                VideoManager.Instance.ShowImage(5, "D1-6a_k", "Image/Camera6/D1.6a_k", false);
                m_Monitor.SendEvent(Const.E_GetItem, 1);
                //CloseAll();
                break;

            case "D15aLadyBag":
                ShowSubtitle("ladybag");
                break;

            case "D13aPaperTip":
                //CloseAll();
                VideoManager.Instance.CloseAll();

                //ShowItemPanel("Image/Camera3/镜头三day1-3b（简体)",2);
                VideoManager.Instance.ShowImage(9, null, "Image/Camera3/镜头三day1-3b（简体)", false);
                ShowItemPanel();
                break;

            case "D17aRubishCan":
                if (!m_Monitor.m_GameModel.day1_GetPen)
                {
                    //CloseAll();
                    VideoManager.Instance.CloseAll();

                    //ShowItemPanel("Image/Camera7/垃圾桶内部特写",6);
                    VideoManager.Instance.ShowImage(9, "RubishCan", "Image/Camera7/垃圾桶内部特写", false);
                    ShowItemPanel();

                    //FindAndSet("RubishCan");
                }
                else
                {
                    //CloseAll();
                    VideoManager.Instance.CloseAll();

                    //ShowItemPanel("Image/Camera7/垃圾桶内部无录音笔",6);
                    VideoManager.Instance.ShowImage(9, null, "Image/Camera7/垃圾桶内部无录音笔", false);
                    ShowItemPanel();
                }
                break;

            case "D17aRecordPen":
                //CloseAll();
                VideoManager.Instance.CloseAll();

                m_Monitor.SendEvent(Const.E_GetItem, 0);
                // ShowItemPanel("Image/Camera7/垃圾桶内部无录音笔",6);
                VideoManager.Instance.ShowImage(9, null, "Image/Camera7/垃圾桶内部无录音笔", false);
                ShowItemPanel();

                m_Monitor.m_GameModel.day1_GetPen = true;
                StartCoroutine(ShowMessage("tips9"));
                //m_InterDic["D1-7a"].transform.Find("RubishCan").gameObject.SetActive(false);
                break;

            case "D11gMedicineHistory":
                //if(m_Monitor.m_GameModel.overAllState=="Day1Period5")//女主躺下后才可以拿病历本
                //{
                //    VideoManager.Instance.ShowImage(9, null, "Image/Camera1/病历本",false);
                //    ShowItemPanel();
                //}
                //ShowItemPanel("Image/Camera1/病历本",0);
                break;

            //day2
            case "D2Desk":

                //ShowItemPanel("Image/Camera1/D2卧室桌子",0);
                VideoManager.Instance.ShowImage(9, "D2Desk", "Image/Camera1/D2卧室桌子", false);
                ShowItemPanel();
                //FindAndSet("D2Desk");
                break;

            case "D2MusicBox":
                VideoManager.Instance.FindAndSet(9, "MusicBox");

                break;

            case "WatchMusicBox":
                ShowSubtitle("musicbox");
                VideoManager.Instance.FindAndSet(9, "D2Desk");
                //FindAndSet("D2Desk");
                break;

            case "PlayMusicBox":
                //HideItemPanel();
                //Segment seg0 = new Segment(9, "Video/Camera1/PlayMusicBox", "D2-1g", false, false ,null,PlayMusicBoxEnd);
                //VideoManager.Instance.PlayVideoClip(seg0);
                PlayMusicBoxEnd();
                //m_Monitor.PlayClip(seg0);
                //CloseAll();
                VideoManager.Instance.CloseAll();

                break;

            case "D2MedicineBook":
                ShowItemInBag(4);
                break;


            case "D2Closet":


                if (!m_Monitor.m_GameModel.day2_GetBattery1)
                {
                    //ShowItemPanel("Image/Camera1/D2-1g_柜子开",0);
                    VideoManager.Instance.ShowImage(9, "D2-1g_gz", "Image/Camera1/D2-1g_柜子开", false);
                    ShowItemPanel();
                    Sound.Instance.PlayEffect("SoundEffect/Sound_OpenCabinet");
                    //FindAndSet("D2-1g_gz");
                }
                break;

            case "LivingRoomBattery":
                m_Monitor.SendEvent(Const.E_GetItem, 3);
                //ShowItemPanel("Image/Camera1/D2-1g_柜子开_没电池",0);
                VideoManager.Instance.ShowImage(9, null, "Image/Camera1/D2-1g_柜子开_没电池", false);
                ShowItemPanel();
                m_Monitor.m_GameModel.day2_GetBattery1 = true;
                StartCoroutine(ShowMessage("tips10"));
                //CloseAll();
                VideoManager.Instance.CloseAll();


                //D2CheckCG();
                break;



            case "D2Capbinet":
                if (!m_Monitor.m_GameModel.day2_GetBattery2)
                {
                    //ShowItemPanel("Image/Camera3/D2.3a_开",2);
                    VideoManager.Instance.ShowImage(9, "D2-3b_k", "Image/Camera3/D2.3a_开", false);
                    ShowItemPanel();
                    Sound.Instance.PlayEffect("SoundEffect/Sound_OpenCabinet");
                    // FindAndSet("D2-3b_k");
                }
                break;

            case "WashRoomBattery":

                VideoManager.Instance.ShowImage(9, "BatteryGet", "Image/Camera3/D2.3c_电池", false);
                ShowItemPanel();
                //ShowItemPanel("Image/Camera3/D2.3c_电池",2);
                //FindAndSet("BatteryGet");

                break;

            case "GetBattery2"://WashRoomBattery
                m_Monitor.SendEvent(Const.E_GetItem, 3);
                m_Monitor.m_GameModel.day2_GetBattery2 = true;
                //ShowItemPanel("Image/Camera3/D2.3c_空",2);
                VideoManager.Instance.ShowImage(9, null, "Image/Camera3/D2.3c_空", false);
                ShowItemPanel();
                StartCoroutine(ShowMessage("tips10"));
                //D2CheckCG();
                break;

            case "D2Bottle":
                //m_Monitor.PlayClip(4,"Video/Camera5/D2.5b","D2-5b",true);
                VideoManager.Instance.FindAndSet(0, "D2-5b");
                break;

            case "D2Table":
                if (!m_Monitor.m_GameModel.day2_Piece3)
                {
                    VideoManager.Instance.ShowImage(9, "Table_Piece", "Image/Camera5/餐厅残片#3", false);
                    ShowItemPanel();
                    //ShowItemPanel("Image/Camera5/餐厅残片#3",4);
                    //FindAndSet("Table_Piece");
                }
                else
                {
                    //ShowItemPanel("Image/Camera5/餐厅残片#3_无",4);
                    VideoManager.Instance.ShowImage(9, null, "Image/Camera5/餐厅残片#3_无", false);
                    ShowItemPanel();
                }
                break;

            case "TablePiece":
                //ShowItemPanel("Image/Camera5/餐厅残片#3_无",4);
                VideoManager.Instance.ShowImage(9, null, "Image/Camera5/餐厅残片#3_无", false);
                ShowItemPanel();
                m_Monitor.SendEvent(Const.E_GetItem, 4);
                m_Monitor.m_GameModel.day2_Piece3 = true;
                StartCoroutine(ShowMessage("tips8"));
                break;

            case "D2Tools":
                if (!m_Monitor.m_GameModel.day2_Tool)
                {
                    VideoManager.Instance.ShowImage(9, "GetTool", "Image/Camera6/工具箱", false);
                    ShowItemPanel();
                    //ShowItemPanel("Image/Camera6/工具箱",5);
                    //FindAndSet("GetTool");
                }
                else
                {
                    VideoManager.Instance.ShowImage(9, null, "Image/Camera6/工具箱_空", false);
                    ShowItemPanel();
                    //ShowItemPanel("Image/Camera6/工具箱_空",5);
                }

                break;

            case "GetTool":
                VideoManager.Instance.ShowImage(9, null, "Image/Camera6/工具箱_空", false);
                ShowItemPanel();
                //ShowItemPanel("Image/Camera6/工具箱_空",5);
                m_Monitor.SendEvent(Const.E_GetItem, 5);
                m_Monitor.m_GameModel.day2_Tool = true;
                StartCoroutine(ShowMessage("tips11"));
                //CloseAll();
                VideoManager.Instance.CloseAll();

                break;


            case "D2Tips":
                VideoManager.Instance.ShowImage(9, null, "Image/Camera7/d2.7c字条", false);
                ShowItemPanel();
                // ShowItemPanel("Image/Camera7/d2.7c字条",6);
                break;

            case "D2Shelf":
                //Segment seg1 = new Segment(6, "Video/Camera7/D2.7c", "D2-7c", false,false);
                //m_Monitor.PlayClip(seg1);
                VideoManager.Instance.ShowImage(9, "D2-7c", "Image/Camera7/D2.7c", false);
                ShowItemPanel();
                Sound.Instance.PlayEffect("SoundEffect/Sound_OpenCabinet");
                //ShowItemPanel("Image/Camera7/D2.7c",6);
                //FindAndSet("D2-7c");
                break;

            case "D2ShelfLeft":
                if (!m_Monitor.m_GameModel.day2_Piece2)
                {
                    VideoManager.Instance.ShowImage(9, "ShelfLeft", "Image/Camera7/d2.7c碎片", false);
                    ShowItemPanel();
                    //ShowItemPanel("Image/Camera7/d2.7c碎片",6);
                    //FindAndSet("ShelfLeft");
                }
                else
                {
                    VideoManager.Instance.ShowImage(9, null, "Image/Camera7/d2.7c无碎片", false);
                    ShowItemPanel();
                    //ShowItemPanel("Image/Camera7/d2.7c无碎片",6);
                    //CloseAll();
                }
                
                break;

            case "D2ShelfRight":
                VideoManager.Instance.ShowImage(9, "ShelfRight", "Image/Camera7/d2.7c药物", false);
                ShowItemPanel();
                //ShowItemPanel("Image/Camera7/d2.7c药物",6);
                //FindAndSet("ShelfRight");
                break;

            case "D2Medicine":
                ShowItemInBag(3);

                m_Monitor.m_GameModel.day2_SeeNum = true;
                break;

            case "D2Piece2":
                // ShowItemPanel("Image/Camera7/d2.7c无碎片",6);
                VideoManager.Instance.ShowImage(6, "D2-7c_k", "Image/Camera7/D2.7c_k", false);
                VideoManager.Instance.ShowImage(9, null, "Image/Camera7/d2.7c无碎片", false);
                ShowItemPanel();
                m_Monitor.SendEvent(Const.E_GetItem, 6);
                m_Monitor.m_GameModel.day2_Piece2 = true;
                StartCoroutine(ShowMessage("tips8"));
                break;

            case "D2WashRoomBottle":
                m_Monitor.Day2Period2();
                Sound.Instance.PlayEffect("SoundEffect/Sound_CupFall");
                //CloseAll();
                VideoManager.Instance.CloseAll();

                break;

            case "D2WaterPung":

                if (!m_Monitor.m_GameModel.day2_WaterPungRepair)
                    StartCoroutine(ShowMessage("tips12"));
                break;

            case "D3WaterPung":
                VideoManager.Instance.ShowImage(2, "D3-3e_Y", "Image/Camera3/D3.3e_Y", false);
                Sound.Instance.PlayEffect("SoundEffect/Sound_WaterSink");
                break;

            case "D3Piece5":
                if (!m_Monitor.m_GameModel.day3_Piece5)
                {
                    VideoManager.Instance.ShowImage(2, "D3-3e_K", "Image/Camera3/D3.3e_K", false);
                    m_Monitor.m_GameModel.day3_Piece5 = true;
                    m_Monitor.SendEvent(Const.E_GetItem, 8);
                    StartCoroutine(ShowMessage("tips8"));
                }
                break;
            case "D3Piece4":
                if (!m_Monitor.m_GameModel.day3_Piece4)
                {
                    VideoManager.Instance.ShowImage(7, "D3-8a_K", "Image/Camera8/D3.8a_K", false);
                    m_Monitor.m_GameModel.day3_Piece4 = true;
                    m_Monitor.SendEvent(Const.E_GetItem, 7);
                    StartCoroutine(ShowMessage("tips8"));
                }
                break;

            case "D3ShelfTop":
                VideoManager.Instance.ShowImage(9, "D3-4a_TOP", "Image/Camera4/D3.4a_TOP", false);
                ShowItemPanel();
                break;

            case "D3ShelfBook":
                VideoManager.Instance.ShowImage(9, "D3-4a_TOP_BOOK", "Image/Camera4/D3.4a_TOP_BOOK", false);
                Sound.Instance.PlayEffect("SoundEffect/Sound_TakeBook");
                //ShowItemPanel();
                break;

            case "D3Book":
                VideoManager.Instance.ShowImage(9, "D3-4a_TOP_BOOK_OPEN", "Image/Camera4/D3.4a_TOP_BOOK_OPEN", false);
                Sound.Instance.PlayEffect("SoundEffect/Sound_Flip");

                //ShowItemPanel();
                break;

            case "D3Paint":
                VideoManager.Instance.ShowImage(9, "D3-2c_PAINT", "Image/Camera2/D3.2c_PAINT", false);
                ShowItemPanel();
                break;

            case "D41dBed":
                VideoManager.Instance.ShowImage(0, "D4-1d_A", "Image/Camera1/D4.1d_A", false);
                break;

            case "D41dBed_C":
                VideoManager.Instance.ShowImage(0, "D4-1d_C", "Image/Camera1/D4.1d_C", false);
                break;

            case "D41dBed_C_O":
                if (!m_Monitor.m_GameModel.day4_SDcard)
                {
                    VideoManager.Instance.ShowImage(9, "D4-1d_C_O", "Image/Camera1/D4.1d_C_O", false);
                    ShowItemPanel();
                }
                break;
            case "D41dBed_C_O_CameraCard":
                //getcard
                m_Monitor.SendEvent(Const.E_GetItem, 10);
                StartCoroutine(ShowMessage("tips13"));
                m_Monitor.m_GameModel.day4_SDcard = true;
                HideItemPanel();
                break;


            case "D41dBrain":
                VideoManager.Instance.ShowImage(9, "D4-1d_Brain", "Image/Camera1/D4.1d_Brain", false);
                ShowItemPanel();
                break;

            case "D41d_Brain_BlueMap":
                if (!m_Monitor.m_GameModel.day4_BlueMap)
                {
                    m_Monitor.m_GameModel.day4_BlueMap = true;
                    m_Monitor.SendEvent(Const.E_GetItem, 9);
                    StartCoroutine(ShowMessage("tips14"));
                }

                break;

            case "D42d_Y":
                VideoManager.Instance.ShowImage(1, "D4-2d_Y", "Image/Camera2/D4.2d_Y", false);
                break;
            case "D42d_K":
                VideoManager.Instance.ShowImage(1, "D4-2d_K", "Image/Camera2/D4.2d_K", false);
                if (!m_Monitor.m_GameModel.day4_Piece6)
                {
                    m_Monitor.m_GameModel.day4_Piece6 = true;
                    m_Monitor.SendEvent(Const.E_GetItem, 11);
                    StartCoroutine(ShowMessage("tips8"));
                }
                break;

            case "D46b_A":
                if(!m_Monitor.m_GameModel.day4_Piece7)
                    VideoManager.Instance.ShowImage(5, "D4-6b_A", "Image/Camera6/D4.6b_A", false);
                break;
            case "D46b_B":
                if (!m_Monitor.m_GameModel.day4_Piece7)
                    VideoManager.Instance.ShowImage(5, "D4-6b_B", "Image/Camera6/D4.6b_B", false);
                break;
            case "D46b_C":
                if (!m_Monitor.m_GameModel.day4_Piece7)
                    VideoManager.Instance.ShowImage(5, "D4-6b_C", "Image/Camera6/D4.6b_C", false);
                break;
            case "D46b_A_Y":
                VideoManager.Instance.ShowImage(5, "D4-6b", "Image/Camera6/D4.6b", false);
                if (!m_Monitor.m_GameModel.day4_Piece7)
                {
                    m_Monitor.m_GameModel.day4_Piece7 = true;
                    m_Monitor.SendEvent(Const.E_GetItem, 12);
                    StartCoroutine(ShowMessage("tips8"));
                }
                break;

        }
        //Sound.Instance.PlayEffect("SoundEffect/Sound_Interact");

    }

    void PlayMusicBoxEnd()
    {
        m_Monitor.m_GameModel.GameOverState = "PlayMusicBox";
        Game.Instance.LoadScene(3);
        VideoManager.Instance.CloseAll();

        //CloseAll();
    }

    #endregion

}
