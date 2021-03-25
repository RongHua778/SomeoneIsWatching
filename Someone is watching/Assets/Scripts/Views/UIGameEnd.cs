using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class UIGameEnd : View
{
    public override string Name { get { return Const.V_GameEnd; } }
    GameModel m_GameModel;
    VideoPlayer m_VideoPlayer;
    bool Ending = false;

    private void Start()
    {
        m_VideoPlayer = this.transform.Find("VideoPlayer").GetComponent<VideoPlayer>();
        m_GameModel = GetModel<GameModel>() as GameModel;
    }

    private void Update()
    {
        if (Ending)
        {
            if (m_VideoPlayer.frame >= (long)m_VideoPlayer.frameCount - 10)
            {
                m_VideoPlayer.Pause();
                Game.Instance.LoadScene(3);
                //Game.Instance.LoadScene(3);
            }

        }
    }

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Const.E_GameEnd);
    }
    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {
            case Const.E_GameEnd:
                m_VideoPlayer.gameObject.SetActive(true);
                string path = obj as string;
                m_VideoPlayer.clip = Resources.Load<VideoClip>("Video/" + path);
                m_GameModel.GameOverState = path;
                Ending = true;
                m_VideoPlayer.Play();
                break;
        }

    }
}
