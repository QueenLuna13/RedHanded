using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light spotlight;

    // Start is called before the first frame update
    void Start()
    {
        if(spotlight == null){
            spotlight = GetComponentInChildren<Light>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (spotlight != null)
            {
                spotlight.enabled = !spotlight.enabled;
            }
        }
    }
}
