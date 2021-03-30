using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    protected bool canContinue = true;//用于测试，避免多个instance重复执行
    private static T m_instance = null;
    public static T Instance
    {
        get { return m_instance; }
    }

    protected virtual void Awake()
    {

        if (m_instance == null)
            m_instance = this as T;
        else
        {
            canContinue = false;
            Destroy(this.gameObject);
            Debug.Log("Instance Already Created");
        }
    }

}
