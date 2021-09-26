using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogPlay 
{
    TMP_Text tMP;
    Image icon;
    string[] dialogContent;
    public void SetDialog(TMP_Text tmpText,Sprite icon,string[] content)
    {
        this.tMP = tmpText;
        this.icon.sprite = icon;
        this.dialogContent = content;
    }
   public int index = 0;
    public void Play()
    {
        if (dialogContent[index]=="P"||dialogContent[index]=="N")
        {
            index++;
        }
        tMP.text = dialogContent[index];
        index++;
    }
}
