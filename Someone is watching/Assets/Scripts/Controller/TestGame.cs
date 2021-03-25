using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TestGame : ApplicationBase<TestGame>
{
    // Start is called before the first frame update
    protected override void Awake() 
    {
        base.Awake();
        Object.DontDestroyOnLoad(this.gameObject);
        RegisterController(Const.E_TestController,typeof(TestControl));
        SendEvent(Const.E_TestController);
    }

    public void LoadScene(int level)
    {
        SceneArgs e = new SceneArgs();
        e.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SendEvent(Const.E_ExitScene, e);
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    public void LevelLoaded(Scene scene, LoadSceneMode mod)
    {
        SceneArgs e = new SceneArgs();
        e.sceneIndex = scene.buildIndex;
        SendEvent(Const.E_EnterScene, e);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
