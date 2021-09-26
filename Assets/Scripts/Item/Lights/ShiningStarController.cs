using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiningStarController : MonoBehaviour
{
    List<ShiningStarLight> lights = new List<ShiningStarLight>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform item in this.transform)
        {
            lights.Add(item.GetComponent<ShiningStarLight>());
        }
        StartCoroutine(ShiningCoroutine());
    }
    /// <summary>
    /// 依次使lights中的light开始闪烁
    /// </summary>
    /// <returns></returns>
    IEnumerator ShiningCoroutine()
    {
        foreach (ShiningStarLight item in lights)
        {
            item.StartShining(0);
            yield return new WaitForSeconds(0.2f);
        }
    }
 
}
