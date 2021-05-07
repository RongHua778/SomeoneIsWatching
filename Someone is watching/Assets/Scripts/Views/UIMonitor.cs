using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CallBackFunc();

public class UIMonitor : View
{

    public GameModel m_GameModel;
    public UIInteractive m_UIInteractive;
    public override string Name
    {
        get { return Const.V_Monitor; }
    }

    public MonitorGrid[] m_Monitors;

    private void Awake()
    {
        m_GameModel = GetModel<GameModel>() as GameModel;
        foreach (MonitorGrid monitor in m_Monitors)
        {
            monitor.m_UIMonitor = this;
        }
    }
    private void Start()
    {
        this.transform.SetAsFirstSibling();
    }
    public void CloseAllMonitors()
    {
        foreach(MonitorGrid mo in m_Monitors)
        {
            if (mo.isAugmented)
                mo.Augmented();
        }
    }

    public void ActiveMonitor(int id,bool active)
    {
        m_Monitors[id].ActiveIt(active);
    }

    public void FindAndSetInteract()
    {
        foreach(MonitorGrid mo in m_Monitors)
        {
            if (mo.isAugmented)
            {
                VideoManager.Instance.FindAndSet(0,mo.State);
            }
        }
    }


    public override void RegisterEvents()
    {
        AttentionEvents.Add(Const.E_OpenMonitor);

        AttentionEvents.Add(Const.E_Augmented);
        AttentionEvents.Add(Const.E_ItemGrid);
        AttentionEvents.Add(Const.E_ShowMessage);

        AttentionEvents.Add(Const.E_Repair);

    }

    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {

            case Const.E_Augmented:
                string e1 = obj as string;
                if (e1 == "Close")
                {
                    m_UIInteractive.HideSubtitle();
                    VideoManager.Instance.CloseAll();
                    m_UIInteractive.CheckAll();
                }
                else
                {
                    VideoManager.Instance.FindAndSet(0,e1);
                }
               
                break;

            case Const.E_ItemGrid:
                Item item = obj as Item;
                m_UIInteractive.ShowItemInBag(item);
                break;

            case Const.E_ShowMessage:
                string key = obj as string;
                m_UIInteractive.StartShowMessage(key);
                break;

            case Const.E_Repair:
                int id = (int)obj;
                switch (id)
                {
                    case 1:
                        Segment seg0 = new Segment(1, "Video/Camera2/D2.2a", "D2-2a", false, false);
                        VideoManager.Instance.PlayVideoClip(seg0);
                        SendEvent(Const.E_ShowMessage, "tips7");
                        break;
                    case 3:
                       
                        Segment seg2 = new Segment(3, "Video/Camera4/D2.4a", "D2-4a", false, false);
                        VideoManager.Instance.PlayVideoClip(seg2);
                        SendEvent(Const.E_ShowMessage, "tips7");
                        break;
                }
                m_Monitors[id].ActiveIt(true);
                m_Monitors[id].CanRepair = false;
                break;
        }
    }




    public void DayStart(int day)
    {
        Debug.Log("StartDay" + day);
        switch (day)
        {
            case 1:
                m_GameModel.overAllState = "Day1Period1";
                ActiveMonitor(0, true);
                ActiveMonitor(1, false);
                ActiveMonitor(2, true);
                ActiveMonitor(3, false);
                ActiveMonitor(4, true);
                ActiveMonitor(5, true);
                ActiveMonitor(6, true);
                ActiveMonitor(7, true);
                ActiveMonitor(8, false);

                m_Monitors[1].CanRepair = true;
                m_Monitors[3].CanRepair = true;

                Segment seg1 = new Segment(0, "Video/Camera1/D1.1a", "D1-1a", true,false);
                Segment seg2 = new Segment(2, "Video/Camera3/D1.3a", "D1-3a", false, true);
                Segment seg3 = new Segment(4, "Video/Camera5/D1.5a", "D1-5a", false, true);
                Segment seg4 = new Segment(5, "Video/Camera6/D1.6a", "D1-6a", false, true);
                Segment seg5 = new Segment(6, "Video/Camera7/D1.7a", "D1-7a", false, true);
                Segment seg6 = new Segment(7, "Video/Camera8/D1.8a", "D1-8a", false, true);

                VideoManager.Instance.PlayVideoClip(seg1);
                VideoManager.Instance.PlayVideoClip(seg2);
                VideoManager.Instance.PlayVideoClip(seg3);
                VideoManager.Instance.PlayVideoClip(seg4);
                VideoManager.Instance.PlayVideoClip(seg5);
                VideoManager.Instance.PlayVideoClip(seg6);
                break;

            case 2:
                m_GameModel.overAllState = "Day2Period1";
                ActiveMonitor(0, true);
                ActiveMonitor(1, false);
                ActiveMonitor(2, true);
                ActiveMonitor(3, false);
                ActiveMonitor(4, true);
                ActiveMonitor(5, true);
                ActiveMonitor(6, true);
                ActiveMonitor(7, true);
                ActiveMonitor(8, false);

                m_Monitors[1].CanRepair = true;
                m_Monitors[3].CanRepair = true;

                Segment seg21 = new Segment(0, "Video/Camera1/D2.1a", "D2-1a", true,true);
                //ShowImage(2, "Image/Camera3/D2.3a_空", "D2-3a",false);
                Segment seg24 = new Segment(4, "Video/Camera5/D2.5a", "D2-5a", false,true);
                Segment seg25 = new Segment(5, "Video/Camera6/D2.6a", "D2-6a", false, true);
                Segment seg26 = new Segment(6, "Video/Camera7/D2.7a", "D2-7a", false, true);
                Segment seg27 = new Segment(7, "Video/Camera8/D1.8a", "D2-8a", false, true);
                VideoManager.Instance.ShowImage(2, "D2-3a", "Image/Camera3/D2.3a_空",false);
                VideoManager.Instance.PlayVideoClip(seg21);
                VideoManager.Instance.PlayVideoClip(seg24);
                VideoManager.Instance.PlayVideoClip(seg25);
                VideoManager.Instance.PlayVideoClip(seg26);
                VideoManager.Instance.PlayVideoClip(seg27);
                break;

            case 3:
                m_GameModel.overAllState = "Day3Period1";
                ActiveMonitor(0, true);
                ActiveMonitor(1, true);
                ActiveMonitor(2, true);
                ActiveMonitor(3, true);
                ActiveMonitor(4, true);
                ActiveMonitor(5, true);
                ActiveMonitor(6, true);
                ActiveMonitor(7, true);
                ActiveMonitor(8, false);
                VideoManager.Instance.ShowImage(0, "D3-1a", "Image/Camera1/D3.1a", true);
                VideoManager.Instance.ShowImage(1, "D3-2c", "Image/Camera2/D3.2c", false);
                VideoManager.Instance.ShowImage(2, "D3-3a", "Image/Camera3/D3.3a", false);
                VideoManager.Instance.ShowImage(3, "D3-4a", "Image/Camera4/D3.4a", false);
                VideoManager.Instance.ShowImage(4, "D3-5a", "Image/Camera5/D3.5a", false);
                VideoManager.Instance.ShowImage(5, "D3-6a", "Image/Camera6/D3.6a", false);
                VideoManager.Instance.ShowImage(6, "D3-7a", "Image/Camera7/D3.7a", false);
                VideoManager.Instance.ShowImage(7, "D3-8a_Y", "Image/Camera8/D3.8a_Y", false);


                break;

            case 4:
                m_GameModel.overAllState = "Day4Period1";
                ActiveMonitor(0, true);
                ActiveMonitor(1, true);
                ActiveMonitor(2, true);
                ActiveMonitor(3, true);
                ActiveMonitor(4, true);
                ActiveMonitor(5, true);
                ActiveMonitor(6, true);
                ActiveMonitor(7, true);
                ActiveMonitor(8, false);
                VideoManager.Instance.ShowImage(0, "D4-1d", "Image/Camera1/D4.1d", false);
                VideoManager.Instance.ShowImage(1, "D4-2d", "Image/Camera2/D4.2d", false);
                VideoManager.Instance.ShowImage(2, "D4-3a", "Image/Camera3/D4.3a", false);
                VideoManager.Instance.ShowImage(3, "D4-4a", "Image/Camera4/D4.4a", false);
                VideoManager.Instance.ShowImage(4, "D4-5a", "Image/Camera5/D4.5a", false);
                VideoManager.Instance.ShowImage(5, "D4-6b", "Image/Camera6/D4.6b", false);
                VideoManager.Instance.ShowImage(6, "D4-7a", "Image/Camera7/D4.7a", false);
                VideoManager.Instance.ShowImage(7, "D4-8a", "Image/Camera8/D4.8a", false);


                break;

        }

        
    }

    public void Day1Period2()
    {
        m_GameModel.overAllState = "Day1Period2";
        Segment seg1 = new Segment(0, "Video/Camera1/D1.1c-睡着1", "D1-1c",true, false,null,Day1Period2_2);//可通过CallBack开启新的序列
        Segment seg2 = new Segment(4, "Video/Camera5/D1.5e有包", "D1-5e", true, false,null,Day1Period2_3);
        //PlayClip(new List<Segment> { seg1, seg2 });
        VideoManager.Instance.PlaySeq(new List<Segment> { seg1, seg2 });

    }
    void Day1Period2_2()
    {
        VideoManager.Instance.ShowImage(0, "D1-1c_k", "Image/Camera1/D1.1c_k",false);
        m_GameModel.overAllState = "Day1Period3";
        //ShowImage(0,"Image/Camera1/D1.1c_k","D1-1c",false);
    }
    void Day1Period2_3()
    {
        m_GameModel.overAllState = "Day1Period4";
        VideoManager.Instance.ShowImage(4, "D1-5e", "Image/Camera5/D1.5e_k",false);
        //ShowImage(4, "Image/Camera5/D1.5e_k", "D1-5e", false);
    }


    public void Day1Period5()
    {
        this.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
        m_GameModel.overAllState = "Day1Period5";
        Segment seg0 = new Segment(0, "Video/Camera1/D1.1g（空镜）", "D1-1g", false, false);
        //PlayClip(seg0);
        Segment seg1 = new Segment(4, "Video/Camera5/D1.5f", "D1-5f", true, false,null,Day1Period5_2);
        Segment seg2 = new Segment(0, "Video/Camera1/D1.1g", "D1-1g", true, false,null,Day1Period6);
        //PlayClip(new List<Segment> { seg1, seg2 });
        Segment seg3 = new Segment(5, "Video/Camera6/D1.6c", "D1-6c", false, false);
        Segment seg4 = new Segment(7, "Video/Camera8/D1.8c", "D1-8c", false, false);
        VideoManager.Instance.PlayVideoClip(seg0);
        VideoManager.Instance.PlaySeq(new List<Segment> { seg1,seg2});
        VideoManager.Instance.PlayVideoClip(seg3);
        VideoManager.Instance.PlayVideoClip(seg4);

        //PlayClip(seg3);
        //PlayClip(seg4);
    }

    void Day1Period5_2()
    {
        VideoManager.Instance.ShowImage(4, "D1-5f_b", "Image/Camera5/D1.5f_b", false);
    }

    void Day1Period6()
    {
        m_GameModel.overAllState = "Day1Period6";
    }

    void Day2Start()
    {
        //SendEvent()
    }

    public void Day2Period2()
    {
        m_GameModel.overAllState = "Day2Period2";

        Segment seg0 = new Segment(2, "Video/Camera3/D2.3g", "D2-3g",false, true);
        VideoManager.Instance.PlayVideoClip(seg0);
        //PlayClip(seg0);

        Segment seg1 = new Segment(0, "Video/Camera1/D2.1h", "D2-1h", false, false);
        Segment seg2 = new Segment(2, "Video/Camera3/D2.3h", "D2-3h", true, false,null,Day2Period2_End);
        Segment seg3 = new Segment(0, "Video/Camera1/D2.1g", "D2-1g", true, false);

        VideoManager.Instance.PlaySeq(new List<Segment> { seg1, seg2, seg3 });
        //Segment seg4 = new Segment(0, "Video/Camera1/D2.1a", "D2-1a", true, true);
        //PlayClip(new List<Segment> { seg1, seg2, seg3 });
        StartCoroutine(m_UIInteractive.ShowMessage("tips15"));

    }

    public void Day2Period2_End()
    {

        VideoManager.Instance.ShowImage(2, "D2-3a", "Image/Camera3/D2.3a_空",false);
        //ShowImage(2, "Image/Camera3/D2.3a_空", "D2-3a",false);
        //PlayClip(0, "Video/Camera1/D2.1g", "D2-1g", false, "Video/Camera1/D2.1a", "D2-1a", true, 0, null, "Day2Period1");
    }

    public void Day2Period4()//第二天夜
    {
        VideoManager.Instance.StopAll();
        m_GameModel.overAllState = "Day2Period4";
        Segment seg0 = new Segment(0, "Video/Camera1/D2.1i", "D2-1i", true, false);
        //PlayClip(seg0);
        Segment seg1 = new Segment(2, "Video/Camera3/D2.3i", "D2-3i", false, false);
        //PlayClip(seg1);
        Segment seg2 = new Segment(4, "Video/Camera5/D2.5i", "D2-5i", false, false);
        //PlayClip(seg2);
        Segment seg3 = new Segment(5, "Video/Camera6/D2.6i", "D2-6i", false, false);
       // PlayClip(seg3);
        Segment seg4 = new Segment(6, "Video/Camera7/D2.7i", "D2-7i", false, false);
        //PlayClip(seg4);
        Segment seg5 = new Segment(7, "Video/Camera8/D2.8i", "D2-8i", false, false);
       // PlayClip(seg5);
        VideoManager.Instance.PlayVideoClip(seg0);
        VideoManager.Instance.PlayVideoClip(seg1);
        VideoManager.Instance.PlayVideoClip(seg2);
        VideoManager.Instance.PlayVideoClip(seg3);
        VideoManager.Instance.PlayVideoClip(seg4);
        VideoManager.Instance.PlayVideoClip(seg5);



    }


}
