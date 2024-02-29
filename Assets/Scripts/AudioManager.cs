using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance to ensure only one AudioManager exists throughout the game
    public static AudioManager instance;

    // Audio clips for different sounds
    public AudioClip pickupSound;
    public AudioClip doorOpenSound;

    private void Awake()
    {
        // Singleton pattern: If another AudioManager is instantiated, it's destroyed to maintain uniqueness
        if (instance == null)
        {
            instance = this; // Set this instance as the AudioManager
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy the duplicate instance
        }

        DontDestroyOnLoad(gameObject); // Keep AudioManager alive between scene changes
    }

    // Play the sound effect when an item is picked up
    public void PlayPickupSound(Vector3 position)
    {
        // Create a new GameObject to hold the audio source
        GameObject audioObject = new GameObject("PickupAudio");
        audioObject.transform.position = position; // Set its position to the pickup position

        // Add an AudioSource component to the new GameObject
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        // Play the pickup sound clip through the dynamically created audio source
        if (pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound); // Play the pickup sound once
        }

        // Destroy the audio source and its GameObject after the sound has finished playing
        Destroy(audioObject, pickupSound.length);
    }

    // Play the sound effect when a door opens
    public void PlayDoorOpenSound(Vector3 position)
    {
        // Create a new GameObject to hold the audio source
        GameObject audioObject = new GameObject("DoorOpenAudio");
        audioObject.transform.position = position; // Set its position to the door position

        // Add an AudioSource component to the new GameObject
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        // Play the door open sound clip through the dynamically created audio source
        if (doorOpenSound != null)
        {
            audioSource.PlayOneShot(doorOpenSound); // Play the door open sound once
        }

        // Destroy the audio source and its GameObject after the sound has finished playing
        Destroy(audioObject, doorOpenSound.length);
    }
}
