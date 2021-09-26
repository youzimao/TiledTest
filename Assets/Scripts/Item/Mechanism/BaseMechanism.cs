using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseMechanism : MonoBehaviour
{
    public UnityAction mechanismTrigger = delegate {  };
    public bool canBeReTrigger = false;

}
