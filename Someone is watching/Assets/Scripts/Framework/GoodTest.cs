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
        
    }

    public void PassNextDay()
    {
        m_UIFunctionManager.DayPass();
    }
}
