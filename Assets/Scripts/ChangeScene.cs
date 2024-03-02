using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public AudioClip buttonSound; // Button click sound
    public AudioSource audioSource; // AudioSource component for playing the sound

    // Goes to MainScene where gameplay is
    public void PlayGame()
    {
        StartCoroutine(PlayAndLoad(1));
    }
    
    // Goes to the main menu scene
    public void Main()
    {
        StartCoroutine(PlayAndLoad(0));
    }

    // Goes to the credit scene
    public void Credits()
    {
        StartCoroutine(PlayAndLoad(2));
    }

    // Quits the game
    public void QuitGame()
    {
        StartCoroutine(PlayAndQuit());
    }

    private IEnumerator PlayAndLoad(int sceneIndex)
    {
        PlayButtonClickSound();
        yield return new WaitForSecondsRealtime(buttonSound.length);
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    private IEnumerator PlayAndQuit()
    {
        PlayButtonClickSound();
        yield return new WaitForSecondsRealtime(buttonSound.length);
        Application.Quit();
    }

    private void PlayButtonClickSound()
    {
        if (audioSource != null && buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);
        }
    }
}