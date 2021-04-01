using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceInfo : MonoBehaviour
{
    public UIMonitor m_UIMonitor;
    public Text piecename;
    public Text info;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdatePieceInfo(Item item)
    {
        info.GetComponent<TextHandler>().SetText(item.Description);
        piecename.GetComponent<TextHandler>().SetText(item.Name);
        //piecename.text = item.Name;
        //if (StaticData.language == "ch")
        //    info.text = item.Description;
        //else if (StaticData.language == "en")
        //    info.text = item.Descrition_En;
    }

    //public void ClosePieceInfo()
    //{
    //    this.gameObject.SetActive(false);
    //    m_UIMonitor.m_UIInteractive.HideSubtitle();
    //    m_UIMonitor.FindAndSetInteract();
    //}
    // Update is called once per frame
    void Update()
    {
        
    }
}
