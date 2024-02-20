using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Goes to MainScene where gameplay is
    public void PlayGame(){
        SceneManager.LoadSceneAsync(1);
        ResetGameState();
    }
    
    // Goes to the main menu scene
    public void Main(){
        SceneManager.LoadSceneAsync(0);
        ResetGameState();
    }

    // Goes to the credit scene
    public void Credits(){
        SceneManager.LoadSceneAsync(2);
    }

    // Quits the game
    public void QuitGame(){
        Application.Quit();
    }

    // Function to reset the game state
    private void ResetGameState(){
        PauseMenu.GameIsPaused = false;
        Time.timeScale = 1f;
    }
}
