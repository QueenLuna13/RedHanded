using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light spotlight;
    public GameObject flashlightOverlay; // Reference to the flashlight overlay child object

    private bool overlayShown = true;

    // Start is called before the first frame update
    void Start()
    {
        if (spotlight == null)
        {
            spotlight = GetComponentInChildren<Light>();
        }

        if (flashlightOverlay != null)
        {
            flashlightOverlay.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetButton("Fire2"))
        {
            if (overlayShown)
            {
                if (spotlight != null)
                {
                    spotlight.enabled = !spotlight.enabled;
                }

                // Disable flashlight overlay after first F key press
                overlayShown = false;
                if (flashlightOverlay != null)
                {
                    flashlightOverlay.SetActive(false);
                }
            }
            else
            {
                // Toggle flashlight
                if (spotlight != null)
                {
                    spotlight.enabled = !spotlight.enabled;
                }
            }
        }
    }
}
