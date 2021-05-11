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

    int TypeID = 1;
    private int picIndex;
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
        this.TypeID = id;
        picIndex = 0;
        Pictures = PicDics[id];
        maxNum = Pictures.Length - 1;
        m_Image.sprite = Pictures[picIndex];
        left.SetActive(false);
        buttonHelper();
    }

    private void buttonHelper()
    {
        if (picIndex <= 0)
        {
            left.SetActive(false);
            right.SetActive(true);
        }
        else if (picIndex >= maxNum)
        {
            left.SetActive(true);
            right.SetActive(false);
        }
        else
        {
            left.SetActive(true);
            right.SetActive(true);
        }
        if (picIndex == 1 && TypeID == 1&& StaticData.language == "en")
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
        picIndex--;
        buttonHelper();
        m_Image.sprite = Pictures[picIndex];
        if (TypeID == 1)
        {
            Sound.Instance.PlayEffect("SoundEffect/Sound_Flip");
        }
    }

    public void RightClick()
    {
        picIndex++;
        buttonHelper();
        m_Image.sprite = Pictures[picIndex];
        if (TypeID == 1)
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
