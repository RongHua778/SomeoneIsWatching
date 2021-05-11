using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class RecordGrid : MonoBehaviour,IPointerClickHandler
{
    public static Action<Transform> OnLeftClick;

    public Record record;
    Image Icon;
    Text Name;
    int ID;

    public void Init()
    {
        Icon = transform.Find("Icon").GetComponent<Image>();
        Name = transform.Find("FileName").GetComponent<Text>();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (OnLeftClick != null)
                OnLeftClick(transform);
        }
    }

    public void UpdateRecord(Record rec)
    {
        Init();
        record = rec;
        Icon.color = rec.Color;
        Name.text = rec.RecordName;
        ID = rec.ID;
    }
    // Start is called before the first frame update


}
