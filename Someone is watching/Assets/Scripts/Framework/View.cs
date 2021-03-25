using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class View : MonoBehaviour
{
    public abstract string Name { get; }
    public List<string> AttentionEvents = new List<string>();

    public abstract void HandleEvent(string eventName, object obj);



    //obtain model
    protected Model GetModel<T>()
        where T : Model
    {
        return MVC.GetModel<T>();
    }



    public virtual void RegisterEvents()
    {

    }
    public void SendEvent(string eventName,object data = null)
    {
        MVC.SendEvent(eventName, data);
    }

    //send event

}
