using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public List<Transform> cmList = new List<Transform>();
    public Transform confinersTf;
    public Transform switchtriggersTf;
    public Transform camersTf;
    //SwitecTrigger
    public CamerSwitch Cm0ToCm1;
    public CamerSwitch Cm1ToCm0;  	
	public CamerSwitch Cm2ToCm1;
	public CamerSwitch Cm1ToCm2;
    //EndSwitecTrigger

    private void Start()
    {

        //GetComponent        
        Cm0ToCm1 = switchtriggersTf.Find(nameof(Cm0ToCm1)).GetComponent<CamerSwitch>();
        Cm1ToCm0 = switchtriggersTf.Find(nameof(Cm1ToCm0)).GetComponent<CamerSwitch>();
		Cm2ToCm1 = switchtriggersTf.Find(nameof(Cm2ToCm1)).GetComponent<CamerSwitch>();
		Cm1ToCm2 = switchtriggersTf.Find(nameof(Cm1ToCm2)).GetComponent<CamerSwitch>();
        //EndGetComponent

        //Action
        Cm0ToCm1.trigger += () => { CameraSwitch(0,1); };
        Cm1ToCm0.trigger += () => { CameraSwitch(1,0); };
		Cm2ToCm1.trigger += () => { CameraSwitch(2,1); };
		Cm1ToCm2.trigger += () => { CameraSwitch(1,2); };
        //EndAction
       
    }
    private void CameraSwitch(int a,int b)
    {
        cmList[a].gameObject.SetActive(false);
        cmList[b].gameObject.SetActive(true);
    }
   
}
