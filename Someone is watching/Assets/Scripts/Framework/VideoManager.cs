using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System;

[Serializable]
public class Displayer
{

    public string name;
    public int id;
    public VideoPlayer video;
    public RawImage videoBG;
    public Image image;
   
}


public class VideoManager : Singleton<VideoManager>
{
    public Dictionary<int, Displayer> m_Displayers=new Dictionary<int, Displayer>();
    public Displayer[] displayers;
    public Transform Interact_Monitor_Father;
    List<Transform> Interact_Monitor=new List<Transform>();
    public Transform Interact_RealWorld_Father;
    List<Transform> Interact_RealWorld = new List<Transform>();

    protected override void Awake()
    {
        base.Awake();
        foreach(Displayer dis in displayers)
        {
            m_Displayers.Add(dis.id, dis);
        }

        foreach (Transform child in Interact_Monitor_Father.GetComponentsInChildren<Transform>(true))
        {
            if(child.tag=="Interactive")
                Interact_Monitor.Add(child);
        }

        foreach (Transform child in Interact_RealWorld_Father.GetComponentsInChildren<Transform>(true))
        {
            if (child.tag == "Interactive")
                Interact_RealWorld.Add(child);
        }

    }

    public void FindAndSet(int ID,string state)
    {
        switch (ID)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
                foreach(Transform tr in Interact_Monitor)
                {
                    if (tr.name == state)
                    {
                        tr.gameObject.SetActive(true);
                    }
                    else
                    {
                        tr.gameObject.SetActive(false);
                    }
                }
                break;
            case 10:
                foreach (Transform tr in Interact_RealWorld)
                {
                    if (tr.name == state)
                    {
                        tr.gameObject.SetActive(true);
                    }
                    else
                    {
                        tr.gameObject.SetActive(false);
                    }
                }
                break;
        }
    }

    public void CloseAll()
    {
        foreach (Transform tr in Interact_Monitor)
        {
            tr.gameObject.SetActive(false);
        }
    }


    public void PlayVideoClip(Segment seg)
    {
        
        Displayer displayer = m_Displayers[seg.ID];

        if (displayer.video.GetComponent<MonitorGrid>() != null)
        {
            MonitorGrid mgrid = displayer.video.GetComponent<MonitorGrid>();
            mgrid.State = seg.State;
            mgrid.Protagonist = seg.Protagonist;
            if (mgrid.isAugmented)
            {
                FindAndSet(seg.ID,seg.State);
            }
        }
        else
        {
            FindAndSet(seg.ID, seg.State);
        }

        displayer.videoBG.gameObject.SetActive(true);
        displayer.image.gameObject.SetActive(false);
        displayer.video.clip = Resources.Load<VideoClip>(seg.VideoClipPath);
        displayer.video.Play();
        displayer.video.isLooping = seg.Loop;
        //FindAndSet(seg.ID,seg.State);
        if (seg.Callback != null)
            StartCoroutine(CheckVideoFinish(displayer, seg.Callback));
    }


    public void PlaySeq(List<Segment> segs)
    {
        StartCoroutine(PlaySequence(segs));
    }

    public void StopAll()
    {
        StopAllCoroutines();
    }

    public IEnumerator PlaySequence(List<Segment> segs)
    {

        for (int i = 0; i < segs.Count; i++)
        {
            Segment seg = segs[i];
            Displayer displayer = m_Displayers[seg.ID];
            PlayVideoClip(seg);
            float time = (float)displayer.video.length;
            yield return new WaitForSeconds(time);
        }
    }

    IEnumerator CheckVideoFinish(Displayer displayer, CallBackFunc callback = null)
    {
        float time = (float)displayer.video.clip.length;
        yield return new WaitForSeconds(time);
        if (callback != null)
            callback();
    }

    public void ShowImage(int id,string state,string path,bool protagonist)
    {
        Displayer displayer = m_Displayers[id];
        //if (state!=null)
        //    FindAndSet(id,state);
        if (displayer.video.GetComponent<MonitorGrid>() != null)
        {
            MonitorGrid mgrid = displayer.video.GetComponent<MonitorGrid>();
            mgrid.State = state;
            mgrid.Protagonist = protagonist;
            if (mgrid.isAugmented)
            {
                FindAndSet(id, state);
            }
        }
        else//真实世界
        {
            FindAndSet(id, state);
        }
        displayer.videoBG.gameObject.SetActive(false);
        displayer.image.gameObject.SetActive(true);
        displayer.image.sprite = Resources.Load<Sprite>(path);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
