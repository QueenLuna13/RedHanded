using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //Goes to MainScene where gameplay is
    public void PlayGame(){
        SceneManager.LoadSceneAsync(1);
    }
    
    //Goes to the main menu scene
    public void Main(){
        SceneManager.LoadSceneAsync(0);
    }

    //Goes to the credit scene
    public void Credits(){
        SceneManager.LoadSceneAsync(2);
    }

    //Quits the game
    public void QuitGame(){
        Application.Quit();
    }

}