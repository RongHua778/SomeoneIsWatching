using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIRealworld : MonoBehaviour
{
    public UIFunctionManager m_FunctionManager;
    public UIPhone m_UIPhone;

    private void Awake()
    {
        
    }


    public void InteractiveClick(string code)
    {
        switch (code)
        {
            case "Phone":
                m_UIPhone.gameObject.SetActive(true);
                break;

            case "Screen":
                m_FunctionManager.ShowPanel(0);
                //SendEvent(Const.E_ShowPanel,0);
                break;
            case "Mail":
                Segment seg1 = new Segment(10, "Video/Animation/D2_Mail", "Day2_Mail", false, false);
                VideoManager.Instance.PlayVideoClip(seg1);
                break;
            case "OpenMail":
                Segment seg2 = new Segment(10, "Video/Animation/OpenMail", "MailOpen", false, false);
                VideoManager.Instance.PlayVideoClip(seg2);
                m_FunctionManager.m_GameModel.day2_SeeNum = true;
                break;
            case "Box":
                Segment seg3 = new Segment(10, "Video/Animation/D3_Box", "Day3_Box", false, false);
                VideoManager.Instance.PlayVideoClip(seg3);
                break;
            case "OpenBox":
                Segment seg4 = new Segment(10, "Video/Animation/OpenBox", "BoxOpen", false, false,null,OpenBoxCallback);
                VideoManager.Instance.PlayVideoClip(seg4);
                break;

        }

    }

    private void OpenBoxCallback()
    {
        m_FunctionManager.SendEvent(Const.E_GetItem, 13);
        m_FunctionManager.SendEvent(Const.E_ShowMessage, "tips18");
    }

    public void ToDoorScene()
    {
        switch (m_FunctionManager.m_GameModel.Day)
        {
            case 1:
                VideoManager.Instance.ShowImage(10, "Day1_Door", "Image/CG/D1_DoorScene", false);
                break;
            case 2:
                VideoManager.Instance.ShowImage(10, "Day2_Door", "Image/CG/D2_DoorScene", false);
                break;
            case 3:
                VideoManager.Instance.ShowImage(10, "Day3_Door", "Image/CG/D3_DoorScene", false);
                break;
            default:
                break;
        }
    }

    public void ToDeskScene()
    {
        switch (m_FunctionManager.m_GameModel.Day)
        {
            case 1:
                VideoManager.Instance.ShowImage(10, "Day1_Desk", "Image/CG/Scene_Computer", false);
                break;
            case 2:
                VideoManager.Instance.ShowImage(10, "Day2_Desk", "Image/CG/Scene_Computer", false);
                break;
            case 3:
                VideoManager.Instance.ShowImage(10, "Day2_Desk", "Image/CG/Scene_Computer", false);
                break;
        }
            

    }


}
