using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BaseLight : MonoBehaviour
{

    float t = 0;   
    public Coroutine StartShiningByTimes(Light2D light,float rate,float minL,float maxL,int times=0)
    {
       return StartCoroutine(ShiningByTimes(light,minL,maxL,rate,times));
    }
    /// <summary>
    /// light闪烁
    /// </summary>
    /// <param name="l2d"></param>
    /// <param name="minL">最小亮度</param>
    /// <param name="maxL">最大亮度</param>
    /// <param name="rate">闪烁速率</param>
    /// <param name="times">闪烁次数</param>
    /// <returns></returns>
    IEnumerator ShiningByTimes(Light2D l2d, float minL, float maxL, float rate, int times=0)
    {
        int _times = 0;
        while (true)
        {
            l2d.intensity = minL;
            l2d.intensity = Mathf.Lerp(minL, maxL, t);
            t += rate;
            t = Mathf.Clamp01(t);
            yield return new WaitForSeconds(0.02f);
            if (l2d.intensity >= maxL)
            {
                l2d.intensity = maxL;
                while (true)
                {
                    l2d.intensity = Mathf.Lerp(minL, maxL, t);
                    t -= rate;
                    t = Mathf.Clamp01(t);
                    yield return new WaitForSeconds(0.02f);
                    if (l2d.intensity <= minL)
                    {
                      
                        break;
                    }
                }
                //循环一次
                _times++;
                if (times != 0)
                {
                    if (_times >= times)
                    {
                        break;
                    }
                }
            }           
        }
    }

 
    }
   
