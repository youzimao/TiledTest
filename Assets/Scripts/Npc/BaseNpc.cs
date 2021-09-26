using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class BaseNpc : MonoBehaviour
{
    #region 对话功能相关
    /// <summary>
    /// 要显示的对话内容
    /// </summary>
    private string[] dialogContent;
    //对话框UI上的tmp
    private TMP_Text content;
    //名字对应的tmp
    private TMP_Text cName;
    //角色的头像
    private Image icon;
    //对话句子的索引
    private int index = 0;
    [Header("对话相关")]
    //随机对话内容
    public List<string> dialogNames;
    //当前对话的name
    public string currentDialogName;
    /// <summary>
    /// npc的name
    /// </summary>
    public string npcName="null";
    /// <summary>
    /// npc的icon
    /// </summary>
    public Sprite npcIcon;
    /// <summary>
    /// 可触发对话范围
    /// </summary>
    public Vector2 triggerSize;
    public Vector2 triggerOffset;
    public GameObject canTalkTipIcon;
    [Header("对话播放速度")]
    public float playSpeed;
    #endregion
    public event UnityAction DialogPlayComplete = delegate { };
    // Start is called before the first frame update
    protected virtual void Start()
    {
        dialogContent = MyUtilities.GetDialogFromXml(Strings.DialogXmlFileName, currentDialogName);
        canTalkTipIcon.SetActive(false);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        TalkTip();
    }

    bool inTalkArea = false;
    bool isPlayer = false;
    /// <summary>
    /// 检测是否在可对话范围内
    /// </summary>
    protected void TalkTip()
    {
       // MyUtilities.DebugDraw(this.transform.position, triggerOffset);
        Collider2D[] c = MyUtilities.OverLipAllBox(transform.position, triggerSize, triggerOffset, LayerMask.GetMask("player"), Color.blue);
        if (c != null)
        {
            isPlayer = false;
            foreach (var item in c)
            {
                if (item.CompareTag("Player"))
                {
                    isPlayer = true;
                }
            }
            inTalkArea = isPlayer;
        }
        if (inTalkArea)
        {
            canTalkTipIcon.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Talk();
            }
        }
        else
        {
            canTalkTipIcon.SetActive(false);
        }
    }


    private bool isPanelOpened = false;
    private bool isShowCompleted = true;
    //当前打开的对话panel
    private string currentPanel;
    protected void Talk()
    {
        GameManager.GetT().canMove = false;
        GameManager.GetT().canAtk = false;
        if (!isShowCompleted)
        {
            return;
        }
        if (isPanelOpened)
        {
            if (index < dialogContent.Length)
            {
                if (dialogContent[index].StartsWith("P") && dialogContent[index].Length == 1)
                {
                    SwitchDialogPanel("playerDialogPanel");
                    index++;
                }
                if (dialogContent[index].StartsWith("N") && dialogContent[index].Length == 1)
                {
                    SwitchDialogPanel("npcDialogPanel");
                    index++;
                }
                content.text = string.Empty;
                // content.text = dialogContent[index];//直接显示
                StartCoroutine(ShowDialogByChar(dialogContent[index]));//逐字显示
                index++;
            }
            //超出索引，对话结束
            else
            {
                DialogPlayComplete.Invoke();              
                PanelManager.GetT().ClosePanel(currentPanel);
                isPanelOpened = false;
                index = 0;
                GameManager.GetT().canMove = true;
                GameManager.GetT().canAtk = true;
            }

        }
        
        else
        {
            //打开player的对话框
            if (dialogContent[index].StartsWith("P") && dialogContent[index].Length == 1)
            {
                SwitchDialogPanel("playerDialogPanel");
                isPanelOpened = true;
                index++;
            }
            //打开npc的对话框
            if (dialogContent[index].StartsWith("N") && dialogContent[index].Length == 1)
            {
                SwitchDialogPanel("npcDialogPanel");
                isPanelOpened = true;
                index++;
            }
            StartCoroutine(ShowDialogByChar(dialogContent[index]));
            index++;
        }
    }
    IEnumerator ShowDialogByChar(string _content)
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
            
            yield return new WaitForSeconds(0.1f/(playSpeed <= 0 ? 1 : playSpeed));
        }
        isShowCompleted = true;
    }
    /// <summary>
    /// 切换或打开对话panel
    /// </summary>
    /// <param name="panelName"></param>
    protected void SwitchDialogPanel(string panelName)
    {
        PanelManager.GetT().ClosePanel(currentPanel);
        GameObject t = PanelManager.GetT().OpenPanel(panelName);
        currentPanel = panelName;       
        foreach (Transform item in t.transform)
        {
            if (item.name=="content")
            {
                content = item.GetComponent<TMP_Text>();
            }else
            if (item.name=="name")
            {
                cName = item.GetComponent<TMP_Text>();
                if (panelName=="playerDialogPanel")
                {
                    cName.text = Strings.PlayerName;
                }
                else
                {
                    cName.text = npcName;
                }
            }
            else if (item.name=="icon")
            {
                icon = item.GetComponent<Image>();
                if (panelName == "playerDialogPanel")
                {
                    //TODO 
                    
                    //icon.sprite = PlayerIcon;
                }
                else
                {
                    icon.sprite = npcIcon;
                }
            }
        }
    }
    /// <summary>
    /// 设置当前对话内容，如果随机，则会从dialogNames中随机选取内容
    /// </summary>
    /// <param name="isRandom"></param>
    public void SetCurrentDialog(bool isRandom=false)
    {
        if (isRandom)
        {
            System.Random random = new System.Random();
            int index = random.Next(0, dialogNames.Count);
            currentDialogName = dialogNames[index];
            dialogContent = MyUtilities.GetDialogFromXml(Strings.DialogXmlFileName, dialogNames[index]);
        }
        else
        {
            dialogContent = MyUtilities.GetDialogFromXml(Strings.DialogXmlFileName, currentDialogName);
        }

    }
}
