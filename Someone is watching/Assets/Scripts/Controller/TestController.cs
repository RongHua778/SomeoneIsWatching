using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControl : Controller
{
    public override void Execute(object obj)
    {
        RegisterModel(new GameModel());

        RegisterView(GameObject.Find("Canvas").GetComponent<UIFunctionManager>());
        RegisterView(GameObject.Find("MainMonitor").GetComponent<UIMonitor>());
        //RegisterView(GameObject.Find("UIInteractive").GetComponent<UIInteractive>());
        RegisterView(GameObject.Find("ItemArea").GetComponent<UIItemManager>());
        RegisterView(GameObject.Find("UIGameEnd").GetComponent<UIGameEnd>());
        RegisterView(GameObject.Find("DeskTop").GetComponent<UIDesktop>());
        //RegisterView(GameObject.Find("PhonePanel").GetComponent<UIPhone>());
        //RegisterView(GameObject.Find("Realworld").GetComponent<UIRealworld>());

        RegisterView(GameObject.Find("UIDialogueManager").GetComponent<UIDialogManager>());

        //RegisterView(GameObject.Find("UIMonitors").GetComponent<UIMonitor>());
        ////RegisterView(GameObject.Find("UIInteractive").GetComponent<UIInteractive>());
        //RegisterView(GameObject.Find("ItemArea").GetComponent<UIItemManager>());
        //RegisterView(GameObject.Find("UIGameEnd").GetComponent<UIGameEnd>());
    }
}
