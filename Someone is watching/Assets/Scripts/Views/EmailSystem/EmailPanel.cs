using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailPanel : MonoBehaviour
{
    TextHandler title;
    //Text author;
    TextHandler time;
    TextHandler content;
    // Start is called before the first frame update
    void Start()
    {
        title = transform.Find("Title").GetComponent<TextHandler>();
        //author = transform.Find("Name").GetComponent<Text>();
        time = transform.Find("Time").GetComponent<TextHandler>();
        content = transform.Find("Content").GetComponent<TextHandler>();
    }


    public void UpdateEmail(Email email)
    {
        title.SetText(email.Title);
        //author.text = email.Author;
        time.SetText(email.Time);
        content.SetText(email.Content);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
