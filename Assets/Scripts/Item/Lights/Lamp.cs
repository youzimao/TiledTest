using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Lamp : BaseLight
{
    public Light2D freeform;
    public Light2D point;
    public float minL, maxL;
    public float rate;
    [Header("延迟时间")]
    public int delayminT;
    public int delaymaxT;
    [Header("闪烁次数")]
    public int flashMinTimes;
    public int flashMaxTimes;
    private void Start()
    {
        StartCoroutine(RandomShining());

    }
    private Coroutine shiningC;
   IEnumerator RandomShining()
    {
        while (true)
        {
            System.Random r = new System.Random();
            float dt = r.Next(delayminT, delaymaxT+1);
            int fTimes = r.Next(flashMinTimes, flashMaxTimes+1);          
            yield return new WaitForSeconds(dt);
            StartShiningByTimes(point, rate, minL, maxL, fTimes);
            //if (shiningC==null)
            //{
            //    shiningC = StartShiningByTimes(point, rate, minL, maxL, fTimes);
            //}
            //else
            //{
            //    StopCoroutine(shiningC);
            //    shiningC = null;
            //    shiningC = StartShiningByTimes(point, rate, minL, maxL, fTimes);
            //}
        }
    }
    [ContextMenu("test1")]
    public void Test1()
    {
        StartShiningByTimes(point, rate, minL, maxL, 3);
    }
}
