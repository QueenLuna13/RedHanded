using UnityEngine;

public class Pickup : MonoBehaviour
{
    public LayerMask itemLayer; // LayerMask to filter which objects can be picked up
    public float interactDistance = 3f; // Distance at which the player can interact with objects
    public GameObject pickupUI; // UI element to display when the player is near a pickable item
    public GameObject obtainedKeyUI; // UI element to display when the player obtains the key
    public GameObject obtainedStatueUI; // UI element to display when the player obtains the statue

    private GameObject itemToPickup; // Reference to the currently interactable item

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance, itemLayer))
        {
            itemToPickup = hit.collider.gameObject;
            pickupUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpItem();
            }
        }
        else
        {
            itemToPickup = null;
            pickupUI.SetActive(false);
        }
    }

    void PickUpItem()
    {
        // Determine which obtained UI to show based on the type of item picked up
        if (itemToPickup.CompareTag("Key"))
        {
            obtainedKeyUI.SetActive(true);
            Invoke("HideObtainedUI", 2f); // Hide the obtained UI after 2 seconds
        }
        else if (itemToPickup.CompareTag("Statue"))
        {
            obtainedStatueUI.SetActive(true);
            Invoke("HideObtainedUI", 2f); // Hide the obtained UI after 2 seconds
        }

        // Destroy the item in the world
        Destroy(itemToPickup);
    }

    void HideObtainedUI()
    {
        // Hide both obtained UIs
        obtainedKeyUI.SetActive(false);
        obtainedStatueUI.SetActive(false);
    }
}
