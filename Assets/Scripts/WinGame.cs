using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    void Update()
    {
        // Check if the player presses the "I" key
        if (Input.GetKeyDown(KeyCode.I))
        {
            // Load the WinScreen scene
            SceneManager.LoadScene("WinScreen");

            // Optional: You can disable the current camera in MainScene
            Camera mainCamera = Camera.main;

            // Check if the camera is not null and has not been destroyed
            if (mainCamera != null && mainCamera.gameObject != null)
            {
                mainCamera.gameObject.SetActive(false);
            }

            // Optional: You can also disable other objects in MainScene
            // For example, if there's a player object
            GameObject playerObject = GameObject.FindWithTag("Player");

            // Check if the player object is not null and has not been destroyed
            if (playerObject != null && playerObject.gameObject != null)
            {
                playerObject.gameObject.SetActive(false);
            }
        }
    }
}
