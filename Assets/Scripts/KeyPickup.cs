using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public LayerMask itemLayer;
    public float interactDistance = 3f;
    public GameObject pickupUI;
    public GameObject obtainedUI;

    private GameObject itemToPickup;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance, itemLayer)){
            itemToPickup = hit.collider.gameObject;
            pickupUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpItem();
            }
        }else{
            itemToPickup = null;
            pickupUI.SetActive(false);
        }
    }

    void PickUpItem(){
        obtainedUI.SetActive(true);
        Invoke("HideObtainedUI", 2f);

        Destroy(itemToPickup);
    }

    void HideObtainedUI(){
        obtainedUI.SetActive(false);
    }
}
