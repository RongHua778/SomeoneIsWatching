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

    Image ItemImage;

    public TextHandler Message_TextHandler;
    public Text Message_Text;


    public UIRecordPen recordPenPanel;
    public DigitClock digitClockPanel;
    public MultiPanel multiPanel;


    private void Start()
    {

        ItemImage = ItemPanel.transform.Find("Image").GetComponent<Image>();

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
        D2Check();
    }

    //直接把手机亮起来
    void D2Check()//检查是否已经收集齐2个电池和查看了药瓶//需要等待弱干时间
    {
        if (m_Monitor.m_GameModel.day2_GetBattery1 && m_Monitor.m_GameModel.day2_GetBattery2 && m_Monitor.m_GameModel.day2_Tool)
        {
            m_Monitor.m_GameModel.overAllState = "Day2Period3";
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
                break;

            case 4:
                multiPanel.UpdatePanel(1);
                multiPanel.State = "MedicineBook";
                multiPanel.gameObject.SetActive(true);
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
                break;

            case "D11cDesk":
                if (m_Monitor.m_GameModel.overAllState == "Day1Period3"||
                    m_Monitor.m_GameModel.overAllState == "Day1Period4")//女主离开家后才可以拿病历本
                {
                    if (!m_Monitor.m_GameModel.day1_MemoryPiece1)
                    {
                        VideoManager.Instance.CloseAll();

                        VideoManager.Instance.ShowImage(9, "Desk", "Image/Camera1/Day1.1d(抽屉)", false);
                        ShowItemPanel();
                    }
                    else
                    {
                        VideoManager.Instance.CloseAll();
                        VideoManager.Instance.ShowImage(9, null, "Image/Camera1/Day1.1d(空抽屉)", false);
                        ShowItemPanel();
                    }
                    Sound.Instance.PlayEffect("SoundEffect/Sound_Drawer");
                }
                break;

            case "D11cPiece1":
                VideoManager.Instance.CloseAll();
                m_Monitor.SendEvent(Const.E_AddPiece, 2);
                m_Monitor.m_GameModel.day1_MemoryPiece1 = true;
                VideoManager.Instance.ShowImage(9, null, "Image/Camera1/Day1.1d(空抽屉)", false);
                ShowItemPanel();
                StartCoroutine(ShowMessage("tips8"));
                break;

            case "D16aCalender":

                VideoManager.Instance.ShowImage(9, null, "Image/Camera6/日历特写", false);
                ShowItemPanel();
                VideoManager.Instance.CloseAll();

                ShowSubtitle("calendar");
                break;

            case "D16aDigitClock":
                VideoManager.Instance.ShowImage(5, "D1-6a_k", "Image/Camera6/D1.6a_k", false);
                m_Monitor.SendEvent(Const.E_GetItem, 1);
                break;

            case "D15aLadyBag":
                ShowSubtitle("ladybag");
                break;

            case "D15fLadyBag":
                if (!m_Monitor.m_GameModel.unlockRecord)
                {
                    VideoManager.Instance.ShowImage(9, "D1-5f_Y", "Image/Camera5/D1.5f_y", false);
                }
                else
                    VideoManager.Instance.ShowImage(9, "D1-5f_K", "Image/Camera5/D1.5f_k", false);
                ShowItemPanel();
                break;

            case "D15fRecordPen":
                VideoManager.Instance.ShowImage(9, "D1-5f_K", "Image/Camera5/D1.5f_k", false);
                m_Monitor.SendEvent(Const.E_UnlockRecord, true);
                m_Monitor.SendEvent(Const.E_ShowMessage, "tips17");
                if (!m_Monitor.m_GameModel.guide4)
                {
                    m_Monitor.SendEvent(Const.E_AddChat, "guide04");
                    m_Monitor.m_GameModel.guide4 = true;
                }
                VideoManager.Instance.CloseAll();
                break;

            case "D13aPaperTip":
                VideoManager.Instance.CloseAll();
                VideoManager.Instance.ShowImage(9, null, "Image/Camera3/镜头三day1-3b（简体)", false);
                ShowItemPanel();
                ShowSubtitle("toilettip");
                break;

            case "D17aRubishCan":
                if (!m_Monitor.m_GameModel.day1_GetPen)
                {
                    VideoManager.Instance.CloseAll();
                    VideoManager.Instance.ShowImage(9, "RubishCan", "Image/Camera7/垃圾桶内部特写", false);
                    ShowItemPanel();
                }
                else
                {
                    VideoManager.Instance.CloseAll();
                    VideoManager.Instance.ShowImage(9, null, "Image/Camera7/垃圾桶内部无录音笔", false);
                    ShowItemPanel();
                }
                break;

            case "D17aRecordPen":
                VideoManager.Instance.CloseAll();
                m_Monitor.SendEvent(Const.E_GetItem, 0);
                VideoManager.Instance.ShowImage(9, null, "Image/Camera7/垃圾桶内部无录音笔", false);
                ShowItemPanel();

                m_Monitor.m_GameModel.day1_GetPen = true;
                StartCoroutine(ShowMessage("tips9"));
                //m_InterDic["D1-7a"].transform.Find("RubishCan").gameObject.SetActive(false);
                break;

            case "D11gMedicineHistory":

                break;

            //day2
            case "D2Desk":

                VideoManager.Instance.ShowImage(9, "D2Desk", "Image/Camera1/D2卧室桌子", false);
                ShowItemPanel();
                break;

            case "D2MusicBox":
                Sound.Instance.PlayEffect("SoundEffect/Music_MusicBox");
                Segment seg = new Segment(9, "Video/Ending/PlayMusicBox", "", false, false,null,PlayMusicBoxEnd);
                VideoManager.Instance.PlayVideoClip(seg);
                ShowItemPanel();
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
                CheckAll();
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
                CheckAll();
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
                m_Monitor.SendEvent(Const.E_AddPiece, 4);
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
                CheckAll();
                StartCoroutine(ShowMessage("tips11"));
                //CloseAll();
                VideoManager.Instance.CloseAll();

                break;


            case "D2Tips":
                VideoManager.Instance.ShowImage(9, null, "Image/Camera7/d2.7c字条", false);
                ShowItemPanel();
                ShowSubtitle("kitchentip");
                break;

            case "D2Shelf":
                VideoManager.Instance.ShowImage(9, "D2-7c", "Image/Camera7/D2.7c", false);
                ShowItemPanel();
                Sound.Instance.PlayEffect("SoundEffect/Sound_OpenCabinet");

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
                ShowSubtitle("manymedicine");
                //ShowItemPanel("Image/Camera7/d2.7c药物",6);
                //FindAndSet("ShelfRight");
                break;

            case "D2Medicine":
                //ShowItemInBag(3);
                VideoManager.Instance.ShowImage(9, "Medicine", "Image/Camera7/Medicine#2", false);
                ShowItemPanel();
                break;

            case "D2Piece2":
                // ShowItemPanel("Image/Camera7/d2.7c无碎片",6);
                VideoManager.Instance.ShowImage(6, "D2-7c_k", "Image/Camera7/D2.7c_k", false);
                VideoManager.Instance.ShowImage(9, null, "Image/Camera7/d2.7c无碎片", false);
                ShowItemPanel();
                m_Monitor.SendEvent(Const.E_AddPiece, 6);
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
                    m_Monitor.SendEvent(Const.E_AddPiece, 8);
                    StartCoroutine(ShowMessage("tips8"));
                }
                break;
            case "D3Piece4":
                if (!m_Monitor.m_GameModel.day3_Piece4)
                {
                    VideoManager.Instance.ShowImage(7, "D3-8a_K", "Image/Camera8/D3.8a_K", false);
                    m_Monitor.m_GameModel.day3_Piece4 = true;
                    m_Monitor.SendEvent(Const.E_AddPiece, 7);
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
    }

    #endregion

}
