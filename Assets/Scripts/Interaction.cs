using UnityEngine;
using TMPro;

public class Interaction : MonoBehaviour
{
    public LayerMask itemLayer;
    public float interactDistance = 3f;
    public GameObject pickupUI;
    public GameObject obtainedKeyUI;
    public GameObject obtainedStatueUI;
    public GameObject doorOpenUI;
    public GameObject doorStatueUI;
    public GameObject doorExitUI;
    public TextMeshProUGUI keyCountText;
    public TextMeshProUGUI statueText;

    private GameObject interact;
    private int keyCount = 0;
    private int statueCount = 0;

    private bool statueDoorOpened = false;

    void Update()
    {
        RaycastHit hit;
        bool isInteractable = Physics.Raycast(transform.position, transform.forward, out hit, interactDistance, itemLayer);
        if (isInteractable)
        {
            interact = hit.collider.gameObject;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Pressed();
            }
            else
            {
                doorOrPickup();
            }
        }
        else
        {
            interact = null;
            pickupUI.SetActive(false);
            doorOpenUI.SetActive(false);
        }

        if (statueDoorOpened && interact != null && interact.CompareTag("statueDoor"))
        {
            doorOpenUI.SetActive(false);
            doorStatueUI.SetActive(false);
        }
    }

    void doorOrPickup()
    {
        if (interact.CompareTag("Key") || interact.CompareTag("Statue"))
        {
            pickupUI.SetActive(true);
            doorOpenUI.SetActive(false);
        }
        else
        {
            pickupUI.SetActive(false);
            doorOpenUI.SetActive(true);
        }
    }

    void Pressed()
    {
        if (interact.CompareTag("Key") || interact.CompareTag("Statue"))
        {
            PickUpItem();
        }
        else if (interact.CompareTag("statueDoor") || interact.CompareTag("exitDoor"))
        {
            openDoor();
        }
    }

    void PickUpItem()
    {
        if (interact.CompareTag("Key"))
        {
            obtainedKeyUI.SetActive(true);
            Invoke("HideObtainedUI", 2f);
            keyCount++;
            UpdateKeyCountUI();
        }
        else if (interact.CompareTag("Statue"))
        {
            obtainedStatueUI.SetActive(true);
            Invoke("HideObtainedUI", 2f);
            statueCount++;
            UpdateStatueCountUI();
        }

        Destroy(interact);
    }

    void openDoor()
    {
        Animator doorAnimator = interact.transform.parent.GetComponent<Animator>();
        if (interact.CompareTag("statueDoor"))
        {
            if (keyCount >= 2)
            {
                doorAnimator.SetTrigger("OpenStatue");
                keyCount -= 2;
                UpdateKeyCountUI();
                statueDoorOpened = true;
            }
            else
            {
                doorStatueUI.SetActive(true);
                Invoke("HideObtainedUI", 2f);
            }
        }
        else if (interact.CompareTag("exitDoor"))
        {
            if (statueCount >= 1)
            {
                doorAnimator.SetTrigger("OpenExit");
                statueCount--;
                UpdateStatueCountUI();
            }
            else
            {
                doorExitUI.SetActive(true);
                Invoke("HideObtainedUI", 2f);
            }
        }
    }


    void HideObtainedUI()
    {
        obtainedKeyUI.SetActive(false);
        obtainedStatueUI.SetActive(false);
        doorExitUI.SetActive(false);
        doorStatueUI.SetActive(false);
    }

    void UpdateKeyCountUI()
    {
        keyCountText.text = keyCount.ToString();
    }

    void UpdateStatueCountUI()
    {
        statueText.text = statueCount.ToString();
    }
}
