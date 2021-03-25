using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public static class MVC
{
    //save MVC
    public static Dictionary<string, Model> Models = new Dictionary<string, Model>();
    public static Dictionary<string, View> Views = new Dictionary<string, View>();
    public static Dictionary<string, Type> CommandMap = new Dictionary<string, Type>();

    //register
    public static void RegisterModel(Model model)
    {
        Models[model.Name] = model;
    }

    public static void RegisterView(View view)
    {
        if (Views.ContainsKey(view.Name))
            Views.Remove(view.name);
        view.RegisterEvents();

        Views[view.Name] = view;
    }

    public static void RegisterController(string eventName ,Type controllerType)
    {
        CommandMap[eventName] = controllerType;
    }

    //obtain
    public static Model GetModel<T>()
        where T:Model
    {
        foreach(Model m in Models.Values)
        {
            if(m is T)
            {
                return (T)m;
            }
            
        }
        return null;
    }

    public static View GetView<T>()
        where T : View
    {
        foreach(View v in Views.Values)
        {
            if (v is T)
                return (T)v;
            
        }
        return null;
    }

    public static void SendEvent(string eventName,object data = null)
    {
        //controller respond
        if (CommandMap.ContainsKey(eventName))
        {
            Type t = CommandMap[eventName];
            Controller c = Activator.CreateInstance(t) as Controller;
            //controller excute
            c.Execute(data);
        }

        //view respond
        foreach(View v in Views.Values)
        {
            if (v.AttentionEvents.Contains(eventName))
            {
                v.HandleEvent(eventName, data);
            }
        }
    }

    //send event


}
