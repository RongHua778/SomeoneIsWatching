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


    int id;

    private int currentID;
    // Start is called before the first frame update

    private void Start()
    {
        //PicDics.Add(0, medicines);
        //PicDics.Add(1, medicineBook);
    }


    void InitDic()
    {
        PicDics[0] = medicines;
        PicDics[1] = medicineBook;
    }

    public void UpdatePanel(int id)
    {
        InitDic();
        currentID = id;
        Pictures = PicDics[id];
        maxNum = Pictures.Length-1;
        m_Image.sprite = Pictures[id];
    }

    public void LeftClick()
    {
        id--;
        if (id < 0)
        {
            id = maxNum;
        }
        m_Image.sprite = Pictures[id];
        if (currentID == 1)
        {
            Sound.Instance.PlayEffect("SoundEffect/Sound_Flip");
        }
    }

    public void RightClick()
    {
        id++;
        if (id >maxNum)
        {
            id = 0;
        }
        m_Image.sprite = Pictures[id];
        if (currentID == 1)
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
