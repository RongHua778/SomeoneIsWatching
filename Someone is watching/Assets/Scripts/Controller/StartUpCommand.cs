using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpCommand : Controller
{
    public override void Execute(object data)
    {
        //注册模型(Model)
        RegisterModel(new GameModel());
        //注册命令(Controller)

 
        RegisterController(Const.E_EnterScene, typeof(EnterSceneCommand));
        RegisterController(Const.E_ExitScene, typeof(ExitSceneCommand));
        RegisterController(Const.E_StartLevel, typeof(StartLevelCommand));
        RegisterController(Const.E_EndLevel, typeof(EndLevelCommand));
        //RegisterController(Consts.E_CountDownComplete, typeof(CountDownCompleteCommand));

        //跳转开始界面
        Game.Instance.LoadScene(1);

    }
}
