using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Pickup : MonoBehaviour
{
    public LayerMask itemLayer;
    public float interactDistance = 3f;
    public GameObject pickupUI;
    public GameObject obtainedKeyUI;
    public GameObject obtainedStatueUI;
    public GameObject doorOpenUI;

    public TextMeshProUGUI keyCountText;
    public TextMeshProUGUI statueText;

    private GameObject interact;
    private int keyCount = 0;
    private int statueCount = 0;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance, itemLayer)) {
            interact = hit.collider.gameObject;
            doorOrPickup();
            if (Input.GetKeyDown(KeyCode.E)) {
                Pressed();
            }
        } else {
            interact = null;
            pickupUI.SetActive(false);
            doorOpenUI.SetActive(false);
        }
    }
    void doorOrPickup()
    {
        if (interact.CompareTag("Key") || interact.CompareTag("Statue"))
        {
            pickupUI.SetActive(true);
        }
        else
        {
            doorOpenUI.SetActive(true);
        }
    }
    void Pressed()
    {
        if (interact.CompareTag("Key") || interact.CompareTag("Statue"))
        {
            PickUpItem();
        }
        else
        {
            openDoor();
        }
    }



    void PickUpItem() {
        if (interact.CompareTag("Key")) {
            obtainedKeyUI.SetActive(true);
            Invoke("HideObtainedUI", 2f);
            keyCount++;
            UpdateKeyCountUI();
        }
        else if (interact.CompareTag("Statue")) {
            obtainedStatueUI.SetActive(true);
            Invoke("HideObtainedUI", 2f);
            statueCount++;
            UpdateStatueCountUI();

        }

        Destroy(interact);
    }

    void openDoor()
    {
        if (interact.CompareTag("statueDoor"))
        {
            if (keyCount == 2)
            {
                //play animation
                keyCount = keyCount - 2;
                UpdateKeyCountUI();
            }
            else
            {
                //ui saying the door is locked
            }
        }
        else if (interact.CompareTag("exitDoor"))
        {
            if (statueCount == 1)
            {
                //play animation
                statueCount--;
                UpdateStatueCountUI();
            }
            else
            {
                //ui saying you're here for the statue
            }
        }
    }

    void HideObtainedUI() {
        obtainedKeyUI.SetActive(false);
        obtainedStatueUI.SetActive(false);
    }

    void UpdateKeyCountUI() {
        keyCountText.text = keyCount.ToString();
    }

    void UpdateStatueCountUI()
    {
        statueText.text = statueCount.ToString();
    }
}