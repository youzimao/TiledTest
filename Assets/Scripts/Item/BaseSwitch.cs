using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseSwitch : MonoBehaviour
{
    public UnityAction switchTrigger=delegate { };
    public UnityAction<Vector2> switchTriggerDir=delegate { };

}
