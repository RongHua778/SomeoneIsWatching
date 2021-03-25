using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDectetor : MonoBehaviour
{
    public UIItemManager m_ItemManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            m_ItemManager.StoreItem(0);
            
        }
    }
}
