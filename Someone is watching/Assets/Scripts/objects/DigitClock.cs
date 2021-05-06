using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DigitClock : MonoBehaviour
{
    public UIMonitor m_UIMonitor;
    public Image[] DigitNums;
    public Sprite[] NumSprites;
    public List<int> DefaultTime = new List<int>();

    private int num0 = 1;
    public int Num0
    {
        get { return num0; }
        set
        {
            num0 = value;
            if (num0 > 2)
                num0 = 0;
            if (num0 < 0)
                num0 = 2;
            if (Num0 == 2 && Num1 >= 3)
            {
                Num1 = 0;
                SetNum(1, Num1);
            }
        }
    }

    private int num1 = 2;
    public int Num1
    {
        get { return num1; }
        set
        {
            if (Num0 == 2)
            {
                num1 = value;
                if (num1 > 3)
                    num1 = 0;
                if (num1 < 0)
                    num1 = 3;
            }
            else
            {
                num1 = value;
                if (num1 > 9)
                    num1 = 0;
                if (num1 < 0)
                    num1 = 9;
            }
        }
    }

    private int num2 = 0;
    public int Num2
    {
        get { return num2; }
        set
        {
            num2 = value;
            if (num2 > 5)
                num2 = 0;
            if (num2 < 0)
                num2 = 5;
        }
    }

    private int num3 = 0;
    public int Num3
    {
        get { return num3; }
        set
        {
            num3 = value;
            if (num3 > 9)
                num3 = 0;
            if (num3 < 0)
                num3 = 9;
        }
    }


    private void Start()
    {
        SetNum(0, Num0);
        SetNum(1, Num1);
        SetNum(2, Num2);
        SetNum(3, Num3);
        DefaultTime = new List<int> { num0, num1, num2, num3 };
    }
    public void CloseClock()
    {
        this.gameObject.SetActive(false);
        m_UIMonitor.m_UIInteractive.HideSubtitle();
        m_UIMonitor.FindAndSetInteract();
        ResetTime();
    }

    public void ConfirmBtnClick()
    {
        this.gameObject.SetActive(false);
        m_UIMonitor.m_UIInteractive.HideSubtitle();
        m_UIMonitor.FindAndSetInteract();
        TimeCheck();
    }

    private void ResetTime()
    {
        SetNum(0, DefaultTime[0]);
        SetNum(1, DefaultTime[1]);
        SetNum(2, DefaultTime[2]);
        SetNum(3, DefaultTime[3]);
    }

    public void TimeCheck()
    {
        switch (m_UIMonitor.m_GameModel.overAllState)
        {
            case "Day1Period1":

                if (num0 == 1 && num1 == 5 && num2 == 0 && num3 == 0 && m_UIMonitor.m_GameModel.day1_PenInLadyBag && m_UIMonitor.m_GameModel.overAllState == "Day1Period1")
                {
                    m_UIMonitor.Day1Period2();
                    Sound.Instance.PlayEffect("SoundEffect/Sound_Distortion");
                }
                else if (num0 != DefaultTime[0] || num1 != DefaultTime[1] || num2 != DefaultTime[2] || num3 != DefaultTime[3])
                {
                    m_UIMonitor.m_GameModel.GameOverState = "ClockWrong";
                    Game.Instance.LoadScene(3);
                }
                DefaultTime = new List<int> { num0, num1, num2, num3 };
                break;

            case "Day1Period4":
                if (num0 == 2 && num1 == 1 && num2 == 0 && num3 == 0 && m_UIMonitor.m_GameModel.day1_MemoryPiece1)
                {
                    m_UIMonitor.Day1Period5();
                    Sound.Instance.PlayEffect("SoundEffect/Sound_Distortion");
                }
                else if (num0 != DefaultTime[0] || num1 != DefaultTime[1] || num2 != DefaultTime[2] || num3 != DefaultTime[3])
                {
                    m_UIMonitor.m_GameModel.GameOverState = "ClockWrong";
                    Game.Instance.LoadScene(3);
                }
                DefaultTime = new List<int> { num0, num1, num2, num3 };
                break;
            case "Day2Period3":
                if (num0 == 2 && num1 == 2 && num2 == 0 && num3 == 0)
                {
                    m_UIMonitor.Day2Period4();
                    Sound.Instance.PlayEffect("SoundEffect/Sound_Distortion");
                }
                else if (num0 != DefaultTime[0] || num1 != DefaultTime[1] || num2 != DefaultTime[2] || num3 != DefaultTime[3])
                {
                    m_UIMonitor.m_GameModel.GameOverState = "ClockWrong";
                    Game.Instance.LoadScene(3);
                }
                DefaultTime = new List<int> { num0, num1, num2, num3 };
                break;
            default:
                break;
        }

        if (num0 != DefaultTime[0] || num1 != DefaultTime[1] || num2 != DefaultTime[2] || num3 != DefaultTime[3])
        {
            m_UIMonitor.m_GameModel.GameOverState = "ClockWrong";
            Game.Instance.LoadScene(3);
        }
        DefaultTime = new List<int> { num0, num1, num2, num3 };

    }

    public void BtnArrowClick(int id)
    {
        switch (id)
        {
            case 0:
                Num0++;
                SetNum(0, Num0);
                break;
            case 1:
                Num0--;
                SetNum(0, Num0);
                break;
            case 2:
                Num1++;
                SetNum(1, Num1);
                break;
            case 3:
                Num1--;
                SetNum(1, Num1);
                break;
            case 4:
                Num2++;
                SetNum(2, Num2);
                break;
            case 5:
                Num2--;
                SetNum(2, Num2);
                break;
            case 6:
                Num3++;
                SetNum(3, Num3);
                break;
            case 7:
                Num3--;
                SetNum(3, Num3);
                break;

        }
        Sound.Instance.PlayEffect("SoundEffect/Sound_Click");
    }

    public void SetNum(int id, int num)
    {
        DigitNums[id].sprite = NumSprites[num];
    }


}
