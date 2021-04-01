using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMemory : View
{
    public override string Name
    {
        get { return Const.V_Memory; }
    }
    GameModel m_GameModel;
    [SerializeField] Transform pieceGrid = default;
    [SerializeField] GameObject memoryPiecePrefab = default;
    [SerializeField] PieceInfo m_PieceInfo = default;

    private void Awake()
    {
        m_GameModel = GetModel<GameModel>() as GameModel;
   
    }

    private void Start()
    {
        ClosePieceInfo();
        foreach (var item in m_GameModel.collectedPieces)
        {
            AddPiece(item);
        }
    }
    public void AddPiece(Item item)
    {
        GameObject newPiece = Instantiate(memoryPiecePrefab, pieceGrid);
        MemoryPiece memory = newPiece.GetComponent<MemoryPiece>();
        memory.m_UIMemory = this;
        memory.UpdateInfo(item);
        m_GameModel.collectedPieces_Temp.Add(item);
    }


    public override void RegisterEvents()
    {
        AttentionEvents.Add(Const.E_AddPiece);
    }

    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {
            case Const.E_AddPiece:
                Item item = UIItemManager.ItemList[(int)obj];
                AddPiece(item);
                break;
        }
    }

    public void OpenPieceInfo(Item m_Piece)
    {
        m_PieceInfo.gameObject.SetActive(true);
        m_PieceInfo.UpdatePieceInfo(m_Piece);
    }

    public void ClosePieceInfo()
    {
        m_PieceInfo.gameObject.SetActive(false);
    }
}
