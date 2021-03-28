using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerController controller;
    public float invulnerableTimer = 0.55f;
    // private bool activeTrigger = false;

    // METHODS
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Enemy Boss")) {
            StartCoroutine(controller.TakeDamage());            
        }

        // If the player collides with a moving platform then set the platform as parent
        // so the player follows the platform
        if (collision.gameObject.CompareTag("MovingPlatform")) {
            transform.SetParent(collision.gameObject.transform);
        }
    }

    // If the player is no longer on top of the moving platform then remove 
    // the platform as the player's parent
    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("MovingPlatform")) {
            transform.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.gameObject.CompareTag("Alcool")) {
            Destroy(trigger.gameObject, 0f);
            FindObjectOfType<AudioManager>().Play("Collect");
            PlayerController.alcoolEmGel++;
        }

        if (trigger.gameObject.CompareTag("Mask")) {
            Destroy(trigger.gameObject);
            FindObjectOfType<AudioManager>().Play("Collect");            
            PlayerController.remainingLives++;
        }

        if (trigger.gameObject.CompareTag("Vaccine")) {
            Destroy(trigger.gameObject);
            StartCoroutine(FindObjectOfType<GameManager>().GameWon());
        }
    }
    
}
