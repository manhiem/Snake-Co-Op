using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private InputField nameField;

    [SerializeField]
    private Text welcomeText;

    private void Start()
    {
        if(PlayerPrefs.HasKey("Name"))
        {
            nameField.gameObject.SetActive(false);
        }

        welcomeText.text = "Welcome " + PlayerPrefs.GetString("Name");
    }

    public void StartGame()
    {
        PlayerPrefs.SetString("Name", nameField.text);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
}
