using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogManager : View
{
    public override string Name
    {
        get { return Const.V_Dialogue; }
    }

    GameModel m_GameModel;
    private Queue<DialogueDetail> details;

    public Text NameText;
    public Text DialogueText;


    public Animator animator;

    private Dialogue currentDialog;

    public DialogueTrigger[] Triggers;

    public Button Contiune;
    public Button SkipBtn;
    public TextHandler choice1;
    public TextHandler choice2;
    public GameObject dialogSystem;

    Dictionary<int, DialogueTrigger> TriggerDIC = new Dictionary<int, DialogueTrigger>();

    void Start()
    {
        m_GameModel = GetModel<GameModel>() as GameModel;
        details = new Queue<DialogueDetail>();

        foreach (DialogueTrigger trigger in Triggers)
        {
            TriggerDIC.Add(trigger.dialogue.ID, trigger);
        }

    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue.ID == 0)
        {
            SkipBtn.gameObject.SetActive(true);
        }
        dialogSystem.SetActive(true);
        animator.SetBool("IsOpen", true);
        currentDialog = dialogue;
        NameText.text = dialogue.name;

        details.Clear();
        foreach (DialogueDetail detail in dialogue.details)
        {
            details.Enqueue(detail);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (details.Count == 0)
        {
            EndDialogue();
            return;
        }
        DialogueDetail detail = details.Dequeue();
        AudioClip sound = detail.sound;
        Camera.main.GetComponent<AudioSource>().clip = sound;
        Camera.main.GetComponent<AudioSource>().Play();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(detail));
    }

    IEnumerator TypeSentence(DialogueDetail detail)
    {
        if (detail.needSelection)
            Contiune.gameObject.SetActive(false);
        else
            Contiune.gameObject.SetActive(true);
        DialogueText.text = "";
        string sentence = LanguageControl.GetValue(detail.key);

        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        if (detail.needSelection)
        {
            animator.SetBool("ShowChoice", true);
            choice1.SetText(detail.choice1);
            choice1.name = detail.targetID1.ToString();
            choice2.SetText(detail.choice2);
            choice2.name = detail.targetID2.ToString();
        }

        switch (detail.key)//为了关闭phone call按钮
        {
            case "reporter7":
            case "bmf4":
                SendEvent(Const.E_DialogEnd);
                break;
        }
    }

    public void ChoiceClick(Text text)
    {
        int id = int.Parse(text.name);
        DialogueTrigger trigger = TriggerDIC[id];
        trigger.TriggerDialogue();

        animator.SetBool("ShowChoice", false);
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        switch (currentDialog.ID)
        {
            case 1:
                m_GameModel.GameOverState = "DeadManNoSay";
                Game.Instance.LoadScene(3);
                break;
            case 2://黑衣人第一幕
                Segment seg = new Segment(10, "Video/Animation/Opening_3", "Opening3", false, false, null, Day1AnimationEnd);
                //Segment seg2 = new Segment(10, "Video/Animation/Opening_4", "Opening3", false, false);
                VideoManager.Instance.PlaySeq(new List<Segment> { seg });
                SkipBtn.gameObject.SetActive(false);
                break;

            case 24:
                m_GameModel.GameOverState = "DeadManNoSay";
                Game.Instance.LoadScene(3);
                break;
            case 23://报社记者对话正常结束
                SendEvent(Const.E_DialogEnd);
                m_GameModel.day3_ReporterWang = true;
                SendEvent(Const.E_DayEndCheck, m_GameModel.DayEndCheck());
                break;
        }
        dialogSystem.SetActive(false);
    }

    private void Day1AnimationEnd()
    {
        VideoManager.Instance.ShowImage(10, "Day1_Desk", "Image/CG/Day1_Desk", false);

    }

    public void Skip()
    {
        VideoManager.Instance.StopAll();
        VideoManager.Instance.ShowImage(10, "Day1_Desk", "Image/CG/Day1_Desk", false);
        dialogSystem.SetActive(false);
        SkipBtn.gameObject.SetActive(false);

    }

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Const.E_TriggerDialogue);
    }

    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {
            case Const.E_TriggerDialogue:
                int id = (int)obj;
                Triggers[id].TriggerDialogue();
                break;
        }
    }
}
