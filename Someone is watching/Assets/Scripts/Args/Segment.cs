using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment
{
    public int ID;
    public string VideoClipPath;
    public string State;
    public bool Protagonist;
    public bool Loop;
    public string OverallState;
    public CallBackFunc Callback;

    public Segment(int id,string videoClipPath,string state,bool protagonist,bool loop,string overallstate=null,CallBackFunc callback=null)
    {
        this.ID = id;
        this.VideoClipPath = videoClipPath;
        this.State = state;
        this.Protagonist = protagonist;
        this.Loop = loop;
        this.OverallState = overallstate;
        this.Callback = callback;

    }

}
