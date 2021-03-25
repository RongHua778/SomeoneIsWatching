using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Email
{
    public int ID { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string Time { get; private set; }
    public string Content { get; private set; }

    public bool Star { get; private set; }
    public Email(int id, string title, string author, string time, string content,bool star)
    {
        this.ID = id;
        this.Title = title;
        this.Author = author;
        this.Time = time;
        this.Content = content;
        this.Star = star;
    }
}
