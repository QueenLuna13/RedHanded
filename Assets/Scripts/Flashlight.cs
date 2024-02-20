using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    public Light spotlight;
    public GameObject overlayCanvas;
    public GameObject overlayText;

    private bool overlayShown = true;

    // Start is called before the first frame update
    void Start()
    {
        if (spotlight == null){
            spotlight = GetComponentInChildren<Light>();
        }

        if (overlayCanvas != null && overlayText != null){
            overlayCanvas.SetActive(true);
            overlayText.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)){
            if (overlayShown){
                if (spotlight != null){
                    spotlight.enabled = !spotlight.enabled;
                }

                
                overlayShown = false;
                if (overlayCanvas != null){
                    overlayCanvas.SetActive(false);
                    overlayText.SetActive(false);
                }
            }else{
                if (spotlight != null){
                    spotlight.enabled = !spotlight.enabled;
                }
            }
        }
    }
}
