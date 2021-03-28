using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayNewGame() {
        SceneManager.LoadScene("Level 1");
    }

    public void Quit() {
        Application.Quit();
    }
}
