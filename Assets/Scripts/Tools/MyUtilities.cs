using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;
using System.Linq;
public class MyUtilities
{
    #region 检测相关

    public static Collider2D OverLipBox(Vector2 origin, Vector2 size, int laymask, Color color)
    {
        Collider2D c;
        Vector3 originPos = new Vector3(origin.x, origin.y, 0);
        Debug.DrawLine(originPos + new Vector3(-size.x / 2, -size.y / 2, 0), originPos + new Vector3(size.x / 2, -size.y / 2, 0), color);
        Debug.DrawLine(originPos + new Vector3(-size.x / 2, -size.y / 2, 0), originPos + new Vector3(-size.x / 2, size.y / 2, 0), color);
        Debug.DrawLine(originPos + new Vector3(size.x / 2, size.y / 2, 0), originPos + new Vector3(size.x / 2, -size.y / 2, 0), color);
        Debug.DrawLine(originPos + new Vector3(size.x / 2, size.y / 2, 0), originPos + new Vector3(-size.x / 2, size.y / 2, 0), color);
        if (laymask == -1)
        {
            c = Physics2D.OverlapBox(origin, size, 0, LayerMask.GetMask("ground"));
        }
        else
        {
            c = Physics2D.OverlapBox(origin, size, 0, laymask);
        }
        if (c != null)
        {
            return c;
        }
        else
        {
            return null;
        }
    }
    public static Collider2D OverLipBox(Vector2 origin, Vector2 size, Vector2 offset, int laymask)
    {
        return OverLipBox(origin + offset, size, laymask, Color.green);
    }

    public static Collider2D[] OverLipAllBox(Vector2 origin, Vector2 size, Vector2 offset, int laymask,Color color)
    {

        Collider2D[] c;
        Vector3 originPos = new Vector3(origin.x, origin.y, 0)+new Vector3(offset.x,offset.y,0);
        Debug.DrawLine(originPos + new Vector3(-size.x / 2, -size.y / 2, 0), originPos + new Vector3(size.x / 2, -size.y / 2, 0), color);
        Debug.DrawLine(originPos + new Vector3(-size.x / 2, -size.y / 2, 0), originPos + new Vector3(-size.x / 2, size.y / 2, 0), color);
        Debug.DrawLine(originPos + new Vector3(size.x / 2, size.y / 2, 0), originPos + new Vector3(size.x / 2, -size.y / 2, 0), color);
        Debug.DrawLine(originPos + new Vector3(size.x / 2, size.y / 2, 0), originPos + new Vector3(-size.x / 2, size.y / 2, 0), color);
        if (laymask == -1)
        {
            c = Physics2D.OverlapBoxAll(origin+offset, size, 0, LayerMask.GetMask("ground"));
        }
        else
        {
            c = Physics2D.OverlapBoxAll(origin+offset, size, 0, laymask);
        }
        if (c != null)
        {
            return c;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// player地面检测
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="size"></param>
    /// <param name="offset"></param>
    /// <param name="layermask"></param>
    /// <returns></returns>
    public static List<Vector2> BoxHitGround(Vector2 origin, Vector2 size,Vector2 offset, int layermask)
    {
        return BoxHitGround(origin+offset, size, layermask, Color.green);
    }
    public static List<Vector2> BoxHitGround(Vector2 origin, Vector2 size, int layermask, Color color)
    {
        RaycastHit2D[] hits;
        List<Vector2> list = new List<Vector2>();
        Vector3 originPos = new Vector3(origin.x, origin.y, 0);
        Debug.DrawLine(originPos + new Vector3(-size.x / 2, -size.y / 2, 0), originPos + new Vector3(size.x / 2, -size.y / 2, 0), color);
        Debug.DrawLine(originPos + new Vector3(-size.x / 2, -size.y / 2, 0), originPos + new Vector3(-size.x / 2, size.y / 2, 0), color);
        Debug.DrawLine(originPos + new Vector3(size.x / 2, size.y / 2, 0), originPos + new Vector3(size.x / 2, -size.y / 2, 0), color);
        Debug.DrawLine(originPos + new Vector3(size.x / 2, size.y / 2, 0), originPos + new Vector3(-size.x / 2, size.y / 2, 0), color);
        if (layermask == -1)
        {
            hits = Physics2D.BoxCastAll(origin, size, 0, Vector2.zero, 0, LayerMask.GetMask("ground"));
        }
        else
        {
            hits = Physics2D.BoxCastAll(origin, size, 0, Vector2.zero, 0, layermask);
        }
        if (hits.Length > 0)
        {
            foreach (var hti in hits)
            {

                list.Add(hti.point);

            }
        }
        return list;
    }
    public static List<Vector2> BoxHitGround(Vector2 origin, Vector2 size, Vector2 offset, int layermask, Color color)
    {
        return BoxHitGround(origin + offset, size, layermask, color);
    }

    #endregion
    #region debug draw
    /// <summary>
    /// Debug.DrawBox()
    /// </summary>
    /// <param name="origin">中心点</param>
    /// <param name="size"></param>
    /// <param name="color"></param>
    public static void DebugDraw(Vector2 origin, Vector2 size, Color color)
    {
        Vector3 originPos = new Vector3(origin.x, origin.y, 0);
        Debug.DrawLine(originPos + new Vector3(-size.x / 2, -size.y / 2, 0), originPos + new Vector3(size.x / 2, -size.y / 2, 0), color);
        Debug.DrawLine(originPos + new Vector3(-size.x / 2, -size.y / 2, 0), originPos + new Vector3(-size.x / 2, size.y / 2, 0), color);
        Debug.DrawLine(originPos + new Vector3(size.x / 2, size.y / 2, 0), originPos + new Vector3(size.x / 2, -size.y / 2, 0), color);
        Debug.DrawLine(originPos + new Vector3(size.x / 2, size.y / 2, 0), originPos + new Vector3(-size.x / 2, size.y / 2, 0), color);
    }
    /// <summary>
    /// Debug.DrawBox()
    /// </summary>
    /// <param name="origin">中心点</param>
    /// <param name="size"></param>
    /// <param name="color"></param>
    public static void DebugDraw(Vector2 origin, Vector2 size)
    {
        DebugDraw(origin, size, Color.green);
    }
    /// <summary>
    /// Debug.DrawBox()
    /// </summary>
    /// <param name="origin">中心点</param>
    /// <param name="size"></param>
    /// <param name="color"></param>
    public static void DebugDraw(Vector3 origin, Vector3 size)
    {
        DebugDraw(origin, size, Color.green);
    }
    /// <summary>
    /// Debug.DrawBox()
    /// </summary>
    /// <param name="origin">中心点</param>
    /// <param name="size"></param>
    /// <param name="color"></param>
    public static void DebugDraw(Vector3 origin, Vector3 size, Color color)
    {
        DebugDraw(origin, size, color);
    }
    public static void DebugDraw(Vector3 origin, Vector2 size)
    {
        Vector2 origin0 = new Vector2(origin.x, origin.y);
        DebugDraw(origin0, size, Color.green);
    }
    public static void DebugDraw(Vector3 origin, Vector2 size, Color color)
    {
        Vector2 origin0 = new Vector2(origin.x, origin.y);
        DebugDraw(origin0, size, color);
    }
#endregion

    #region XML
    /// <summary>
    /// 读取xml文件中的对话内容
    /// </summary>
    /// <param name="fileName">xml文件名，不需要加后缀</param>
    /// <param name="nodeName">对话节点名</param>
    /// <returns></returns>
    public static string[] GetDialogFromXml(string fileName, string nodeName)
    {
        XmlDocument xmlDoc = new XmlDocument();
        // xmlDoc.Load(Application.dataPath + "/Resources/" + fileName + ".xml");
        TextAsset tt = (TextAsset)Resources.Load(Strings.XmlDir + "/" + Strings.XmlDiaDir + "/" + fileName);
        xmlDoc.LoadXml(tt.text);
        Resources.UnloadAsset(tt);
        XmlNodeList xmlNode = xmlDoc.GetElementsByTagName(nodeName);

        XmlNodeList xmlNlist = xmlNode[0].SelectNodes("a");
        //  Debug.Log(xmlNlist.Count);
        string[] temp = new string[xmlNlist.Count];
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = xmlNlist[i].InnerText;
           
        }
        return temp;
    }
    /// <summary>
    /// 向xml文件中添加对话，若该对话已存在默认更新对话内容
    /// </summary>
    /// <param name="fileName">xml文件名，不需要加后缀</param>
    /// <param name="dialogName">该对话的名称</param>
    /// <param name="dialogContent">对话内容</param>
    /// <param name="isUpdata">若该节点已存在，是否直接更新该节点内容，默认为true</param>
    public static void AddDialogToXml(string fileName, string dialogName, string[] dialogContent, bool isUpdata = true)
    {
        string path = Application.dataPath + "/Resources/" + Strings.XmlDir + "/" + Strings.XmlDiaDir + "/" + fileName + ".xml";
        //string path = Application.dataPath + "/Resources/" + Strings.XmlDir + "/" + Strings.XmlDiaDir + "/" + fileName + ".xml";
       // //TextAsset asset = Resources.Load<TextAsset>(Strings.XmlDir + "/" + Strings.XmlDiaDir + "/" + fileName);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(path);
        XmlElement root = xmlDoc.DocumentElement;//获取根节点
        XmlNodeList nodeList = root.GetElementsByTagName(dialogName);
        if (nodeList.Count > 0)
        {
            if (isUpdata)
            {
                Debug.Log("更新");
                UpdateNode(fileName, dialogName, dialogContent);
            }
            else
            {
                Debug.Log("位置：Utilities.cs，原因：该节点已存在");
                return;
            }

        }
        else
        {
            XmlElement subRoot = xmlDoc.CreateElement(dialogName);
            for (int i = 0; i < dialogContent.Length; i++)
            {
                XmlElement t = xmlDoc.CreateElement("a");
                t.InnerText = dialogContent[i];
                subRoot.AppendChild(t);
            }
            root.AppendChild(subRoot);
            xmlDoc.Save(path);
        }
        
    }
    /// <summary>
    /// 更新节点内容
    /// </summary>
    /// <param name="fileName">xml文件名</param>
    /// <param name="dialogName">对话节点名</param>
    /// <param name="dialogContent">更新内容</param>
    public static void UpdateNode(string fileName, string dialogName, string[] dialogContent)
    {
        string path = Application.dataPath + "/Resources/" + Strings.XmlDir + "/" + Strings.XmlDiaDir + "/" + fileName + ".xml";
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(path);
        XmlElement root = xmlDoc.DocumentElement;//获取根节点
        XmlNode subNode = root.SelectSingleNode(dialogName);
        if (subNode == null)
        {
            Debug.Log("位置：Utilities.cs，原因：该节点不存在，请先添加该节点");
            return;
        }
        root.RemoveChild(subNode);
        Debug.Log("删除节点" + subNode.Name);
        xmlDoc.Save(path);
        AddDialogToXml(fileName, dialogName, dialogContent);
    }
   
    public static string[] GetStrsFromXml(string fileName,string strName)
    {
        XmlDocument xmlDoc = new XmlDocument();
        // xmlDoc.Load(Application.dataPath + "/Resources/" + fileName + ".xml");
        TextAsset tt = (TextAsset)Resources.Load(Strings.XmlDir + "/" + Strings.XmlDiaDir + "/" + fileName);
        xmlDoc.LoadXml(tt.text);
        Resources.UnloadAsset(tt);
        XmlNodeList xmlNode = xmlDoc.GetElementsByTagName(strName);
        XmlNodeList xmlNlist = xmlNode[0].SelectNodes("a");
        string[] temp = new string[xmlNlist.Count];
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = xmlNlist[i].InnerText.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "").Trim();
            //string l_strResult = str.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");

            //temp[i] = xmlNlist[i].InnerText.Replace("\n","").Trim();
        }
        return temp;
    }
    #endregion
    #region 杂项
    /// <summary>
    /// 获取脚本文件的路径
    /// </summary>
    /// <param name="_scriptName"></param>
    /// <returns></returns>
   public static string GetPath(string _scriptName)
    {
        string[] path = UnityEditor.AssetDatabase.FindAssets(_scriptName);
        if (path.Length > 1)
        {
            Debug.LogError("有同名文件" + _scriptName + "获取路径失败");
            return null;
        }
        if (path.Length==0)
        {
            Debug.Log("无此文件");
            return null;
        }
        //将字符串中得脚本名字和后缀统统去除掉
        string _path = AssetDatabase.GUIDToAssetPath(path[0]);//.Replace((@"/" + _scriptName + ".cs"), "");
        return _path;
    }
    public static GUIStyle MyGUIStyle()
    {
        GUIStyle myStyle = new GUIStyle()
        {
            fontSize = 16,
            normal = new GUIStyleState() {textColor = Color.white },
            alignment = TextAnchor.MiddleCenter,
        };
        return myStyle;
    }
    #endregion
    #region editor
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_codes">需要添加的代码</param>
    /// <param name="sign">标识</param>
    /// <param name="addToend">是否添加到末尾</param>
    public static void ChangeScript(string[] _codes,string sign,bool addToend=true)
    {
        string startSign ="//"+ sign;
        string endSign = "//End"+sign;
        //读取文件
        string path = "Assets/Scripts/Camera/CameraController.cs";//Assets/Scripts/Camera/CameraController.cs
        string[] codes = File.ReadAllLines(path);
        //写入代码
        List<string> myCodes = codes.ToList<string>();
        //识别标识
        int startSignIndex = -1;
        int endSignIndex = -1;
        for (int i = 0; i < myCodes.Count; i++)
        {
            if (myCodes[i].Trim()== startSign)
            {
                startSignIndex = i;
            }
            if (myCodes[i].Trim()==endSign)
            {
                endSignIndex = i;
            }
        }
        if (addToend)
        {
            if (endSignIndex != -1)
            {
                for (int i = 0; i < _codes.Length; i++)
                {
                    myCodes.Insert(endSignIndex + i, _codes[i]);
                }
            }
        }
        else
        {
            if (startSignIndex != -1)
            {
                for (int i = 0; i < _codes.Length; i++)
                {
                    myCodes.Insert(startSignIndex + i+1, _codes[i]);
                }
            }
        }
       
        //保存文件 
        File.WriteAllLines(path,myCodes.ToArray());
    }
   
    #endregion
}
