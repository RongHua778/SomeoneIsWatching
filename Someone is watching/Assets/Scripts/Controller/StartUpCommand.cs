using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        //跳转开始界面,如果是本场景就不跳转
        if (SceneManager.GetActiveScene().buildIndex != Game.Instance.StartSceneIndex)
            Game.Instance.LoadScene(Game.Instance.StartSceneIndex);

    }
}
