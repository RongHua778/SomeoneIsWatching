using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoBase : MonoBehaviour
{
    public UIMonitor m_UIMonitor;
    public VideoPlayer m_VideoPlayer;

    public int ID;
    public string State;

    protected Image m_ImageBG;
    protected RawImage m_VideoBG;

    public bool CheckState = false;
    public string CheckingState = null;

    protected virtual void Awake()
    {
        m_VideoPlayer = this.GetComponent<VideoPlayer>();
        m_VideoBG = this.transform.Find("Video").GetComponent<RawImage>();
        m_ImageBG = this.transform.Find("Image").GetComponent<Image>();
    }

    public void Pause()
    {
        m_VideoPlayer.Pause();
    }

    public void Play()
    {
        m_VideoPlayer.Play();
    }

    public virtual void PlayVideoClip(string path, string state, bool loop, bool protagonist, CallBackFunc callback, string overAllState)
    {

        //如果在当前镜头中触发新视频时，关闭所有当前镜头正在交互的元素
        if (m_UIMonitor.m_UIInteractive.ItemPanel.gameObject.activeSelf && m_UIMonitor.m_UIInteractive.ItemPanel.currentMonitorID == this.ID)
            m_UIMonitor.m_UIInteractive.HideItemPanel();


        m_VideoBG.gameObject.SetActive(true);
        m_ImageBG.gameObject.SetActive(false);
        this.State = state;
        m_VideoBG.gameObject.SetActive(true);
        m_VideoPlayer.clip = Resources.Load<VideoClip>(path);
        m_VideoPlayer.Play();
        m_VideoPlayer.isLooping = loop;

        if (callback != null || overAllState != null)
            StartCoroutine(CheckVideoFinish(overAllState, callback));
    }

    protected IEnumerator CheckVideoFinish(string overAllState = null, CallBackFunc callback = null)
    {
        float time = (float)m_VideoPlayer.clip.length;
        yield return new WaitForSeconds(time);
        if (overAllState != null)
            m_UIMonitor.m_GameModel.overAllState = overAllState;
        if (callback != null)
            callback();
    }

    public virtual void ShowImage(string path, string state, bool protagonist)
    {
        this.State = state;
        m_VideoBG.gameObject.SetActive(false);
        m_ImageBG.gameObject.SetActive(true);
        m_ImageBG.sprite = Resources.Load<Sprite>(path);
    }

}
