using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{    
    public void PlayAgain()
    {
        SceneManager.LoadScene(2);
    } 
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
