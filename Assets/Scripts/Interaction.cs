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
    public GameObject Glass;
    private int keyCount = 0;
    private int statueCount = 0;
    private AudioManager audioManager;

    private bool statueDoorOpened = false;

    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene.");
        }
    }

    void Update()
    {
        RaycastHit hit;
        bool isInteractable = Physics.Raycast(transform.position, transform.forward, out hit, interactDistance, itemLayer);
        if (isInteractable)
        {
            interact = hit.collider.gameObject;
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButton("Pickup"))
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
            
            if (audioManager != null)
            {
                audioManager.PlayPickupSound(transform.position);
            }
        }
        else if (interact.CompareTag("Statue"))
        {
            obtainedStatueUI.SetActive(true);
            Invoke("HideObtainedUI", 2f);
            statueCount++;
            UpdateStatueCountUI();

            if (audioManager != null)
            {
                audioManager.PlayPickupSound(transform.position);
            }
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
                if (Glass != null)
                {
                    Animator glassAnimator = Glass.GetComponent<Animator>();
                    if (glassAnimator != null)
                    {
                        glassAnimator.SetTrigger("Down"); // Adjust "TriggerAnimation" to match your animation trigger name
                    }
                    else
                    {
                        Debug.LogError("Animator component not found on room animation object.");
                    }
                }
                else
                {
                    Debug.LogError("Room animation object reference is null.");
                }
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

        if (audioManager != null)
        {
            audioManager.PlayDoorOpenSound(transform.position);
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
