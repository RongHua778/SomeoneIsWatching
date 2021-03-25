using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{

    public string name;
    public int ID;
    public DialogueDetail[] details;

}
[System.Serializable]
public class DialogueDetail
{
    public bool needSelection;
    public string choice1;
    public int targetID1;
    public string choice2;
    public int targetID2;
    public AudioClip sound;
    //[TextArea(3, 10)]
    public string key;
}


