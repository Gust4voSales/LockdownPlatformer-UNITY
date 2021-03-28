using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.gameObject.CompareTag("Player")) {
            gameManager.LevelComplete();
        }
    }    
}
