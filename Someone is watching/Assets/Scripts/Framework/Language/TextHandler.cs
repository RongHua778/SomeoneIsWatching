using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHandler : MonoBehaviour
{

    [SerializeField]
    private string LangKey = "";
    // Start is called before the first frame update

    private void Start()
    {
        GameEvents.Instance.onLanguageChange += ChangeLanguage;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.onLanguageChange -= ChangeLanguage;
    }

    void ChangeLanguage()
    {
        Text text = this.gameObject.GetComponent<Text>();
        if (text != null)
        {
            var value = LanguageControl.GetValue(LangKey).Replace("\\n","\n");
            text.text = value;
        }
    }

    public void SetText(string key)
    {
        LangKey = key;
        ChangeLanguage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
