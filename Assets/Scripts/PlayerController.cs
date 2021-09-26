using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{

    public LayerMask border;
    public float speed;
    private float iX;
    public Animator anim;
    private Rigidbody2D rgbody;
    [Header("atk")]
    public GameObject atkGo;
    public float atkForce;
    private float atkDirX = 1;
    private bool atkComplete=true;
    [Header("move")]
    public float jumpForce;
    public float maxYSpeed;
    public Vector2 size1, size2, offset1, offset2;

    private void Start()
    {
        rgbody=GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        iX = Input.GetAxisRaw("Horizontal");
        atkDirX = iX == 0 ? atkDirX : iX;
        if (GameManager.GetT().canMove)
        {
            Movement();
            Jump();
          
        }
        if (GameManager.GetT().canAtk && (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0)))
        {
            Attack();
        }
        if (rgbody.velocity.y<-maxYSpeed)
        {
            float t = rgbody.velocity.y;
            rgbody.velocity = new Vector2(rgbody.velocity.x,t);
        }
    }


    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            List<Vector2> red = MyUtilities.BoxHitGround(new Vector2(transform.position.x, transform.position.y), size2, offset2, -1, Color.red);
            List<Vector2> green = MyUtilities.BoxHitGround(new Vector2(transform.position.x, transform.position.y), size1, offset1, -1);
            //Debug.Log(red.Count);
            //Debug.Log(green.Count);
            if (red.Count>0||green.Count>0)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
            }
        }
    }
    private void Movement()
    {
        if (iX != 0)
        {
            if (!MyUtilities.OverLipBox(new Vector2(transform.position.x, transform.position.y), new Vector2(0.1f, 0.2f), Vector2.up * 0.2f + Vector2.right * iX * 0.3f, -1) &&
            !MyUtilities.OverLipBox(new Vector2(transform.position.x, transform.position.y), new Vector2(0.1f, 0.2f), Vector2.up * 0.2f + Vector2.right * iX * 0.3f, border)
                )
            {
                transform.Translate(Vector3.right * iX * speed * Time.deltaTime);
            }

            anim.Play("run");
            Flip(iX);
        }
        else
        {
            anim.Play("idle");
        }
        
    }
    private void Attack()
    {
        if (atkComplete)
        {
            atkComplete = false;
            StartCoroutine(AtkCorotinue());
        }
        //atkOffset *= atkDirX;
    }

    IEnumerator AtkCorotinue()
    {
        atkGo.SetActive(true);
        Collider2D[] cl = MyUtilities.OverLipAllBox(new Vector2(transform.position.x, transform.position.y), new Vector2(1,0.2f), new Vector2(0.8f * atkDirX, 0), LayerMask.GetMask("prop"), Color.red);

        if (cl.Length > 0)
        {
            foreach (var item in cl)
            {
                //if (item.CompareTag("Npc"))
                //{
                //    Destroy(item.gameObject);
                //}
                //else
                //if (item.CompareTag("Item"))
                //{
                //    float dirX = (item.transform.position.x - this.transform.position.x) < 0 ? -1 : 1;
                //    item.GetComponent<Rigidbody2D>().AddForce(Vector2.right * dirX * atkForce);
                //}
                //else
                if (item.CompareTag("switch"))
                {
                    Vector2 dir = item.transform.position - this.transform.position;
                    dir.y = 0;
                    item.GetComponent<BaseSwitch>().switchTriggerDir(dir.normalized);
                    item.GetComponent<BaseSwitch>().switchTrigger();
                }
                else
                if (item.CompareTag("mirror"))
                {
                    item.GetComponent<Mirror>().Rotation();
                    //Debug.Log(123);
                }
            }
        }
        yield return new WaitForSeconds(0.25f);
        atkGo.SetActive(false);
        atkComplete = true;
    }

    private void Flip(float x)
    {
        transform.localScale = new Vector3(x, 1, 1);
    }
    public void Light()
    {

    }
    [ContextMenu("开启所有技能")]
    public void OnAllSkill()
    {
        GameManager.GetT().canAtk = true;
        Debug.Log("开启");

    }
    [ContextMenu("关闭所有技能")]
    public void OffAllSkill()
    {
        GameManager.GetT().canAtk = false;
        Debug.Log("关闭");

    }
}
