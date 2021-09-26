using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(CameraController))]
public class CmControllerEditor : Editor
{
    GameObject cameraPrefab, confinerPrefab, triggerPrefab;
    string cmIndex;
    List<string> adjoinCmIndex=new List<string>();
    private void Awake()
    {
        cameraPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Camera/CM vcam1.prefab",typeof(GameObject)) as GameObject;
        confinerPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Camera/CM1 Confiner.prefab", typeof(GameObject)) as GameObject;
        triggerPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Camera/Cm0ToCm1.prefab", typeof(GameObject)) as GameObject;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CameraController cameraController = (CameraController)target;

        //垂直布局
        GUILayout.BeginVertical();
        GUILayout.Label("camera prefab");
        EditorGUILayout.ObjectField(cameraPrefab, typeof(GameObject), true);
        GUILayout.Label("confiner prefab");
        EditorGUILayout.ObjectField(confinerPrefab, typeof(GameObject), true);
        GUILayout.Label("trigger prefab");
        EditorGUILayout.ObjectField(triggerPrefab, typeof(GameObject), true);
        GUILayout.BeginVertical();

        GUILayout.EndVertical();

        GUILayout.BeginHorizontal();
        GUILayout.Label("new camera index");
         cmIndex = GUILayout.TextField(cmIndex, GUILayout.MinWidth(50));
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        GUILayout.Label("adjon camera List");
        if (GUILayout.Button("添加"))
        {
            adjoinCmIndex.Add("");
        } 
        if (GUILayout.Button("删减"))
        {
            adjoinCmIndex.RemoveAt(adjoinCmIndex.Count - 1);
        }
        GUILayout.EndHorizontal();
        for (int i = 0; i < adjoinCmIndex.Count; i++)
        {
            adjoinCmIndex[i] = GUILayout.TextField(adjoinCmIndex[i], GUILayout.MaxWidth(60));
        }

       
        if (GUILayout.Button("添加Camera"))
        {
            AddNewCamera(cameraController.camersTf,cameraController.confinersTf,cameraController.switchtriggersTf);
        }
        GUILayout.EndVertical();

        
    }
    private void AddNewCamera(Transform cameraTf,Transform confinerTf,Transform triggerTf)
    {
        if (cmIndex=="")
        {
            return;
        }
        //添加confiner
        GameObject newConfiner = Instantiate(confinerPrefab, confinerTf);
        newConfiner.name = string.Format("CM{0} Confiner", cmIndex);
        //添加camera
        GameObject newCamera = Instantiate(cameraPrefab, cameraTf);
        newCamera.name = "CM vcam" + cmIndex;
        newCamera.GetComponent<Cinemachine.CinemachineConfiner>().m_BoundingShape2D = newConfiner.GetComponent<PolygonCollider2D>();
        newCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = GameObject.Find("player").transform;
        //添加switch trigger

        for (int i = 0; i < adjoinCmIndex.Count; i++)
        {
            //添加两个trigger
            GameObject newTrigger0 = Instantiate(triggerPrefab, triggerTf);
            newTrigger0.name = string.Format("Cm{0}ToCm{1}", cmIndex,adjoinCmIndex[i]);
            GameObject newTrigger1 = Instantiate(triggerPrefab, triggerTf);
            newTrigger1.name = string.Format("Cm{0}ToCm{1}",adjoinCmIndex[i], cmIndex);

            //添加声明代码 	public CamerSwitch Cm2ToCm1;
            string[] code0 = new string[] { 
                string.Format("\tpublic CamerSwitch Cm{0}ToCm{1};", cmIndex, adjoinCmIndex[i])
                , string.Format("\tpublic CamerSwitch Cm{0}ToCm{1};", adjoinCmIndex[i],cmIndex) };
            ChangeScript(code0, "SwitecTrigger");
            //添加获取组件代码      Cm0ToCm1 = switchtriggersTf.Find(nameof(Cm0ToCm1)).GetComponent<CamerSwitch>();
            string[] code1 = new string[] {
                string.Format("\t\t{0} = switchtriggersTf.Find(nameof({0})).GetComponent<CamerSwitch>();",newTrigger0.name) ,
                string.Format("\t\t{0} = switchtriggersTf.Find(nameof({0})).GetComponent<CamerSwitch>();",newTrigger1.name)
            };
            ChangeScript(code1, "GetComponent");
            //添加事件注册代码      Cm0ToCm1.trigger += () => { CameraSwitch(0,1); };
            string[] code2 = new string[] 
            {
               "\t\t"+newTrigger0.name+".trigger += () => { "+string.Format("CameraSwitch({0},{1})", cmIndex,adjoinCmIndex[i])+"; };",
               "\t\t"+newTrigger1.name+".trigger += () => { "+string.Format("CameraSwitch({0},{1})",adjoinCmIndex[i],cmIndex)+"; };"
            };
            ChangeScript(code2, "Action");

        };

        


    }


    //思路
    //第一步：读取脚本  File.ReadAllLines(path);
    //第二步：添加代码 myCodes.Insert(endSignIndex + i, _codes[i]);
    //第三步：保存  File.WriteAllLines(path, myCodes.ToArray());

    public void ChangeScript(string[] _codes, string sign, bool addToend = true)
    {
        
        //读取文件
        string path = "Assets/Scripts/Camera/CameraController.cs";//Assets/Scripts/Camera/CameraController.cs
        string[] codes = File.ReadAllLines(path);
        //写入代码
        List<string> myCodes = codes.ToList<string>();
        //识别标识
        string startSign = "//" + sign;
        string endSign = "//End" + sign;
        int startSignIndex = -1;
        int endSignIndex = -1;
        for (int i = 0; i < myCodes.Count; i++)
        {
            if (myCodes[i].Trim() == startSign)
            {
                startSignIndex = i;
            }
            if (myCodes[i].Trim() == endSign)
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
                    myCodes.Insert(startSignIndex + i + 1, _codes[i]);
                }
            }
        }
        //到此为止，把从CameraCOntroller.cs里的代码行和需要添加的代码合并到了List<string>中
        //保存文件 
        File.WriteAllLines(path, myCodes.ToArray());
    }
}
