using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelManager : SingletonMono<PanelManager>
{
   
    public List<Transform> panels;
  
  

 
    public GameObject OpenPanel(string panelName)
    {
        foreach (var item in panels)
        {
            if (item.name == panelName)
            {
                item.gameObject.SetActive(true);
                return item.gameObject;
            }
        }
        return null;
    }
    public void ClosePanel(string panelName)
    {
        foreach (var item in panels)
        {
            if (item.name == panelName)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
    public void ClosePanel(string panelName,float delay)
    {
        foreach (var item in panels)
        {
            if (item.name == panelName)
            {
                StartCoroutine(ClosePanelC(panelName, delay));
            }
        }
    }
    IEnumerator ClosePanelC(string pName, float t)
    {
        yield return new WaitForSeconds(t);
        ClosePanel(pName);
    }

    private void Start()
    {
        foreach (var item in panels)
        {
            ClosePanel(item.name);
        }
       

    }
    #region test

    [ContextMenu("open npc")]
    public void Test()
    {
        OpenPanel("npcDialogPanel");
    }
    [ContextMenu("close npc")]
    public void Test0()
    {
        ClosePanel("npcDialogPanel");
    }
    [ContextMenu("open tip")]
    public void Test1()
    {
        OpenPanel("tipPanel");
    }
    [ContextMenu("close tip")]
    public void Test2()
    {
        ClosePanel("tipPanel");
    }


    #endregion
}
