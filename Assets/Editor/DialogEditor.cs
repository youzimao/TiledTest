using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogEditor : EditorWindow
{

    


    string fileName = "Dialogs";
    string _diaName = string.Empty;
    List<string> dias = new List<string>();

    string deleIndex = "-1";
    string addIndex = "-1";
    string labIndex = "0";
    [MenuItem("Window/DialogEditor")]
    static void Inti()
    {
        DialogEditor newTest = EditorWindow.GetWindow<DialogEditor>(typeof(DialogEditor));
        newTest.Show();
    }
    private void OnEnable()
    {
        dias.Clear();
        dias.Add("P");
        dias.Add("");
        dias.Add("N");
        dias.Add("");
    }
    private void OnGUI()
    {
        GUILayout.BeginVertical();
        //diaName
        GUILayout.BeginHorizontal();
        GUILayout.Label("对话名称", GUILayout.Width(60));
        _diaName = GUILayout.TextField(_diaName, GUILayout.MinWidth(60));
        GUILayout.EndHorizontal();
        //path
        GUILayout.BeginHorizontal();
        GUILayout.Label("保存的xml文件（不需要添加后缀）", GUILayout.Width(60));
        fileName = GUILayout.TextField(fileName, GUILayout.MinWidth(60));
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        
        for (int i = 0; i < dias.Count; i++)
        {
            GUILayout.BeginHorizontal();
            labIndex = i.ToString();
             GUILayout.Label(labIndex,GUILayout.MaxWidth(18));
            dias[i] = GUILayout.TextField(dias[i]);
            GUILayout.EndHorizontal();
        }
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("P", GUILayout.MinWidth(50), GUILayout.MaxWidth(100)))
        {
            if (addIndex == "-1")
            {
                dias.Add("P");
                dias.Add("");
            }
            else
            {
                int index = int.Parse(addIndex);
                dias.Insert(index + 1, "P");
                dias.Add("");

            }
        }
        if (GUILayout.Button("N", GUILayout.MinWidth(50), GUILayout.MaxWidth(100)))
        {
            if (addIndex == "-1")
            {
                dias.Add("N");
                dias.Add("");
            }
            else
            {
                int index = int.Parse(addIndex);
                dias.Insert(index + 1, "N");
                dias.Add("");

            }
        }
        if (GUILayout.Button("添加", GUILayout.MinWidth(50), GUILayout.MaxWidth(100)))
        {
            
            if (addIndex == "-1")
            {
                dias.Add("");
            }
            else
            {
                int index = int.Parse(addIndex);
                dias.Insert(index+1,"");
            }
        }
        addIndex = GUILayout.TextField(addIndex, GUILayout.MinWidth(20), GUILayout.MaxWidth(30));
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("删除", GUILayout.MinWidth(50), GUILayout.MaxWidth(100)))
        {
            if (deleIndex=="-1")
            {
                dias.RemoveAt(dias.Count - 1);
            }
            else
            {
                int index = int.Parse(deleIndex);
                dias.RemoveAt(index);
            }
        }
        deleIndex = GUILayout.TextField(deleIndex,GUILayout.MinWidth(20),GUILayout.MaxWidth(30));
        GUILayout.EndHorizontal();
        
        GUILayout.EndHorizontal();
        if (GUILayout.Button("保存", GUILayout.MinWidth(150), GUILayout.MaxWidth(300)))
        {
            int count = dias.Count;
            string[] temp = new string[count];
            for (int i = 0; i < count; i++)
            {
                if (dias[i] != "" && dias[i] != null)
                {
                    temp[i] = dias[i];
                }
            }
            MyUtilities.AddDialogToXml(fileName, _diaName, temp);
        }
    }

    private void OnDestroy()
    {
        dias.Clear();
    }
}
