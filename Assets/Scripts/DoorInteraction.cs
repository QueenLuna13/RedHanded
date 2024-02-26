using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorInteraction : MonoBehaviour
{
    public LayerMask itemLayer;
    public float interactDistance = 3f;
    public GameObject doorOpenUI;
    public TextMeshProUGUI keyCountText;
    public TextMeshProUGUI statueText;

    private GameObject statueDoor;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
