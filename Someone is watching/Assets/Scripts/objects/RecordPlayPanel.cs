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

    AudioSource m_Audio;

    public Button btnPlay;
    public Button btnPause;

    float playtime = 0;
    public void ShowPanel(Record rec)
    {
        m_Rec = rec;
        recordName.text = rec.RecordName;
        recordTime.text = rec.RecordDate;
        textHandler.GetComponent<Text>().text = "";
        btnPlay.gameObject.SetActive(true);
        btnPause.gameObject.SetActive(false);
    }

    public void ClosePanel()
    {
        m_Audio.Stop();
        this.gameObject.SetActive(false);
    }

    public void PlayRecord()
    {
        textHandler.SetText(m_Rec.RecordText);
        m_Audio.clip = m_RecordPen.m_Record.RecordClip;
        m_Audio.Play();
        btnPlay.gameObject.SetActive(false);
        btnPause.gameObject.SetActive(true);

        if (m_Rec.ID == 2)
        {
            m_RecordPen.m_UIdesktop.m_GameModel.day3_RedRecord = true;
        }
    }

    public void PauseRecord()
    {
        m_Audio.Pause();
        btnPlay.gameObject.SetActive(true);
        btnPause.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_Audio = this.gameObject.AddComponent<AudioSource>();
        m_RecordPen = transform.parent.GetComponent<UIRecordPen>();
        this.gameObject.SetActive(false);
    }

    private void Update()
    {

        if (m_Audio.isPlaying)
        {
            playtime += Time.deltaTime;
            if (playtime >= m_Audio.clip.length)
            {
                m_Audio.Stop();
                playtime = 0;
                btnPlay.gameObject.SetActive(true);
                btnPause.gameObject.SetActive(false);
            }
        }
    }
    // Update is called once per frame

}
