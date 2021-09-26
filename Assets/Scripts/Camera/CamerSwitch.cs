using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CamerSwitch : MonoBehaviour
{
    public UnityAction trigger = delegate {  };
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trigger.Invoke();
        }
    }
}
