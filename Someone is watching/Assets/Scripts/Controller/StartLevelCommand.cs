using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelCommand : Controller
{

    public override void Execute(object data)
    {
        RoundModel rModel = GetModel<RoundModel>();

        StartLevelArgs e = data as StartLevelArgs;
        //进入游戏
        Game.Instance.LoadScene(3);

    }

}