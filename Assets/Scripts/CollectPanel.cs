using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPanel : SingletonMono<CollectPanel>
{
    public GameObject chip0;
    public GameObject chip1;
    public GameObject chip2;
    public GameObject chip3;
    public void GetChip(int index)
    {
        switch (index)
        {
            case 0:
                chip0.SetActive(true);
                break;
            case 1:
                chip1.SetActive(true);
                break;
            case 2:
                chip2.SetActive(true);
                break;
            case 3:
                chip3.SetActive(true);
                break;
            default:
                break;
        }
    }
}
