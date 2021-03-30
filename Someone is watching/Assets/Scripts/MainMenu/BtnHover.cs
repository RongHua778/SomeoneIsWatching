using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnHover : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] Text boldTxt = default;
    public void OnPointerEnter(PointerEventData eventData)
    {
        boldTxt.fontStyle = FontStyle.Bold;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        boldTxt.fontStyle = FontStyle.Normal;
    }


}
