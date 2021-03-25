﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecordPen : View
{
    public override string Name
    {
        get { return Const.V_RecordPen; }
    }

    public UIDesktop m_UIdesktop;
    public RecordPlayPanel m_PlayPanel;
    public PassWordPanel m_PassWordPanel;

    public GameObject recordPrefab;
    public Transform content;
    public Record[] Records;

    public AudioSource m_Audio;

    public Button btnPlay;
    public Button btnPause;

    Record m_Record;

    float playtime = 0;
    // Start is called before the first frame update
    void Awake()
    {
        RecordGrid.OnLeftClick += FileClick;
    }

    private void OnDestroy()
    {
        RecordGrid.OnLeftClick -= FileClick;

    }

    private void Start()
    {
        this.transform.SetAsFirstSibling();
        AddRecordFile(Records[0]);
        AddRecordFile(Records[1]);
        AddRecordFile(Records[2]);
        AddRecordFile(Records[4]);
    }

    public void AddRecordFile(Record rec)
    {
        GameObject obj = Instantiate(recordPrefab, content);
        RecordGrid recordGrid = obj.GetComponent<RecordGrid>();
        recordGrid.UpdateRecord(rec);
    }
    public void FileClick(Transform trans)
    {
        RecordGrid recGrid = trans.GetComponent<RecordGrid>();
        Record rec = recGrid.record;
        if (rec.NeedPassword)
        {
            m_PassWordPanel.gameObject.SetActive(true);
            m_PassWordPanel.ShowPanel(rec);
        }
        else
        {
            ShowPlayPanel(rec);
        }

        btnPlay.gameObject.SetActive(true);
        btnPause.gameObject.SetActive(false);

    }

    public void ShowPlayPanel(Record rec)
    {
        m_PlayPanel.gameObject.SetActive(true);
        m_PlayPanel.ShowPanel(rec);
        m_Record = rec;
    }






    public void PlayClip()
    {
        m_Audio.clip = m_Record.RecordClip;
        m_Audio.Play();
        btnPlay.gameObject.SetActive(false);
        btnPause.gameObject.SetActive(true);

        if (m_Record.ID == 2)
        {
            m_UIdesktop.m_GameModel.day3_RedRecord = true;
        }

    }

    public void PauseClip()
    {
        m_Audio.Pause();
        btnPlay.gameObject.SetActive(true);
        btnPause.gameObject.SetActive(false);
    }

    public void CloseRecordPen()
    {
        this.gameObject.SetActive(false);
        m_Audio.Stop();
        btnPlay.gameObject.SetActive(true);
        btnPause.gameObject.SetActive(false);
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

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Const.E_AddRecord);
    }

    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {
            case Const.E_AddRecord:
                int recordID = (int)obj;
                AddRecordFile(Records[recordID]);
                break;
        }
    }
}