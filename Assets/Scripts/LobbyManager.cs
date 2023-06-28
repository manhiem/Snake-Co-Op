using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private InputField nameField;

    [SerializeField]
    private Text welcomeText;

    public void StartGame()
    {
        PlayerPrefs.SetString("Name", nameField.text);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
}
