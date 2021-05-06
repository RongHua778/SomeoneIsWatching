using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MonitorGrid : MonoBehaviour
{
    public bool IsActive;
    public UIMonitor m_UIMonitor;
    public string State;
    public bool Protagonist = false;
    //public delegate void CallBack();
    public bool isAugmented = false;
    public Image ImageCanRepair;

    float x;
    float y;
    Image m_ImageBG;
    RectTransform m_RectTransfrom;
    Image battery;
    Button m_AugmentedBtn;

    bool m_CanRepair = false;
    public bool CanRepair
    {
        get { return m_CanRepair; }
        set
        {
            m_CanRepair = value;
            if (ImageCanRepair == null)
                return;
            if (m_CanRepair)
                ImageCanRepair.gameObject.SetActive(true);
            else
                ImageCanRepair.gameObject.SetActive(false);

        }
    }



    protected void Awake()
    {
        //base.Awake();
        battery = this.transform.Find("Battery").GetComponent<Image>();
        m_AugmentedBtn = this.transform.Find("Augmented").GetComponent<Button>();
        m_RectTransfrom = this.GetComponent<RectTransform>();
        m_ImageBG = this.transform.Find("Image").GetComponent<Image>();

        x = m_RectTransfrom.anchoredPosition.x;
        y = m_RectTransfrom.anchoredPosition.y;
    }


    public void ActiveIt(bool active)
    {
        if (active == false)
        {
            IsActive = active;
            //m_ImageBG.gameObject.SetActive(false);
            m_ImageBG.sprite = Resources.Load<Sprite>("Image/UI/Monitor_NoSignal");
            battery.sprite = Resources.Load<Sprite>("Image/Battery_Low");
            m_AugmentedBtn.gameObject.SetActive(false);
        }
        else
        {
            IsActive = active;
            battery.sprite = Resources.Load<Sprite>("Image/Battery_Full");
            m_AugmentedBtn.gameObject.SetActive(true);
        }
    }

    public void Augmented()//btnclick
    {
        if (isAugmented)
        {
            isAugmented = false;
            m_RectTransfrom.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, x, 530);
            m_RectTransfrom.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, -y, 290);

            m_UIMonitor.SendEvent(Const.E_Augmented, "Close");
            //SendMessageUpwards("AugmentedMonitor", "Close", SendMessageOptions.RequireReceiver);
        }
        else
        {
            isAugmented = true;
            this.transform.SetAsLastSibling();
            m_RectTransfrom.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 1560);
            m_RectTransfrom.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 880);
            if (!Protagonist)
            {

                m_UIMonitor.SendEvent(Const.E_Augmented, this.State);
            }
            else
            {
                m_UIMonitor.SendEvent(Const.E_ShowMessage, "tips16");
                Debug.Log("无法探索该房间");
            }
            CheckChat();
        }
        Sound.Instance.PlayEffect("SoundEffect/Sound_ComputerOpen");

    }

    private void CheckChat()
    {
        switch (State)
        {
            case "D1-6a":
                if (!m_UIMonitor.m_GameModel.guide3)
                {
                    m_UIMonitor.SendEvent(Const.E_AddChat, "guide03");
                    m_UIMonitor.m_GameModel.guide3 = true;
                }
                break;
            case "D1-7a":
                if (!m_UIMonitor.m_GameModel.guide2)
                {
                    m_UIMonitor.SendEvent(Const.E_AddChat, "guide02");
                    m_UIMonitor.m_GameModel.guide2 = true;
                }
                break;
            case "D2-3a":
                if (!m_UIMonitor.m_GameModel.guide7)
                {
                    m_UIMonitor.SendEvent(Const.E_AddChat, "guide07");
                    m_UIMonitor.m_GameModel.guide7 = true;
                }
                break;
        }

    }



}
