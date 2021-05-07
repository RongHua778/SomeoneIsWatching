using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogTest : MonoBehaviour
{
    public Text sizeText;
    
    //public Text mainText;

    
    public void UpdateText(string text)
    {
        sizeText.text +=("\n\n"+text);
        Debug.Log("YES");
        //mainText.text += text;
    }



}
