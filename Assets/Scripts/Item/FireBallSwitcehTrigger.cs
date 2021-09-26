using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSwitcehTrigger : BaseSwitch
{
    public GameObject fireBall;
    public float fireBallSpeed;
    private void Start()
    {
        switchTriggerDir += Trigger;
    }
    private void Trigger(Vector2 dir)
    {
        GameObject go= Instantiate(fireBall, this.transform.position, Quaternion.identity);
        go.GetComponent<FireBall>().SetDir(dir);
    }
}
