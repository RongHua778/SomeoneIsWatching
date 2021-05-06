using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChatBox : View
{
    public override string Name => Const.V_ChatBox;

    public GameObject chatPrefab = default;
    [SerializeField] GameObject redDot;
    [SerializeField] GameObject child;

    [SerializeField] Transform content;

    private void AddChat(string text)
    {
        GameObject chat = Instantiate(chatPrefab, content);
        chat.GetComponent<ChatBoxSize>().SendText(text);
        redDot.SetActive(true);
        Sound.Instance.PlayEffect("SoundEffect/Sound_Message");
    }

    public void ChatBoxBtnClick()
    {
        if (child.activeSelf)
            child.SetActive(false);
        else
            child.SetActive(true);
        redDot.SetActive(false);
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        AddChat("guide01");
    //    }
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        AddChat("guide01");
    //    }
    //}

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Const.E_AddChat);

    }

    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {
            case Const.E_AddChat:
                string text = (string)obj;
                AddChat(text);
                break;
        }

    }


}
