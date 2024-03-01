using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    public string winScreenSceneName = "WinScreen";
    public string mainSceneName = "MainScene"; // Change this to the name of your main scene

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with the trigger object
        if (other.CompareTag("Player"))
        {
            // Load the WinScreen scene
            SceneManager.LoadScene(winScreenSceneName);

            // Optional: You can disable the current camera in MainScene
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                mainCamera.gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        // Enable the camera in WinScreen scene when this script is enabled
        Camera winScreenCamera = Camera.main;
        if (winScreenCamera != null)
        {
            winScreenCamera.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        // Disable the camera in MainScene when this script is disabled
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
        }
    }
}
