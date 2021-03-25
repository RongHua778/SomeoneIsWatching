using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassWordPanel : MonoBehaviour
{
    public UIRecordPen m_RecordPen;
    InputField inputField;


    Record m_Rec;

    public void ShowPanel(Record rec)
    {
        m_Rec = rec;
    }

    public void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }

    public void Confirm()
    {
        string password = inputField.text;
        if (password == m_Rec.PassWord)
        {
            ClosePanel();
            m_RecordPen.ShowPlayPanel(m_Rec);
            m_RecordPen.m_UIdesktop.SendEvent(Const.E_ShowMessage, "tips4");
            Sound.Instance.PlayEffect("SoundEffect/Sound_Correct");
        }
        else
        {
            m_RecordPen.m_UIdesktop.SendEvent(Const.E_ShowMessage, "tips5");

        }


    }


    // Start is called before the first frame update
    void Start()
    {
        inputField = transform.Find("InputField").GetComponent<InputField>();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
