using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public Transform t0, t1;
    public CommTrigger trigger0, trigger1;
    public Direction direction;
    private bool isIn=true;
    private void Start()
    {
        trigger0= t0.GetComponent<CommTrigger>();
        trigger1= t1.GetComponent<CommTrigger>();
        trigger0.SetCheckTag("fireball");
        trigger1.SetCheckTag("fireball");
        trigger0.colliderTriggerAction += OnFireBallC1;
        trigger1.colliderTriggerAction += OnFireBallC0;
    }

    private void OnFireBallC0(Collider2D c)
    {
        if (!isIn)
        {
            isIn = true;
            return;
        }
        else
        {
            isIn = false;
        } 
        c.transform.position = t0.position;
        if (transform.rotation.eulerAngles.z==0)
        {
            c.GetComponent<FireBall>().SetDir(Vector2.right);
        }
        else if(transform.rotation.eulerAngles.z == 90)
        {
            c.GetComponent<FireBall>().SetDir(Vector2.up);
        }
        else if(transform.rotation.eulerAngles.z == 180)
        {
            c.GetComponent<FireBall>().SetDir(Vector2.left);
        } else if(transform.rotation.eulerAngles.z == 270)
        {
            c.GetComponent<FireBall>().SetDir(Vector2.down);
        }

    }
    private void OnFireBallC1(Collider2D c)
    {
        if (!isIn)
        {
            isIn = true;
            return;
        }
        else
        {
            isIn = false;
        }
        c.transform.position = t1.position;
        if (transform.rotation.eulerAngles.z == 0)
        {
            c.GetComponent<FireBall>().SetDir(Vector2.up);
        }
        else if (transform.rotation.eulerAngles.z == 90)
        {
            c.GetComponent<FireBall>().SetDir(Vector2.left);
        }
        else if (transform.rotation.eulerAngles.z == 180)
        {
            c.GetComponent<FireBall>().SetDir(Vector2.down);
        }
        else if (transform.rotation.eulerAngles.z == 270)
        {
            c.GetComponent<FireBall>().SetDir(Vector2.right);
        }


    }

    [ContextMenu("asd")]
    public void Rotation()
    {
        this.transform.Rotate(Vector3.forward, 90f);

    }
}
public enum Direction
{
    up,down,left,right
}