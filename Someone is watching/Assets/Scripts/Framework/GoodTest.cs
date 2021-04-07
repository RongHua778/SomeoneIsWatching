using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodTest : MonoBehaviour
{
    public UIFunctionManager m_UIFunctionManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    Screen.fullScreen = false;  //退出全屏         

        //}

        ////按A全屏
        //if (Input.GetKey(KeyCode.A))
        //{
        //    Screen.SetResolution(1920, 1080, true);

        //    Screen.fullScreen = true;  //设置成全屏,
        //}

    }

    public void PassNextDay()
    {
        m_UIFunctionManager.DayPass();
    }
}
