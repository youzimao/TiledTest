using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLight : MonoBehaviour
{
   public Light2D gl;
    // Start is called before the first frame update
    void Start()
    {
        gl.intensity = 0.05f;
    }

}
