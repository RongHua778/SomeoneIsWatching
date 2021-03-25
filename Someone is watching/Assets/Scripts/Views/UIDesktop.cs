using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDesktop : View
{
    public override string Name
    {
        get { return Const.V_Desktop; }
    }

    public UIMonitor m_Monitor;
    public UIRecordPen m_RecordPen;
    public EmailManager m_EmailSystem;
    public GameModel m_GameModel;

    public void Show()
    {
        HideMonitor();
    }


    public void MonitorIconClick()
    {
        m_Monitor.transform.SetSiblingIndex(8);
        if (!m_GameModel.daying)
        {
            m_Monitor.DayStart(m_GameModel.Day);
            m_GameModel.daying = true;
        }
        m_Monitor.m_UIInteractive.HideItemPanel();//关闭ItemPanel
        m_Monitor.CloseAllMonitors();
        Sound.Instance.PlayEffect("SoundEffect/Sound_ComputerOpen");
        //m_Monitor.DayStart(m_GameModel.Day);
    }

    public void RecoredPenClick()
    {
        m_RecordPen.transform.SetSiblingIndex(8);
        Sound.Instance.PlayEffect("SoundEffect/Sound_ComputerOpen");
    }

    public void EmailClick()
    {
        m_EmailSystem.transform.SetSiblingIndex(8);
        Sound.Instance.PlayEffect("SoundEffect/Sound_ComputerOpen");

    }

    public void HideRecordPen()
    {
        m_RecordPen.transform.SetSiblingIndex(0);
        Sound.Instance.PlayEffect("SoundEffect/Sound_ComputerOpen");

    }

    public void RepairingClick()
    {
        SendEvent(Const.E_ShowMessage, "监视程序修复中...");
    }

    public void HideMonitor()
    {
        m_Monitor.transform.SetSiblingIndex(0);
        m_Monitor.m_UIInteractive.CheckAll();
        Sound.Instance.PlayEffect("SoundEffect/Sound_ComputerOpen");

        //Debug.Log("Tes00");
    }

    public void CloseScreen()
    {
        //if (m_GameModel.AllowExitToReal)
        SendEvent(Const.E_ShowPanel, 1);
        Sound.Instance.PlayEffect("SoundEffect/Sound_ComputerOpen");

        //else
        //SendEvent(Const.E_ShowMessage, "未知错误，无法退出");
    }
    // Start is called before the first frame update
    void Start()
    {
        m_GameModel = GetModel<GameModel>() as GameModel;

    }

    // Update is called once per frame
    void Update()
    {

    }


    public override void RegisterEvents()
    {
        
    }

    public override void HandleEvent(string eventName, object obj)
    {
        
    }

}
