using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject levelCompleteUI;
    public GameObject menuUI;
    public PlayerController player;
    
    public bool gameHasEnded = false;

    // METHODS
    private void Update() {
        Time.timeScale = menuUI.activeSelf ? 0 : 1; // pausing
        if (Input.GetKeyDown(KeyCode.Escape)) {
            menuUI.SetActive(!menuUI.activeSelf);
        }
    }

    public void LevelComplete() {
        levelCompleteUI.SetActive(true);

        // SHOW NEXT LVL 
        Invoke("LoadNextScene", 2f);
    }

    public void GameOver() {
        if (!gameHasEnded) {
            gameHasEnded = true;
            gameOverUI.SetActive(true);

            Invoke("Restart", 2f);
        }
    }

    public void ContinueGame() {
        menuUI.SetActive(false);
    }

    public IEnumerator GameWon() {
        yield return new WaitForSeconds(1f);

        Text congratulationsText = levelCompleteUI.GetComponentInChildren<Text>();
        congratulationsText.text = "Parabéns! Você foi vacinado";
        levelCompleteUI.SetActive(true);
        player.ResetPlayer();

        yield return new WaitForSeconds(2);
        LoadNextScene(); // It will load the Credits scene 
    }

    private void LoadNextScene() {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(nextSceneIndex);
        } else {
            Debug.Log("THE END");
        }
        
    }

    private void Restart() {
        player.ResetPlayer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
