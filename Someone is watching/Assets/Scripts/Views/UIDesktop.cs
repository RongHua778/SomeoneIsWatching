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
    public UIMemory m_UIMemory;
    public EmailManager m_EmailSystem;
    public GameModel m_GameModel;
    public GameObject RecordPenBtn;

    private const int ShowIndex = 10;


    public void Show()
    {
        HideMonitor();
        HideRecordPen();
        HideEmail();
        HideMemory();
    }

    public void UnlockRecordBtn(bool unlock)
    {
        m_GameModel.unlockRecord = unlock;
        RecordPenBtn.SetActive(unlock);
    }


    public void MonitorIconClick()
    {
        m_Monitor.transform.SetSiblingIndex(ShowIndex);
        if (!m_GameModel.daying)
        {
            m_Monitor.DayStart(m_GameModel.Day);
            m_GameModel.daying = true;
        }
        m_Monitor.m_UIInteractive.HideItemPanel();//关闭ItemPanel
        m_Monitor.CloseAllMonitors();
        Sound.Instance.PlayEffect("SoundEffect/Sound_ComputerOpen");

        if (!m_GameModel.guide1)//发送第一个指引
        {
            SendEvent(Const.E_AddChat, "guide01");
            m_GameModel.guide1 = true;
        }

    }

    public void RecoredPenClick()
    {
        m_RecordPen.transform.SetSiblingIndex(ShowIndex);
        Sound.Instance.PlayEffect("SoundEffect/Sound_ComputerOpen");
    }

    public void MemoryClick()
    {
        m_UIMemory.transform.SetSiblingIndex(ShowIndex);
        Sound.Instance.PlayEffect("SoundEffect/Sound_ComputerOpen");
    }

    public void EmailClick()
    {
        m_EmailSystem.transform.SetSiblingIndex(ShowIndex);
        Sound.Instance.PlayEffect("SoundEffect/Sound_ComputerOpen");

    }
    public void MemomryClick()
    {
        m_UIMemory.transform.SetSiblingIndex(ShowIndex);
        Sound.Instance.PlayEffect("SoundEffect/Sound_ComputerOpen");

    }

    public void HideMemory()
    {
        m_UIMemory.transform.SetSiblingIndex(0);
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
    }

    public void HideEmail()
    {
        m_EmailSystem.transform.SetAsFirstSibling();
        Sound.Instance.PlayEffect("SoundEffect/Sound_ComputerOpen");
    }

    public void CloseScreen()
    {
        SendEvent(Const.E_ShowPanel, 1);
        Sound.Instance.PlayEffect("SoundEffect/Sound_ComputerOpen");

    }
    // Start is called before the first frame update
    void Start()
    {
        m_GameModel = GetModel<GameModel>() as GameModel;
    }

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Const.E_UnlockRecord);
    }

    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {
            case Const.E_UnlockRecord:
                bool unlock = (bool)obj;
                UnlockRecordBtn(unlock);
                break;
        }
    }

}
