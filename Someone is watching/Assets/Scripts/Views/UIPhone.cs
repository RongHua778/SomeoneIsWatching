using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPhone : MonoBehaviour
{
    
    public UIMonitor m_Monitor;
    public UIDialogManager dialogManager;
    public GameObject choicePanel;
    public Text choice1;
    public Text choice2;
    public Text NumArea;
    List<int> nums = new List<int>();
    [SerializeField] GameObject closeBtn = default;



    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void ClickNum(int num)
    {
        string text = null;
        if (num==10)//remove
        {
            if (nums.Count == 0)
                return;
            nums.RemoveAt(nums.Count-1);
            if (nums.Count == 0)
            {
                NumArea.text = "";
                return;
            }
            if (nums.Count > 0)
            {
                
                for (int i = 0; i < nums.Count; i++)
                {
                    text = text + nums[i].ToString();
                }
                NumArea.text = text;
            }
            return;
        }

        if (nums.Count >= 9)
            return;

        nums.Add(num);
        for(int i = 0; i < nums.Count; i++)
        {
            text = text + nums[i].ToString();
        }
        NumArea.text = text;
        Sound.Instance.PlayEffect("SoundEffect/Sound_Click");
    }

    public void BoHao()
    {
        string now = string.Join("", nums);
        switch (now)
        {
            case "000101777"://BMF
                if (m_Monitor.m_GameModel.day2_SeeNum)
                {
                    m_Monitor.m_GameModel.Calling = true;
                    m_Monitor.SendEvent(Const.E_TriggerDialogue,3);
                    NumArea.text = "通话中";
                    Debug.Log("Success Dial");
                    ShowCloseBtn(false);
                }
                else
                {
                    m_Monitor.SendEvent(Const.E_ShowMessage, "tips19");
                }
                break;

            case "17749079"://明日报社
                if (m_Monitor.m_GameModel.day3_LoginEmail)
                {
                    m_Monitor.m_GameModel.Calling = true;
                    m_Monitor.SendEvent(Const.E_TriggerDialogue, 9);
                    NumArea.text = "通话中";
                    m_Monitor.m_GameModel.day3_ReporterWang = true;
                    Debug.Log("Success Dial");
                    ShowCloseBtn(false);
                }
                else
                {
                    m_Monitor.SendEvent(Const.E_ShowMessage, "tips19");
                }
                break;
        }

    }

    public void ShowCloseBtn(bool value)
    {
        closeBtn.SetActive(value);
    }

    
   
}
