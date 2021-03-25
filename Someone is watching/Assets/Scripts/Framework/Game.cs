﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Sound))]

public class Game : ApplicationBase<Game>
{
    public Sound Sound = null;

    public void LoadScene(int level)
    {
        
        SceneArgs e = new SceneArgs();
        e.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SendEvent(Const.E_ExitScene,e);
        SceneManager.LoadScene(level, LoadSceneMode.Single);
        //language
        GameEvents.Instance.LanguageChange();
    }

    public void LevelLoaded(Scene scene,LoadSceneMode mod)
    {
        SceneArgs e = new SceneArgs();
        e.sceneIndex = scene.buildIndex;
        SendEvent(Const.E_EnterScene, e);
    }

    private void Start()
    {
        //language
        LanguageControl.LoadLanguage();



        Object.DontDestroyOnLoad(this.gameObject);
        Sound = Sound.Instance;
        RegisterController(Const.E_StartUp, typeof(StartUpCommand));
        SendEvent(Const.E_StartUp);
        SceneManager.sceneLoaded += LevelLoaded;
    }
}