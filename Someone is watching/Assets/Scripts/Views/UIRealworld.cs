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



 
}
