using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiPanel : MonoBehaviour
{
    public Image m_Image;
    public Sprite[] Pictures;
    public int maxNum;
    public Sprite[] medicines;
    public Sprite[] medicineBook;
    public UIInteractive m_UIinteractve;
    public string State = "";
    Dictionary<int, Sprite[]> PicDics = new Dictionary<int, Sprite[]>();

    [SerializeField] GameObject Translation ,left,right= default;

    int id = 0;
    private int currentID;
    // Start is called before the first frame update

    enum category
    {
        medicine,
        book,
    }



    void InitDic()
    {
        PicDics[(int)category.medicine] = medicines;
        PicDics[(int)category.book] = medicineBook;
    }

    public void UpdatePanel(int id)
    {
        InitDic();
        this.id = id;
        currentID = 0;
        Pictures = PicDics[id];
        maxNum = Pictures.Length - 1;
        m_Image.sprite = Pictures[currentID];
        left.SetActive(false);
        buttonHelper();
    }

    private void buttonHelper()
    {
        if (currentID <= 0)
        {
            left.SetActive(false);
            right.SetActive(true);
        }
        else if (currentID >= maxNum)
        {
            left.SetActive(true);
            right.SetActive(false);
        }
        else
        {
            left.SetActive(true);
            right.SetActive(true);
        }
        if (currentID == 1 && id == 1&& StaticData.language == "ch")
        {
            Translation.SetActive(true);
        }
        else
        {
            Translation.SetActive(false);
        }
    }
    public void LeftClick()
    {
        currentID--;
        buttonHelper();
        m_Image.sprite = Pictures[id];
        if (id == 1)
        {
            Sound.Instance.PlayEffect("SoundEffect/Sound_Flip");
        }
    }

    public void RightClick()
    {
        currentID++;
        buttonHelper();
        m_Image.sprite = Pictures[currentID];
        if (id == 1)
        {
            Sound.Instance.PlayEffect("SoundEffect/Sound_Flip");
        }
    }

    public void Close()
    {
        m_UIinteractve.HideItemPanel();
        this.gameObject.SetActive(false);
        //关闭的时候判断
    }
}
