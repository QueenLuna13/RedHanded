using UnityEngine;
using TMPro;

public class Pickup : MonoBehaviour
{
    public LayerMask itemLayer; 
    public float interactDistance = 3f; 
    public GameObject pickupUI; 
    public GameObject obtainedKeyUI; 
    public GameObject obtainedStatueUI; 
    public TextMeshProUGUI keyCountText;
    public TextMeshProUGUI statueText;

    private GameObject itemToPickup; 
    private int keyCount = 0; 
    private bool hasStatue = false; 

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance, itemLayer)){
            itemToPickup = hit.collider.gameObject;
            pickupUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E)){
                PickUpItem();
            }
        }else{
            itemToPickup = null;
            pickupUI.SetActive(false);
        }
    }

    void PickUpItem(){
        if (itemToPickup.CompareTag("Key")){
            keyCount++; 
            UpdateKeyCountUI();
        }else if (itemToPickup.CompareTag("Statue")){
            hasStatue = true;
            UpdateStatueUI();
        }

        Destroy(itemToPickup);
    }

    void UpdateKeyCountUI(){
        keyCountText.text = keyCount.ToString();
    }

    void UpdateStatueUI(){
        statueText.text = "1"; 
    }
}