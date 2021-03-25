using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordPlayPanel : MonoBehaviour
{
    Record m_Rec;
    UIRecordPen m_RecordPen;
    public Text recordName;
    public Text recordTime;
    [SerializeField]
    TextHandler textHandler;

    public void ShowPanel(Record rec)
    {
        m_Rec = rec;
        recordName.text = rec.RecordName;
        recordTime.text = rec.RecordDate;
        textHandler.SetText(rec.RecordText);
    }

    public void ClosePanel()
    {
        this.gameObject.SetActive(false);
        m_RecordPen.m_Audio.Stop();
    }


    // Start is called before the first frame update
    void Start()
    {

        m_RecordPen = transform.parent.GetComponent<UIRecordPen>();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame

}
