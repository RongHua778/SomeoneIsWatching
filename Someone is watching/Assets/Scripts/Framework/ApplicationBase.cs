using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ApplicationBase<T> : Singleton<T>
    where T : MonoBehaviour
{
    //register controller
    protected void RegisterController(string eventName,Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }

    protected void SendEvent(string eventName,object data=null)
    {
        MVC.SendEvent(eventName,data);
        
    }

}
