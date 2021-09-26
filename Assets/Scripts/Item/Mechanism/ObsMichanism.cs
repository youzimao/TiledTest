using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsMichanism : BaseMechanism
{
    public List<Transform> items=new List<Transform>();
    private bool switchOn=true;
    private void Start()
    {
        foreach (Transform item in transform)
        {
            items.Add(item);
        }
        mechanismTrigger += Trigger;
    }
    public void Trigger()
    {
        if (canBeReTrigger)
        {

            if (switchOn)
            {
                StartCoroutine(OpenObs());
                switchOn = false;
            }
            else
            {
                StartCoroutine(CLoseObs());
                switchOn = true;
            }
        }
        else
        {
            StartCoroutine(OpenObs());
        }


    }

    IEnumerator OpenObs()
    {
        foreach (var item in items)
        {
            item.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }
    }
    IEnumerator CLoseObs()
    {
        foreach (var item in items)
        {
            item.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
