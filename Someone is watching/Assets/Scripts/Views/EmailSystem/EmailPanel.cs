using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailPanel : MonoBehaviour
{
    Text title;
    Text author;
    Text time;
    Text content;
    // Start is called before the first frame update
    void Start()
    {
        title = transform.Find("Title").GetComponent<Text>();
        author = transform.Find("Name").GetComponent<Text>();
        time = transform.Find("Time").GetComponent<Text>();
        content = transform.Find("Content").GetComponent<Text>();
    }


    public void UpdateEmail(Email email)
    {
        title.text = email.Title;
        author.text = email.Author;
        time.text = email.Time;
        content.text = email.Content;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
