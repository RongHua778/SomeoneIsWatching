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

        }

    }

    public void ToDoorScene()
    {
        VideoManager.Instance.ShowImage(10, "Day1_Door", "Image/CG/Scene_Door", false);
    }

    public void ToDeskScene()
    {
        switch (m_FunctionManager.m_GameModel.Day)
        {
            case 1:
                VideoManager.Instance.ShowImage(10, "Day2_End", "Image/CG/Scene_Computer", false);
                break;
            case 2:
                VideoManager.Instance.ShowImage(10, "CG1", "Image/CG/Scene_Computer", false);
                break;
        }
            

    }


}
