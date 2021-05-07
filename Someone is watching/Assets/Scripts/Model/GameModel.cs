using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class GameModel : Model
{
    public override string Name { get { return Const.M_GameModel; } }

    public bool Calling = false;

    public int Day = 2;
    public bool daying = false;
    public bool unlockRecord = false;
    string overallstate = null;
    public string overAllState
    {
        get { return overallstate; }
        set
        {
            overallstate = value;
            SendEvent(Const.E_DayEndCheck, DayEndCheck());
            Debug.Log(overallstate);
        }
    }
    public string GameOverState = "";


    public bool day1_GetPen = false;
    public bool day1_PenInLadyBag = false;
    public bool day1_MemoryPiece1 = false;
    public bool day1_HeardRecord3 = false;

    public bool day2_GetBattery1 = false;
    public bool day2_GetBattery2 = false;
    public bool day2_Piece3 = false;
    public bool day2_Tool = false;
    public bool day2_Piece2 = false;
    public bool day2_SeeNum = false;
    public bool day2_CG1 = false;

    public bool AllowExitToReal = true;
    //public bool MonitorRepairing = false;
    public bool day2_PhoneCall = false;

    public bool day2_WaterPungRepair = false;

    public bool day2_Camera2Repair = false;
    public bool day2_Camera4Repair = false;

    public bool day3_Piece4 = false;
    public bool day3_Piece5 = false;

    public bool day3_LoginEmail = false;
    public bool day3_RedRecord = false;
    public bool day3_ReporterWang = false;

    public bool day4_SDcard = false;
    public bool day4_BlueMap = false;
    public bool day4_Piece6 = false;
    public bool day4_Piece7 = false;

    public bool guide1 = false;
    public bool guide2 = false;
    public bool guide3 = false;
    public bool guide4 = false;
    public bool guide5 = false;
    public bool guide6 = false;
    public bool guide7 = false;
    public bool guide8 = false;
    public bool guide9 = false;
    public bool guide10 = false;
    public bool guide11 = false;

    public List<Item> collectedItems = new List<Item>();
    public List<Item> collectedItems_Temp = new List<Item>();

    public List<Item> collectedPieces = new List<Item>();
    public List<Item> collectedPieces_Temp = new List<Item>();

    public void NextDay(bool isTest = false)
    {
        switch (Day)
        {
            case 1:
                GameOverState = "Day2";
                Day = 2;
                daying = false;
                Game.Instance.LoadScene(3);

                break;
            case 2:
                GameOverState = "Day3";
                Day = 3;
                Game.Instance.LoadScene(3);
                break;

            case 3:
                GameOverState = "Day4";
                Day = 4;
                Game.Instance.LoadScene(3);
                break;
        }
    }

    public bool DayEndCheck()
    {
        switch (Day)
        {
            case 1:
                if (overallstate == "Day1Period6" && day1_HeardRecord3)
                {
                    if (!guide5)
                    {
                        SendEvent(Const.E_AddChat, "guide05");
                        guide5 = true;
                    }
                    return true;
                }
                break;
            case 2:
                if (day2_Camera2Repair && day2_Camera4Repair && day2_WaterPungRepair)
                {
                    if (!guide9)
                    {
                        SendEvent(Const.E_AddChat, "guide09");
                        guide9 = true;
                    }
                    return true;
                }
                break;
            case 3:
                if (day3_LoginEmail && day3_ReporterWang && day3_RedRecord)
                {
                    if (!guide11)
                    {
                        SendEvent(Const.E_AddChat, "guide11");
                        guide11 = true;
                    }
                    return true;
                }
                break;
        }
        return false;
    }

    public void Reset()
    {
        //Day = 1;
        overallstate = null;
        GameOverState = "";
        daying = false;
        day1_GetPen = false;
        day1_PenInLadyBag = false;
        day1_MemoryPiece1 = false;
        day1_HeardRecord3 = false;
        day2_Camera2Repair = false;
        day2_Camera4Repair = false;
        day2_CG1 = false;
        day2_GetBattery1 = false;
        day2_GetBattery2 = false;
        day2_PhoneCall = false;
        day2_Piece2 = false;
        day2_Piece3 = false;
        day2_SeeNum = false;
        day2_Tool = false;
        day2_WaterPungRepair = false;
        day3_Piece4 = false;
        day3_Piece5 = false;

        guide1 = guide2 = guide3 = guide4 = guide5 = guide6 = guide7 = guide8 = guide9 = guide10 = guide11 = false;
    }

    public bool HandleItem(Item item, string targetName)
    {
        bool handleSuccess = false;
        switch (item.ID)
        {
            case 0:
                switch (targetName)
                {
                    case "LadyBag":
                        day1_PenInLadyBag = true;
                        SendEvent(Const.E_ShowMessage, "tips2");
                        handleSuccess = true;
                        collectedItems_Temp.Remove(item);
                        break;
                }
                break;

            case 3:
                //if (overAllState != "Day2Period4")
                //    handleSuccess = false;
                //else
                //{
                    switch (targetName)
                    {
                        case "EnergyRepair1":
                            day2_Camera2Repair = true;
                            SendEvent(Const.E_Repair, 1);
                            handleSuccess = true;
                            break;

                        case "EnergyRepair3":
                            day2_Camera4Repair = true;
                            handleSuccess = true;
                            SendEvent(Const.E_Repair, 3);
                            break;


                    }
                //}
                break;

            case 5:

                switch (targetName)
                {
                    case "Day2WaterPungCanRepair":
                        handleSuccess = true;
                        day2_WaterPungRepair = true;
                        SendEvent(Const.E_ShowMessage, "tips3");
                        break;
                }
                break;

            case 9:
                switch (targetName)
                {
                    case "BlueMapEffect":
                        handleSuccess = true;
                        VideoManager.Instance.ShowImage(2, "D4-3b", "Image/Camera3/D4.3b", false);
                        break;
                }
                break;

        }
        if (handleSuccess)
        {
            Sound.Instance.PlayEffect("SoundEffect/Sound_Apply");
        }
        return handleSuccess;
    }



}
