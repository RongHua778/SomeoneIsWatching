using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryPiece : MonoBehaviour
{
    [HideInInspector] public UIMemory m_UIMemory;
    [SerializeField] Image iconImg = default;
    [SerializeField] Text nameTxt = default;
    
    private Item m_Piece;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateInfo(Item item)
    {
        nameTxt.GetComponent<TextHandler>().SetText(item.Name);
        m_Piece = item;
        iconImg.sprite = Resources.Load<Sprite>(item.Icon);
    }

    public void OnPieceClick()
    {
        m_UIMemory.OpenPieceInfo(m_Piece);
    }

    
}
