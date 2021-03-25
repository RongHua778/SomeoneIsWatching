using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPhone : MonoBehaviour
{
    
    public UIMonitor m_Monitor;
    public DialogTest m_Dialog;
    public GameObject choicePanel;
    public Text choice1;
    public Text choice2;
    public Text NumArea;
    List<int> nums = new List<int>();



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

        if (nums.Count >= 8)
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
            case "92000908":
                if (m_Monitor.m_GameModel.day2_SeeNum&&m_Monitor.m_GameModel.day2_GetBattery1&&m_Monitor.m_GameModel.day2_GetBattery2)
                {
                    m_Monitor.m_GameModel.Calling = true;
                    m_Monitor.SendEvent(Const.E_TriggerDialogue,3);
                    NumArea.text = "通话中";
                    Debug.Log("Success Dial");
                }
                else
                {
                    Debug.Log("Fail Dial");
                }
                break;

            case "7758258":
                m_Monitor.m_GameModel.Calling = true;
                m_Monitor.SendEvent(Const.E_TriggerDialogue, 9);
                NumArea.text = "通话中";
                m_Monitor.m_GameModel.day3_ReporterWang = true;
                Debug.Log("Success Dial");
                break;

        }
        
        
    }

    //全自动播放
    public IEnumerator PlayDialog(int index,string text)
    {
        yield return new WaitForSeconds(1f);
        m_Dialog.UpdateText(text);
        //PLAYSOUND
        yield return new WaitForSeconds(1f);
        ShowChoice(index);
        
    }

    public void ShowChoice(int index)
    {
        choicePanel.SetActive(true);
        switch (index)
        {
            case 0:
                choice1.text = "询问库存情况";
                choice2.text = "询问治疗疾病";
                break;
            case 1:
                choice1.text = "询问副作用";
                choice2.text = "询问用量";
                break;

            case 2:
                choice1.text = "购买固态";
                choice2.text = "购买液态";
                break;
            default:
                choicePanel.SetActive(false);
                m_Monitor.m_GameModel.Calling = false;
                break;
        }

    }

    public void ChoiceClick(Text btnText)
    {
        string text = btnText.text;
        switch (text)
        {
            case "询问库存情况":
                StartCoroutine(PlayDialog(1, "基本无臭无味，易于口服"));
                break;

            case "询问治疗疾病":
                StartCoroutine(PlayDialog(1, "精神的安慰剂"));
                break;

            case "询问副作用":
                StartCoroutine(PlayDialog(2, "產生微弱幻覺"));
                break;

            case "询问用量":
                StartCoroutine(PlayDialog(2, "最多六粒一天，過量雖沒有生命危險，但會讓人熟睡"));
                break;

            case "购买固态":
                StartCoroutine(PlayDialog(99, "感谢，将会邮递给您。"));
                NumArea.text = "";
                m_Monitor.Day2Period4();
                break;
            case "购买液态":
                StartCoroutine(PlayDialog(99, "感谢，将会邮递给您。"));
                NumArea.text = "";
                m_Monitor.Day2Period4();

                break;
            default:
                break;

        }
        choicePanel.SetActive(false);
    }


   
}
