using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class Tools 
{
   public static void ShowText(this TextMeshProUGUI txt, MonoBehaviour go, string text, float time, Action callback=null)
    {
        IEnumerator IShowText()
        {
            float timePerChar =  time/ text.Length ;
            string newTxt = "";
            int i = 0;
            while (newTxt.Length < text.Length)
            {
                newTxt+= text[i++];
                txt.text = newTxt;
                yield return new WaitForSeconds(timePerChar);
            }
            callback?.Invoke();
        }
        go.StartCoroutine(IShowText());
    }
}
