using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rgbody;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rgbody = GetComponent<Rigidbody2D>();
    }
    public void DetroyFireBall()
    {
        anim.Play("boom");
        rgbody.velocity = Vector2.zero;
        Destroy(this.gameObject, 0.5f);
    }
    public void SetDir(Vector2 dir)
    {
        rgbody.velocity = Vector2.zero;
        if (dir==Vector2.up)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }else if (dir == Vector2.left)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (dir == Vector2.down)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }else if (dir == Vector2.right)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        rgbody.velocity = dir * 6;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("wall"))
        {
            DetroyFireBall();
        }
    }
}
