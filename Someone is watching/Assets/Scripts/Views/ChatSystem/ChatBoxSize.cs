using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatBoxSize : MonoBehaviour
{
    public Image boxFrame;
    public Text HorizontalText;
    public Text VerticalText;

    public int minFrameWidth = 100;
    public int maxFrameWidth = 600;

    public int rowHeight = 80;

    public Color textColor = new Color(0, 0, 0, 1);
    public Color hideColor = new Color(0, 0, 0, 0);

    private float offsetDistance;
    // Start is called before the first frame update
    void Start()
    {
        offsetDistance = boxFrame.rectTransform.sizeDelta.y - HorizontalText.rectTransform.sizeDelta.y / 2;
        VerticalText.color = hideColor;
        HorizontalText.color = textColor;
        StartCoroutine(AdjustSize());
    }

    private void AdjustDialogBoxSize()
    {
        if (VerticalText.rectTransform.sizeDelta.y > rowHeight)
        {
            VerticalText.color = textColor;
            HorizontalText.color = hideColor;
            boxFrame.rectTransform.sizeDelta = new Vector2(minFrameWidth + VerticalText.rectTransform.sizeDelta.x / 2, VerticalText.rectTransform.sizeDelta.y / 2 + offsetDistance);
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(minFrameWidth + VerticalText.rectTransform.sizeDelta.x / 2, VerticalText.rectTransform.sizeDelta.y / 2 + offsetDistance);
        }
        else
        {
            boxFrame.rectTransform.sizeDelta = new Vector2(minFrameWidth + HorizontalText.rectTransform.sizeDelta.x / 2, HorizontalText.rectTransform.sizeDelta.y / 2 + offsetDistance);
        }

    }

    public void SendText(string input)
    {
        VerticalText.GetComponent<TextHandler>().SetText(input);
        HorizontalText.GetComponent<TextHandler>().SetText(input);
        //StartCoroutine(AdjustSize());
    }

    IEnumerator AdjustSize()
    {
        yield return new WaitForEndOfFrame();
        AdjustDialogBoxSize();
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    SendText("你就是个抱宝宝你就是个抱宝宝你就是个抱宝宝你就是个抱宝宝你就是个抱宝宝你就是个抱宝宝你就是个抱宝宝你就是个抱宝宝你就是个抱宝宝你就是个抱宝宝");
        //}
    }
}
