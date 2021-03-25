using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSceneCommand : Controller
{
    public override void Execute(object data)
    {

        SceneArgs e = data as SceneArgs;

        //注册视图(View)
        switch (e.sceneIndex)
        {
            case 0://init
                
                break;

            case 1://menu
                RegisterView(GameObject.Find("UIMainMenu").GetComponent<UIMainMenu>());
                break;
            case 2://start
                //RegisterModel(new GameModel());//TestGame自己注册MODEL，Game在Startupcommand里注册MODEL

                RegisterView(GameObject.Find("Canvas").GetComponent<UIFunctionManager>());
                RegisterView(GameObject.Find("MainMonitor").GetComponent<UIMonitor>());
                //RegisterView(GameObject.Find("UIInteractive").GetComponent<UIInteractive>());
                RegisterView(GameObject.Find("ItemArea").GetComponent<UIItemManager>());
                //RegisterView(GameObject.Find("UIGameEnd").GetComponent<UIGameEnd>());
                RegisterView(GameObject.Find("DeskTop").GetComponent<UIDesktop>());
                RegisterView(GameObject.Find("UIRecordPen").GetComponent<UIRecordPen>());
                //RegisterView(GameObject.Find("PhonePanel").GetComponent<UIPhone>());
                //RegisterView(GameObject.Find("Realworld").GetComponent<UIRealworld>());

                RegisterView(GameObject.Find("UIDialogueManager").GetComponent<UIDialogManager>());
                RegisterView(GameObject.Find("EmailSystem").GetComponent<EmailManager>());
                break;

            case 3://end
                RegisterView(GameObject.Find("UIEndScene").GetComponent<UIEndScene>());
                break;

            default:
                break;
        }

        

    }
}
