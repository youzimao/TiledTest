using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadPanel : MonoBehaviour
{
    public Button nextBtn, lastBtn;
    public TMP_Text content, title;
    public Button endTipBtn;
    private float playSpeed=0.02f;
    private string[] contentStrs;
    private int index = 0;
    private bool readComplete=false;
    private bool isReading = false;
    private List<bool> readPage = new List<bool>();
    private void Start()
    {
        readComplete=false;
        endTipBtn.gameObject.SetActive(false);
        nextBtn.onClick.AddListener(NextPage);   
        lastBtn.onClick.AddListener(LastPage);   
        endTipBtn.onClick.AddListener(CloseReadPanel);       
    }
    private void Update()
    {
        if (readComplete&&(Input.GetKeyDown(KeyCode.Space) ))
        {
            CloseReadPanel();  
        }
        if (isReading&&(Input.GetKeyDown(KeyCode.Q) ))
        {
            LastPage();
        }
        if (isReading&&(Input.GetKeyDown(KeyCode.E) ))
        {
            NextPage();
        }
    }
    public void SetReadPanel(string title, string[] contents,float playSpeed=0.02f)
    {
        GameManager.GetT().canAtk = false;
        GameManager.GetT().canMove = false;
        isReading = true;
        this.title.text = title;
        this.contentStrs = contents;
        this.playSpeed = playSpeed;
        if (contents.Length==1)
        {
            nextBtn.gameObject.SetActive(false);
            lastBtn.gameObject.SetActive(false);
            endTipBtn.gameObject.SetActive(true);
        }
        for (int i = 0; i < contentStrs.Length; i++)
        {
            readPage.Add(false);
        }
        StartCoroutine(ShowDialogByChar(contentStrs[index]));
        //this.content.text = contentStrs[index];
        Debug.Log(contentStrs.Length);
    }
    bool isShowCompleted = true;
    IEnumerator ShowDialogByChar(string _content, float r = 0.02f)
    {
        content.text = string.Empty;
        isShowCompleted = false;
        string rich = string.Empty;
        for (int i = 0; i < _content.Length; i++)
        {
            ///使用富文本标签
            if (_content[i] == "<".ToCharArray()[0])
            {
                int t = i;
                for (int j = t; j < _content.Length; j++, i++)
                {
                    if (_content[j] == ">".ToCharArray()[0])
                    {
                        rich += ">";
                        content.text += rich;
                        break;
                    }
                    else
                    {
                        rich += _content[j];
                    }
                }
            }
            else
            {
                content.text += _content[i];
            }
            yield return new WaitForSeconds(r);
        }
        isShowCompleted = true;
        readPage[index] = true;
        if (index==contentStrs.Length-1)
        {
            endTipBtn.gameObject.SetActive(true);
        }
    }

    private void CloseReadPanel()
    {
        index = 0;
        readComplete = false;
        isReading = false;
        GameManager.GetT().canAtk = true;
        GameManager.GetT().canMove = true;
        PanelManager.GetT().ClosePanel(this.name);
        endTipBtn.gameObject.SetActive(false);
        readPage.Clear();
    }
    private void NextPage()
    {
        if (!isShowCompleted)
        {
            return;
        }
        if (index +1< contentStrs.Length)
        {
            index++;
            if (readPage[index])
            {
                this.content.text = contentStrs[index];
            }
            else
            {
                Debug.Log(contentStrs[index]);
                StartCoroutine(ShowDialogByChar(contentStrs[index],playSpeed));
            }
        }
        else
        {
            endTipBtn.gameObject.SetActive(true);
            readComplete = true;
        }
    }
    private void LastPage()
    {
        if (!isShowCompleted)
        {
            return;
        }
        if (index > 0)
        {
            index--;
            Debug.Log("index:"+index);
            if (readPage[index])
            {
                this.content.text = contentStrs[index];

            }
            else
            {
                StartCoroutine(ShowDialogByChar(contentStrs[index],playSpeed));
            }
        }
        else
        {
            // Debug.Log("已到第一页");
        }


    }
}
