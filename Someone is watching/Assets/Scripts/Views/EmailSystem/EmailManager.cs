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

    public void Close()
    {
        this.transform.SetAsFirstSibling();
        Sound.Instance.PlayEffect("SoundEffect/Sound_ComputerOpen");

    }
    // Start is called before the first frame update
    void Awake()
    {
        EmailGrid.OnLeftClick += EmailGrid_OnLeftClick;
        normalTR = transform.Find("ContentPanel").gameObject;
        passwordTR = transform.Find("PassWord").gameObject;

        m_GameModel = GetModel<GameModel>() as GameModel;
        Email E00 = new Email(0,"新闻","明报新闻","2012年12月19日", 
            "XX藥研科學家逝世，享年53歲。在XX公司任職，是多年的元老。意外逝去 后公司" +
            "董事表示惋惜，并給予家屬大筆撫恤金。 死因仍在調查中，外界對此事眾說紛紜。" +
            "如有相關線索請致電：7758258 初步判斷死亡原因：獨自駕駛，車禍，意外" +
            "心肌梗塞墜崖。",true);
        Email E01 = new Email(1, "报社工资结单", "明日报社", "2022年8月23日", "你这个月的工资是0元",false);
        Email E02 = new Email(2, "新闻", "明报新闻", "2022年8月15日", "XXX新藥即將上市，藥監局獲批，A股獲得首輪融",false);
        Email E03 = new Email(3, "???", "???", "2022年8月12日", "我考慮了很久，還是不能呈堂公證。抱歉，我還有自己的家人。上次的錄 音麻煩刪除。",false);
        Email E04 = new Email(4, "新闻", "明报新闻", "2022年8月16日", "脑科学领域革新，记忆这个尚未探知的领域，再未来十年内，脑记忆深潜成为可能。", false);
        Email E05 = new Email(5, "广告", "利率低至0.3%", "2022年8月16日", "欢迎咨询北华银行最新贷款计划，每人可享有十万额度最低0.3%利率折扣。", false);




        EmailList.Add(0, E00);
        EmailList.Add(1, E01);
        EmailList.Add(2, E02);
        EmailList.Add(3, E03);
        EmailList.Add(4, E04);
        EmailList.Add(5, E05);

        //EmailList.Add(4, E01);
        Close();

        AddEmail(2);
        AddEmail(3);
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
