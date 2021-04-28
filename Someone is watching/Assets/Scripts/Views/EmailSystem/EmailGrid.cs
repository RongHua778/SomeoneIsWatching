using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class EmailGrid : MonoBehaviour,IPointerClickHandler
{

    public static Action<Transform> OnLeftClick;

    public Email m_Email;
    Image star;
    TextHandler emailName;
    TextHandler title;
    TextHandler time;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (OnLeftClick != null)
                OnLeftClick(transform);
        }
    }

    public void Init()
    {
        //emailName = transform.Find("Name").GetComponent<TextHandler>();
        title = transform.Find("Title").GetComponent<TextHandler>();
        time = transform.Find("Time").GetComponent<TextHandler>();
        star = transform.Find("Star").GetComponent<Image>();
    }

    public void UpdateEmail(Email email)
    {

        Init();
        m_Email = email;
        //emailName.text = email.Author;
        title.SetText(email.Title);
        time.SetText(email.Time);
        star.gameObject.SetActive(email.Star);

        transform.GetComponent<Toggle>().group = transform.parent.GetComponent<ToggleGroup>();
    }

}
