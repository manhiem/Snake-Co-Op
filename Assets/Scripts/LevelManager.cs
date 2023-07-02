using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void ClassicLevel()
    {
        SceneManager.LoadScene(2);
    }
    public void Classic()
    {
        SceneManager.LoadScene(3);
    }
    public void Disco()
    {
        SceneManager.LoadScene(4);
    }
    public void Nokia()
    {
        SceneManager.LoadScene(5);
    }

    //public void CoopLevel()
    //{
    //    SceneManager.LoadScene();
    //}
    //public void Multiplayer()
    //{
    //    SceneManager.LoadScene();
    //}
}
