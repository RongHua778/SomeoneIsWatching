using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ItemUI: MonoBehaviour
{

    //public Text ItemName;
    //public void UpdateItem(string name)
    //{
    //    this.ItemName.text = name;
    //}

    public Image ItemImage;
    public void UpdateImage(string s)
    {
        ItemImage.sprite =Resources.Load<Sprite>(s);
    }




}
