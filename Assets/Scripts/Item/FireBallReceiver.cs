using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallReceiver : MonoBehaviour
{
    public CommTrigger trigger;
    public Transform pLight;
    public BaseMechanism mechanism;
    private void Start()
    {
        pLight.gameObject.SetActive(false);
        trigger.SetCheckTag("fireball");
        trigger.colliderTriggerAction += (Collider2D c) =>
        {
            if (c.CompareTag("fireball"))
            {
                //TODO 打开对应开关
                c.GetComponent<FireBall>().DetroyFireBall();
                mechanism.mechanismTrigger.Invoke();
                this.GetComponent<Animator>().Play("triggerAnim");
                pLight.gameObject.SetActive(true);
            }
        };
    }
}
