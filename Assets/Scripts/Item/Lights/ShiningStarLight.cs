using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering.Universal;

public class ShiningStarLight : BaseLight
{
    public Light2D l2;
    public float minL=0.5f;
    public float maxL=1.5f;
    public float rate=0.02f;
    private void Start()
    {
        l2 = GetComponent<Light2D>();
    }
    /// <summary>
    /// 随机延时时间闪烁
    /// </summary>
    /// <param name="r"></param>
    public void StartShining(int r)
    {
        System.Random random = new System.Random();
        int dt = random.Next(0, r);
        Invoke("Test0", dt);
    }

    [ContextMenu("shining")]
    public void Test0()
    {
        StartShiningByTimes(l2, rate, minL, maxL);
    }
}
