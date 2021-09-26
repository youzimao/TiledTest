using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    PlatformEffector2D platformEffector;
    private void Start()
    {
        platformEffector = GetComponent<PlatformEffector2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StartCoroutine("StayOn");

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        StopCoroutine("StayOn");
    }
   

    IEnumerator StayOn()
    {

        while (true)
        {
            //按下 下键加空格键
            if (Input.GetKey(KeyCode.S)&&Input.GetKeyDown(KeyCode.Space))
            {
                platformEffector.rotationalOffset = 180;
                StartCoroutine("Exit");
            }
            yield return null;
        }
    }

    IEnumerator Exit()
    {
        StopCoroutine("StayOn");
        //0.5s后 平台恢复
        yield return new WaitForSeconds(0.5f);
        platformEffector.rotationalOffset = 0;
    }
}
