using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : SingletonMono<GameManager>
{
    public bool canMove=true;  
    public bool canAtk=true;

  
    #region Test
    public Vector3 worldPos;
    private Coroutine c;
    [ContextMenu("start")]
    public void Test0()
    {
        c = StartCoroutine(TestC());
    }  
    [ContextMenu("stop")]
    public void Test1()
    {
        StopCoroutine(c);
    }
    [ContextMenu("log")]
    public void Test2()
    {
        Debug.Log(c);
    }
    IEnumerator TestC()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            Debug.Log(123);
        }
    }
    #endregion


}
