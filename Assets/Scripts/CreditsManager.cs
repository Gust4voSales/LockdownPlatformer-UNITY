using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
        }
    }

    public void OnCreditsEnd() {
        SceneManager.LoadScene(0);
    }
}
