using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailManager : View
{
    GameModel m_GameModel;

    Dictionary<int, Email> EmailList = new Dictionary<int, Email>();

    public GameObject emailPrefab;
    public Transform content;
    public EmailPanel emailPanel;
    public bool NeedPassWord = true;

    public InputField acount;
    public InputField password;

    GameObject normalTR;
    GameObject passwordTR;


    public override string Name
    {
        get { return Const.V_Email; }
    }

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Const.E_SendEmail);

    }

    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {
            case Const.E_SendEmail:
                int id = (int)obj;
                AddEmail(id);
                break;
        }
    }

    public void AddEmail(int id)
    {
        Email email = EmailList[id];
        GameObject obj = Instantiate(emailPrefab, content);
        obj.transform.SetAsFirstSibling();
        EmailGrid emailGrid = obj.GetComponent<EmailGrid>();
        emailGrid.UpdateEmail(email);
    }


    private void OnDestroy()
    {
        EmailGrid.OnLeftClick -= EmailGrid_OnLeftClick;
    }

    void EmailGrid_OnLeftClick(Transform transform)
    {
        emailPanel.gameObject.SetActive(true);
        emailPanel.UpdateEmail(transform.GetComponent<EmailGrid>().m_Email);
    }

    void Start()
    {
        EmailGrid.OnLeftClick += EmailGrid_OnLeftClick;
        normalTR = transform.Find("ContentPanel").gameObject;
        passwordTR = transform.Find("PassWord").gameObject;

        m_GameModel = GetModel<GameModel>() as GameModel;
        Email E00 = new Email(0,"email01_title","明报新闻", "email01_time", "email01_content",true);
        Email E01 = new Email(1, "email02_title", "明日报社", "email02_time", "email02_content", false);
        Email E02 = new Email(2, "email03_title", "明报新闻", "email03_time", "email03_content", false);
        Email E03 = new Email(3, "email04_title", "???", "email04_time", "email04_content", false);
        

        EmailList.Add(0, E00);
        EmailList.Add(1, E01);
        EmailList.Add(2, E02);
        EmailList.Add(3, E03);

        AddEmail(3);
        AddEmail(2);
        AddEmail(1);
        AddEmail(0);


        if (!NeedPassWord)
        {
            normalTR.SetActive(true);
            passwordTR.SetActive(false);
        }
        else
        {
            normalTR.SetActive(false);
            passwordTR.SetActive(true);
        }

    }

    public void PassWordConfirm()
    {
        if (acount.text == "12345" && password.text == "23456")
        {
            normalTR.SetActive(true);
            passwordTR.SetActive(false);
            NeedPassWord = false;
            m_GameModel.day3_LoginEmail = true;
            Sound.Instance.PlayEffect("SoundEffect/Sound_Correct");
        }
        else
        {
            SendEvent(Const.E_ShowMessage, "tips6");
        }
    }


    private void Update()
    {

    }

}
